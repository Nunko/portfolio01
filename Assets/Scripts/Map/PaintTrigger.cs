using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fruit.CameraM;

namespace Fruit.Map
{
    public class PaintTrigger : MonoBehaviour
    {
        public Object sign;
        GameObject _sign;
        public GameObject paintPanel;
        public CameraMode_SeeingSomething _seeingSomething;

        void OnTriggerEnter(Collider other) 
        {            
            if (other.gameObject.CompareTag("Player") == true)
            {
                if (!_sign) GenerateSign();

                InteractionEventBus.Subscribe(InteractionEventType.SEEINGPAINT, TogglePaintPanel);
                CheckPaintGameObject();
                _sign.SetActive(true);
            }        
        }

        void OnTriggerStay(Collider other) 
        {
            if (other.gameObject.CompareTag("Player") == true && _sign.activeSelf == false)
            {
                if (!_sign) GenerateSign();

                InteractionEventBus.Subscribe(InteractionEventType.SEEINGPAINT, TogglePaintPanel);
                CheckPaintGameObject();
                _sign.SetActive(true);
            }        
        }

        void OnTriggerExit(Collider other) 
        {
            if (other.gameObject.CompareTag("Player") == true)
            {
                InteractionEventBus.Unsubscribe(InteractionEventType.SEEINGPAINT, TogglePaintPanel);
                _sign.SetActive(false);
            }        
        }

        void GenerateSign()
        {            
            var parentTransform = gameObject.transform.parent;
            Vector3 addPostion = new Vector3(0, 0.75f, 0);            
            Vector3 newPositon = parentTransform.position + addPostion;
            _sign = Instantiate(sign, parentTransform) as GameObject;
            _sign.transform.position = newPositon;
        }

        void TogglePaintPanel()
        {
            if (paintPanel.activeSelf == false)
            {
                paintPanel.SetActive(true);
            }
            else
            {
                paintPanel.SetActive(false);
            }
        }

        void CheckPaintGameObject()
        {
            _seeingSomething.SetLookAtGObj(gameObject);
        }
    }
}
