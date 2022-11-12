using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            keyboardPanel.SetActive(!keyboardPanel.activeSelf);
        }
    }
}
