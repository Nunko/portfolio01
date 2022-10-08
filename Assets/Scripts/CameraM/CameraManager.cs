using UnityEngine;

namespace Fruit.CameraM
{
    public class CameraManager : MonoBehaviour
    {
        public GameObject player;

        public CameraController _cameraController;

        public Vector3 delta;
        
        Vector3 eyePosition;

        public GameObject MainButtonPanel;
  
        void Awake() {
            eyePosition = new Vector3(0, 1f, 0);
        }

        void OnEnable()
        {
            CameraModeEventBus.Subscribe(CameraModeType.ALL, CheckDelta);
            CameraModeEventBus.Subscribe(CameraModeType.ALL, ToggleMainButtonPanel);
            CameraModeEventBus.Subscribe(CameraModeType.ADVENTURE, MoveCameraAdventureMode);
            CameraModeEventBus.Subscribe(CameraModeType.VIEWINGPOINT, MoveCameraViewingPointMode);
            CameraModeEventBus.Subscribe(CameraModeType.SEEINGSOMETHING, MoveCameraSeeingSomethingMode);            
        }

        void OnDisable()
        {
            CameraModeEventBus.Unsubscribe(CameraModeType.ALL, CheckDelta);
            CameraModeEventBus.Unsubscribe(CameraModeType.ALL, ToggleMainButtonPanel);
            CameraModeEventBus.Unsubscribe(CameraModeType.ADVENTURE, MoveCameraAdventureMode);
            CameraModeEventBus.Unsubscribe(CameraModeType.VIEWINGPOINT, MoveCameraViewingPointMode);
            CameraModeEventBus.Unsubscribe(CameraModeType.SEEINGSOMETHING, MoveCameraSeeingSomethingMode);
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

        void MoveCameraSeeingSomethingMode()
        {
            GameObject lookAtGObj = _cameraController.lookAtGObj;
            transform.position = lookAtGObj.transform.position + delta;
            transform.LookAt(lookAtGObj.transform.parent);
            transform.position += eyePosition/4;
        }

        void ToggleMainButtonPanel()
        {
            if (_cameraController.mode == CameraModeType.ADVENTURE)
            {
                ShowMainButtonPanel();
            }
            else
            {
                HideMainButtonPanel();
            }
        }

        void ShowMainButtonPanel()
        {
            if (MainButtonPanel.GetComponent<CanvasGroup>().alpha == 0)
            {
                MainButtonPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
                MainButtonPanel.GetComponent<CanvasGroup>().alpha = 1;
                MainButtonPanel.GetComponent<CanvasGroup>().interactable = true;
            }            
        }

        void HideMainButtonPanel()
        {
            if (MainButtonPanel.GetComponent<CanvasGroup>().alpha == 1)
            {
                MainButtonPanel.GetComponent<CanvasGroup>().interactable = false;
                MainButtonPanel.GetComponent<CanvasGroup>().alpha = 0;
                MainButtonPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }
}