using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using Unity.Barracuda;
using TMPro;

namespace Fruit.UIF
{
    public class Classification : MonoBehaviour
    {
        public Camera subCamera;
        int currentScreenWidth, currentScreenHeight;
        int captureWidth, captureHeight;
        float rectX, rectY;

        public NNModel modelFile;

        string[] labels;
        public string outputAlphabet;
        public TextMeshProUGUI inputTextTMPro;
        public string correctAnswer;

        PaintBoard paintBoard;
        string folder;

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

        void OnDisable()
        {
            outputAlphabet = "";
            inputTextTMPro.text = "";
        }

        public void RecognizeAlphabet()
        {         
            MnistTest();   
        }

        Texture2D GetTexture2D()
        {
            Rect rect = new Rect(rectX, rectY, currentScreenWidth, currentScreenHeight);
            RenderTexture renderTexture = new RenderTexture(currentScreenWidth, currentScreenHeight, 24);
            Texture2D screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);

            subCamera.targetTexture = renderTexture;
            subCamera.Render();

            RenderTexture.active = renderTexture;
            screenShot.ReadPixels(rect, 0, 0);

            subCamera.targetTexture = null;
            RenderTexture.active = null;

            // SaveScreenShot(screenShot, 0.ToString());

            return screenShot;
        }

        void LoadLabels() 
        {
            labels = new string[26];
            string alphabets = "abcdefghijklmnopqrstuvwxyz";
            for(int i = 0; i < labels.Length; i++)
            {
                labels[i] = alphabets[i].ToString();
            }            
	    }

        void MnistTest()
        {
            NNModel _model = modelFile;
            Texture2D _image = GetTexture2D();
            using var input = new Tensor(1, 28, 28, 1);            

            _image = ScaleTexture(_image, 28, 28);
            // SaveScreenShot(_image, 1.ToString());
            for (var y = 0; y < 28; y++)
            {
                for (var x = 0; x < 28; x++)
                {
                    var tx = x * _image.width  / 28;
                    var ty = y * _image.height / 28;
                    input[0, 27 - y, x, 0] = _image.GetPixel(tx, ty).grayscale;
                }
            }
            
            // Run the MNIST model.
            using var worker =
            ModelLoader.Load(_model).CreateWorker(WorkerFactory.Device.GPU);

            worker.Execute(input);

            // Inspect the output tensor.
            var output = worker.PeekOutput();

            List<float> temp = output.ToReadOnlyArray().ToList();
            float max = temp.Max();
            int index = temp.IndexOf(max);

            //set UI text
            outputAlphabet += labels[index];
            if (CheckCorrectAnswer() == true)
            {                
                paintBoard.SubmitAnswer();
            }
            else
            {
                inputTextTMPro.text = outputAlphabet;
            }              

            //dispose tensors
            input.Dispose();
            output.Dispose();
        }        

        bool CheckCorrectAnswer()
        {
            if (outputAlphabet == correctAnswer) return true;
            else return false;            
        }

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
                    // create file and write optional header with image bytes
                    var f = System.IO.File.Create(filename);
                    f.Write(fileData, 0, fileData.Length);
                    f.Close();
                    Debug.Log(string.Format("Wrote screenshot {0} of size {1}", filename, fileData.Length));
                }).Start();
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
    }
}