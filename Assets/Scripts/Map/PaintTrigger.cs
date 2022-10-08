using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fruit.CameraM;

namespace Fruit.Map
{
    public class PaintTrigger : MonoBehaviour
    {
        public Object Sign;
        GameObject _Sign;
        public GameObject PaintPanel;
        public CameraMode_SeeingSomething _seeingSomething;

        void OnTriggerEnter(Collider other) 
        {
            if (!_Sign) GenerateSign();

            if (other.gameObject.CompareTag("Player") == true)
            {
                InteractionEventBus.Subscribe(InteractionEventType.SEEINGPAINT, TogglePaintPanel);
                CheckPaintGameObject();
                _Sign.SetActive(true);
            }        
        }

        void OnTriggerStay(Collider other) 
        {
            if (!_Sign) GenerateSign();

            if (other.gameObject.CompareTag("Player") == true && _Sign.activeSelf == false)
            {
                InteractionEventBus.Subscribe(InteractionEventType.SEEINGPAINT, TogglePaintPanel);
                CheckPaintGameObject();
                _Sign.SetActive(true);
            }        
        }

        void OnTriggerExit(Collider other) 
        {
            if (other.gameObject.CompareTag("Player") == true)
            {
                InteractionEventBus.Unsubscribe(InteractionEventType.SEEINGPAINT, TogglePaintPanel);
                _Sign.SetActive(false);
            }        
        }

        void GenerateSign()
        {            
            var parentTransform = gameObject.transform.parent;
            Vector3 addPostion = new Vector3(0, 0.75f, 0);
            _Sign = Instantiate(Sign, parentTransform) as GameObject;
            _Sign.transform.position = parentTransform.position + addPostion;
        }

        void TogglePaintPanel()
        {
            if (PaintPanel.activeSelf == false)
            {
                PaintPanel.SetActive(true);
            }
            else
            {
                PaintPanel.SetActive(false);
            }
        }

        void CheckPaintGameObject()
        {
            _seeingSomething.SetLookAtGObj(gameObject);
        }
    }
}
