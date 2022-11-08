using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fruit.CameraM;
using Fruit.UIF;

namespace Fruit.Map
{
    public class PaintTrigger : MonoBehaviour
    {
        public GameObject sign;
        GameObject _sign;
        GameObject paintPanel;        
        CameraMode_SeeingSomething seeingSomething;
        PaintBoard paintBoard;
        PaintData paintData;

        public PaintScriptableObject paint;
        string correctAnswer;

        static List<int> currentAnswers;

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
            _sign = Instantiate(sign, parentTransform);
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
            seeingSomething.SetLookAtGObj(gObj);
        }

        void SetActiveTrueSign()
        {
            CheckPaintGameObject(gameObject);
            _sign.SetActive(true);            
        }

        void PushCorrectAnswer()
        {
            paintBoard.SetCorrectAnswer(correctAnswer);
        }

        void OnEnable()
        {            
            paintData = this.GetComponentInParent<PaintData>();
            paintPanel = paintData.paintPanel;
            seeingSomething = paintData.seeingSomething;
            paintBoard = paintData.paintBoard;

            SetPaint();
            correctAnswer = paint.answer; 
        }

        void SetPaint()
        {
            int count = paintData.paints.Count;
            paint = paintData.paints[Random.Range(0, count)];
        }

        void SetPaintPanelImage()
        {
            paintPanel.transform.Find("Image").gameObject.GetComponent<Image>().sprite = paint.fruitPaint;
        }   
    }
}
