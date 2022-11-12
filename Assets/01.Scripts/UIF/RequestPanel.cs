using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fruit.CameraM;

namespace Fruit.UIF
{
    public class RequestPanel : MonoBehaviour
    {
        public CameraController _cameraControllaer;

        public GameObject buttonPanel;
        public GameObject keyboardPanel;        

        Fruit.Map.PaintData paintData;
        List<string> paintNames;

        void Start()
        {
            paintData = FindObjectOfType<Fruit.Map.PaintData>();
            paintNames = new List<string>();
            foreach (var paint in paintData.paints)
            {
                paintNames.Add(paint.answer);
            }
        }

        void OnEnable()
        {
            Time.timeScale = 0;
            _cameraControllaer.ChangeCameraModeToViewingPoint();
        }

        void OnDisable()
        {
            if (keyboardPanel.activeSelf == true) ToggleKeyboardPanel();
            if (buttonPanel.activeSelf == false) ToggleButtonPanel();

            _cameraControllaer.ChangeCameraModeToAdventure();
            Time.timeScale = 1;
        }

        public void ToggleButtonPanel()
        {
            buttonPanel.SetActive(!buttonPanel.activeSelf);
        }

        public void ClickOpenKeyboardButton()
        {
            ToggleButtonPanel();
            ToggleKeyboardPanel();
        }

        public void CloseKeyboardButton()
        {
            ToggleKeyboardPanel();
            ToggleButtonPanel();
        }

        void ToggleKeyboardPanel()
        {
            inputTMPro.text = "";
            keyboardPanel.SetActive(!keyboardPanel.activeSelf);
        }

        public TextMeshProUGUI inputTMPro;
        public string playerInputWord;
        public void AddLetter(string alphabet)
        {
            inputTMPro.text += alphabet;            
        }

        public void DeleteLetter()
        {
            inputTMPro.text = inputTMPro.text.Remove(inputTMPro.text.Length - 1, 1);
        }

        public void SubmitWord()
        {
            playerInputWord = inputTMPro.text;
            inputTMPro.text = "";
            CheckWord();
        }

        void CheckWord()
        {
            if (paintNames.Contains(playerInputWord) == true)
            {                
                GetItem();
                gameObject.SetActive(false);
            }
        }

        void GetItem()
        {
            Debug.Log($"{playerInputWord} 획득");
        }
    }
}
