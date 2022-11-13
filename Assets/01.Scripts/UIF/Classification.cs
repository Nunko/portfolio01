using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using Unity.Barracuda;

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

        void Start()
        {
            currentScreenWidth = Screen.width;
            currentScreenHeight = Screen.height;
            captureWidth = (int)(currentScreenWidth * subCamera.rect.width);
            captureHeight = (int)(currentScreenHeight * subCamera.rect.height);
            rectX = currentScreenWidth * subCamera.rect.x;
            rectY = currentScreenHeight * (0.5f - subCamera.rect.y);

            LoadLabels();                
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
            outputAlphabet = labels[index];
            Debug.Log(outputAlphabet);

            //dispose tensors
            input.Dispose();
            output.Dispose();
        }        
    }
}