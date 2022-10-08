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
        static List<GameObject> Signs;

        void OnEnable()
        {
            Signs = new List<GameObject>();
        }

        void OnTriggerEnter(Collider other) 
        {            
            if (other.gameObject.CompareTag("Player") == true)
            {
                if (!_sign) GenerateSign();

                InteractionEventBus.Subscribe(InteractionEventType.SEEINGPAINT, TogglePaintPanel);
                SetActiveTrueSign();
            }        
        }

        void OnTriggerStay(Collider other) 
        {
            if (other.gameObject.CompareTag("Player") == true && Signs.Contains(_sign) == false)
            {
                if (!_sign) GenerateSign();

                InteractionEventBus.Subscribe(InteractionEventType.SEEINGPAINT, TogglePaintPanel);
                SetActiveTrueSign();
            }        
        }

        void OnTriggerExit(Collider other) 
        {
            if (other.gameObject.CompareTag("Player") == true)
            {
                InteractionEventBus.Unsubscribe(InteractionEventType.SEEINGPAINT, TogglePaintPanel);
                Signs.Remove(_sign);
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

        void CheckPaintGameObject(GameObject gObj)
        {
            _seeingSomething.SetLookAtGObj(gObj);
        }

        void SetActiveTrueSign()
        {
            Signs.Add(_sign);
            if (Signs.Count > 1)
            {
                for (int i = 0; i < Signs.Count - 1; i++)
                {
                    Signs[i].SetActive(false);
                }
            }

            CheckPaintGameObject(Signs[(Signs.Count - 1)].transform.parent.Find("Trigger").gameObject);
            Signs[(Signs.Count - 1)].SetActive(true);            
        }
    }
}
