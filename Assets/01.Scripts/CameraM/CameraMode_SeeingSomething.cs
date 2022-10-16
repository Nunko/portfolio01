using UnityEngine;

namespace Fruit.CameraM
{
    public class CameraMode_SeeingSomething : MonoBehaviour, ICameraMode
    {
        public CameraModeType modeType = CameraModeType.SEEINGSOMETHING;
        public GameObject _lookAtGObj;

        CameraController _cameraController;

        public void Handle(CameraController cameraController)
        {
            if (!_cameraController) _cameraController = cameraController;
            _cameraController.lookAtGObj = _lookAtGObj;
            _cameraController.lookAt = new Vector3(0, 0, 0);
            CameraModeEventBus.Publish(CameraModeType.ALL);
            CameraModeEventBus.Publish(CameraModeType.SEEINGSOMETHING);
        }

        public void SetLookAtGObj(GameObject lookAtGObj)
        {
            _lookAtGObj = lookAtGObj;
        }
    }
}