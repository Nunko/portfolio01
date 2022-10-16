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
            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
            this.GetComponent<CanvasGroup>().alpha = 1;
            this.GetComponent<CanvasGroup>().interactable = true;
            paintBoard.SetActive(true);            
            subCamera.enabled = true;
        }

        void HidePaintPanel()
        {
            if (subCamera) subCamera.enabled = false;            
            if (paintBoard) paintBoard.SetActive(false);
            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
            this.GetComponent<CanvasGroup>().alpha = 1;
            this.GetComponent<CanvasGroup>().interactable = true;
        }
    }
}