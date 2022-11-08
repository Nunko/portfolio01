using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Fruit.CameraM;
using Fruit.UIF;

namespace Fruit.Map
{
    public class PaintTrigger : MonoBehaviour
    {
        public Object sign;
        GameObject _sign;
        public GameObject paintPanel;        
        public CameraMode_SeeingSomething _seeingSomething;
        public PaintBoard _paintBoard;

        public Object paintDataFolder;
        public PaintScriptableObject paintData;
        string correctAnswer;

        void OnTriggerStay(Collider other) 
        {
            if (other.gameObject.CompareTag("Player") == true)
            {
                if (!_sign) GenerateSign();
                if (_sign.activeSelf == false)
                {
                    InteractionEventBus.Subscribe(InteractionEventType.SEEINGPAINT, TogglePaintPanel);
                    InteractionEventBus.Subscribe(InteractionEventType.SEEINGPAINT, PushCorrectAnswer);
                    SetActiveTrueSign();
                }                
            }        
        }

        void OnTriggerExit(Collider other) 
        {
            if (other.gameObject.CompareTag("Player") == true)
            {
                InteractionEventBus.Unsubscribe(InteractionEventType.SEEINGPAINT, TogglePaintPanel);
                InteractionEventBus.Unsubscribe(InteractionEventType.SEEINGPAINT, PushCorrectAnswer);
                _sign.SetActive(false);
            }        
        }

        void GenerateSign()
        {            
            var parentTransform = this.transform.parent;
            Vector3 addPostion = new Vector3(0, 0.75f, 0);            
            Vector3 newPositon = parentTransform.position + addPostion;
            _sign = Instantiate(sign, parentTransform) as GameObject;
            _sign.transform.position = newPositon;
        }

        void TogglePaintPanel()
        {
            if (paintPanel.activeSelf == false)
            {
                SetPaintPanelImage();
                paintPanel.SetActive(true);
            }
            else
            {
                paintPanel.SetActive(false);
            }
        }

        void CheckPaintGameObject(GameObject gObj)
        {
            _seeingSomething.SetLookAtGObj(gObj);
        }

        void SetActiveTrueSign()
        {
            CheckPaintGameObject(gameObject);
            _sign.SetActive(true);            
        }

        void PushCorrectAnswer()
        {
            _paintBoard.SetCorrectAnswer(correctAnswer);
        }

        void OnEnable()
        {            
            PickUpPaint();
            correctAnswer = paintData.answer; 
        }
  
        void PickUpPaint()
        {

        }

        void SetPaintPanelImage()
        {
            paintPanel.transform.Find("Image").gameObject.GetComponent<Image>().sprite = paintData.fruitPaint;
        }   
    }
}
