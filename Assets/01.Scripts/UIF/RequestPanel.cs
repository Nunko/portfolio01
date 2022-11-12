using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fruit.CameraM;

namespace Fruit.UIF
{
    public class RequestPanel : MonoBehaviour
    {
        public CameraController _cameraControllaer;

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
    }
}
