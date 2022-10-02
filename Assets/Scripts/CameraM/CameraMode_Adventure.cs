using UnityEngine;

namespace Fruit.CameraM
{
    public class CameraMode_Adventure : MonoBehaviour, ICameraMode
    {
        public CameraModeType modeType = CameraModeType.ADVENTURE;

        CameraController _cameraController;

        public void Handle(CameraController cameraController)
        {
            if (!_cameraController) _cameraController = cameraController;
            _cameraController.lookAt = new Vector3(5, 5, 5);
            CameraModeEventBus.Publish(CameraModeType.ALL);
            CameraModeEventBus.Publish(CameraModeType.ADVENTURE);
        }
    }
}