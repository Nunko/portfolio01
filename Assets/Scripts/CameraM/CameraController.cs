using UnityEngine;

namespace Fruit.CameraM
{
    public class CameraController : MonoBehaviour
    {
        public Vector3 lookAt;
        public CameraModeType mode;

        ICameraMode _adventure, _viewingPoint;
        CameraModeContext _cameraModeContext;

        void Awake()
        {
            _cameraModeContext = new CameraModeContext(this);
            _adventure = gameObject.AddComponent<CameraMode_Adventure>();
            _viewingPoint = gameObject.AddComponent<CameraMode_ViewingPoint>();            
        }

        void OnEnable() {
            ChangeCameraModeToAdventure();
        }

        public void ChangeCameraModeToAdventure()
        {
            mode = CameraModeType.ADVENTURE;
            _cameraModeContext.Transition(_adventure);
        }

        public void ChangeCameraModeToViewingPoint()
        {
            mode = CameraModeType.VIEWINGPOINT;
            _cameraModeContext.Transition(_viewingPoint);
        }

        /*
        void OnGUI() 
        {
            GUILayout.Label("Review output in the console:");
            
            GUILayout.Label("mode: " + mode);

            if (GUILayout.Button("ADVENTURE")) {
                ChangeCameraModeToAdventure();
            }
            if (GUILayout.Button("VIEWINGPOINT")) {
                ChangeCameraModeToViewingPoint();
            }
        }
        */
    }
}