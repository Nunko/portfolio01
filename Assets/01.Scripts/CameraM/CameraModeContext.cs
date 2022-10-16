namespace Fruit.CameraM
{
    public class CameraModeContext
    {
        public ICameraMode currentMode;
        CameraModeType currentType;
        CameraController _cameraController;

        public CameraModeContext(CameraController cameraController)
        {
            _cameraController = cameraController;
        }

        public void Transition()
        {
            currentMode.Handle(_cameraController);
        }

        public void Transition(ICameraMode mode)
        {
            currentMode = mode;
            Transition();
        }
    }
}