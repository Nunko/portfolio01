using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using Unity.Barracuda;
using TMPro;

namespace Fruit.UIF
{
    // AI 알파벳 분류 모델
    public class Classification : MonoBehaviour
    {        
        public Camera subCamera; // 그림판을 비출 카메라(SubCamera)       
        int currentScreenWidth, currentScreenHeight; // 현재 스크릔 크기       
        int captureWidth, captureHeight; // 그림판 크기      
        float rectX, rectY; // 그림판 rect 값
        
        public NNModel modelFile; // onnx 형식 파일
        
        string[] labels; // 알파벳들을 담을 배열
        public string outputAlphabet; // 결과 값이 가장 높은 알파벳 
        public TextMeshProUGUI inputTextTMPro; // 판정된 알파벳이 표시될 UI
        public string correctAnswer; // 정답

        PaintBoard paintBoard; 
        string folder; // 스크린샷 폴더 경로

        // 변수 할당
        void Start()
        {            
            currentScreenWidth = Screen.width;
            currentScreenHeight = Screen.height;
            captureWidth = (int)(currentScreenWidth * subCamera.rect.width);
            captureHeight = (int)(currentScreenHeight * subCamera.rect.height);
            rectX = currentScreenWidth * subCamera.rect.x;
            rectY = currentScreenHeight * (0.5f - subCamera.rect.y);

            paintBoard = FindObjectOfType<PaintBoard>();

            LoadLabels();                
        }

        void LoadLabels() 
        {
            labels = new string[37];
            string alphabets = "abcdefghijklmnopqrstuvwxyzabdefghnqrt";
            for(int i = 0; i < labels.Length; i++)
            {
                labels[i] = alphabets[i].ToString();
            }            
	    }

        // 비활성화될 때 변수 초기화
        void OnDisable()
        {
            outputAlphabet = "";
            inputTextTMPro.text = "";
        }

        // 알파벳 분류 시작
        public void RecognizeAlphabet()
        {         
            MnistTest();   
        }
                
        void MnistTest()
        {
            // 그림판 이미지를 Texture2D 타입 변수로 받는다
            Texture2D _image = GetTexture2D();
            // AI 모델에 넣을 Tensor 변수
            using var input = new Tensor(1, 28, 28, 1);            
            // 그림판 이미지 스케일을 바꿔 크기를 28*28로
            _image = ScaleTexture(_image, 28, 28);

            // SaveScreenShot(_image, 1.ToString()); // 이미지 확인용 코드

            // input을 구성
            for (var y = 0; y < 28; y++)
            {
                for (var x = 0; x < 28; x++)
                {
                    var tx = x * _image.width  / 28;
                    var ty = y * _image.height / 28;
                    input[0, 27 - y, x, 0] = _image.GetPixel(tx, ty).grayscale;
                }
            }
            
            // Barracuda 라이브러리를 사용하여 EMIST Letters를 학습한 AI 모델을 불러온다
            using var worker =
            ModelLoader.Load(modelFile).CreateWorker(WorkerFactory.Device.GPU);
            // AI 모델에 input을 넣는다
            worker.Execute(input);

            // 예측 값들이 들어있는 결과물
            var output = worker.PeekOutput();
            // 예측 값들 중 가장 높은 값을 구한다 
            List<float> temp = output.ToReadOnlyArray().ToList();
            float max = temp.Max();
            int index = temp.IndexOf(max);

            // 예측 값이 가장 높은 알파벳을 맨 뒤에 추가한다
            outputAlphabet += labels[index];
            // UI에 반영한다            
            if (CheckCorrectAnswer() == true) // 정답이 완성될 경우 
            {                
                paintBoard.SubmitAnswer();
            }            
            else // 아직 정답이 나오지 않은 경우
            {
                inputTextTMPro.text = outputAlphabet;
            }              

            // 생성된 Tensor 타입 변수를 처리한다
            input.Dispose();
            output.Dispose();
        }        

        Texture2D GetTexture2D()
        {
            // 그림판 크기와 위치의 직사각형
            Rect rect = new Rect(rectX, rectY, currentScreenWidth, currentScreenHeight);
            RenderTexture renderTexture = new RenderTexture(currentScreenWidth, currentScreenHeight, 24);
            Texture2D screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);

            subCamera.targetTexture = renderTexture;
            subCamera.Render();

            RenderTexture.active = renderTexture;
            screenShot.ReadPixels(rect, 0, 0);

            subCamera.targetTexture = null;
            RenderTexture.active = null;

            // SaveScreenShot(screenShot, 0.ToString()); // 이미지 확인용 코드

            return screenShot;
        }

        Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
        {
            Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
            Color[] rpixels = result.GetPixels(0);
            float incX = (1.0f / (float)targetWidth);
            float incY = (1.0f / (float)targetHeight);
            for (int px = 0; px < rpixels.Length; px++)
            {
                rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth), incY * ((float)Mathf.Floor(px / targetWidth)));
            }
            result.SetPixels(rpixels, 0);
            result.Apply();
            return result;
        }   

        bool CheckCorrectAnswer()
        {
            if (outputAlphabet == correctAnswer) return true;
            else return false;            
        }

        // 이미지를 저장하여 확인
        void SaveScreenShot(Texture2D screenShot, string filename)
        {
            if (folder == null || folder.Length == 0)
            {
                folder = Application.dataPath;
                if (Application.isEditor)
                {
                    var stringPath = folder + "/..";
                    folder = Path.GetFullPath(stringPath);
                }
                folder += "/screenshots";

                System.IO.Directory.CreateDirectory(folder);
            }
            
            filename = $"{folder}/{filename}.png";

            byte[] fileData = null;
            fileData = screenShot.EncodeToPNG();

            new System.Threading.Thread(() =>
                {
                    var f = System.IO.File.Create(filename);
                    f.Write(fileData, 0, fileData.Length);
                    f.Close();
                    Debug.Log(string.Format("Wrote screenshot {0} of size {1}", filename, fileData.Length));
                }).Start();
        }             
    }
}