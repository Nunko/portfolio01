using UnityEngine;

namespace Fruit.CameraM
{
    public class CameraManager : MonoBehaviour
    {
        public GameObject player;

        public CameraController _cameraController;

        public Vector3 delta;
        
        Vector3 eyePosition;
  
        void Awake() {
            eyePosition = new Vector3(0, 1f, 0);
        }

        void OnEnable()
        {
            CameraModeEventBus.Subscribe(CameraModeType.ALL, CheckDelta);
            CameraModeEventBus.Subscribe(CameraModeType.ADVENTURE, MoveCameraAdventureMode);
            CameraModeEventBus.Subscribe(CameraModeType.VIEWINGPOINT, MoveCameraViewingPointMode);
        }

        void Start()
        {
            CheckDelta();
        }

        void LateUpdate()
        {
            if (_cameraController.mode == CameraModeType.ADVENTURE) {
                MoveCameraAdventureMode();
            }          
        }

        void CheckDelta()
        {
            if (delta != _cameraController.lookAt) delta = _cameraController.lookAt;
        }

        void MoveCameraAdventureMode()
        {
            transform.position = player.transform.position + delta;
            transform.LookAt(player.transform);
        }

        void MoveCameraViewingPointMode()
        {
            transform.position = player.transform.position + delta + eyePosition;
            transform.rotation = player.transform.rotation;
        }
    }
}