using UnityEngine;
using Fruit.CameraM;

namespace Fruit.UIF
{
    public class PaintPanel : MonoBehaviour
    {
        public CameraController _cameraControllaer;

        void OnEnable() 
        {
            Time.timeScale = 0;
            _cameraControllaer.ChangeCameraModeToSeeingSomething();
        }

        void OnDisable() 
        {
            Time.timeScale = 1;
            _cameraControllaer.ChangeCameraModeToAdventure();
        }
    }
}