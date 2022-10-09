using UnityEngine;
using Fruit.CameraM;

namespace Fruit.UIF
{
    public class PaintPanel : MonoBehaviour
    {
        public CameraController _cameraControllaer;
        public GameObject paintBoard;
        public Camera subCamera;

        void OnEnable() 
        {
            Time.timeScale = 0;
            _cameraControllaer.ChangeCameraModeToSeeingSomething();           
            ShowPaintPanel();            
        }

        void OnDisable() 
        {
            HidePaintPanel();                     
            _cameraControllaer.ChangeCameraModeToAdventure();      
            Time.timeScale = 1;         
        }

        void ShowPaintPanel()
        {
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            gameObject.GetComponent<CanvasGroup>().interactable = true;
            paintBoard.SetActive(true);            
            subCamera.enabled = true;
        }

        void HidePaintPanel()
        {
            if (subCamera) subCamera.enabled = false;            
            if (paintBoard) paintBoard.SetActive(false);
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            gameObject.GetComponent<CanvasGroup>().interactable = true;
        }
    }
}