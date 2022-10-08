using UnityEngine;

namespace Fruit.CameraM
{
    public class CameraController : MonoBehaviour
    {
        public Vector3 lookAt;
        public CameraModeType mode;
        public GameObject lookAtGObj;

        ICameraMode _adventure, _viewingPoint, _seeingSomething;
        CameraModeContext _cameraModeContext;

        void Awake()
        {
            _cameraModeContext = new CameraModeContext(this);
            _adventure = gameObject.GetComponent<CameraMode_Adventure>();
            _viewingPoint = gameObject.GetComponent<CameraMode_ViewingPoint>();    
            _seeingSomething = gameObject.GetComponent<CameraMode_SeeingSomething>();        
        }

        void Start() 
        {
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

        public void ChangeCameraModeToSeeingSomething()
        {
            mode = CameraModeType.SEEINGSOMETHING;
            _cameraModeContext.Transition(_seeingSomething);
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