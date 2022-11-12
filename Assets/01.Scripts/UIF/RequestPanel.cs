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
            gameObject.SetActive(false);
        }
    }
}
