using UnityEngine;

namespace Fruit.CameraM
{
    public class CameraMode_ViewingPoint : MonoBehaviour, ICameraMode
    {
        public CameraModeType modeType = CameraModeType.VIEWINGPOINT;

        CameraController _cameraController;

        public void Handle(CameraController cameraController)
        {
            if (!_cameraController) _cameraController = cameraController;
            _cameraController.lookAt = new Vector3(0, 0, 0);
            CameraModeEventBus.Publish(CameraModeType.ALL);
            CameraModeEventBus.Publish(CameraModeType.VIEWINGPOINT);
        }
    }
}