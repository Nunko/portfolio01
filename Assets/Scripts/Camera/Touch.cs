using UnityEngine;

namespace Fruit.Camera
{
    public class Touch : MonoBehaviour
    {
        public GameObject lookPoint;

        public float rotateSpeed;
        
        float xRotateMove;
        float yRotateMove;
        Vector2 touchPoint;

        UnityEngine.Camera mainCamera;
        
        void Awake()
        {
            mainCamera = UnityEngine.Camera.main;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) touchPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (Input.GetMouseButton(0))
            {
                RotateCamera();
            }
        }

        void RotateCamera()
        {
            xRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;
            yRotateMove = Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;

            Vector3 lookPointPositon = lookPoint.transform.position;

            mainCamera.transform.RotateAround(lookPointPositon, Vector3.right, -yRotateMove);
            mainCamera.transform.RotateAround(lookPointPositon, Vector3.up, xRotateMove);

            mainCamera.transform.LookAt(lookPointPositon);
        }
    }
}