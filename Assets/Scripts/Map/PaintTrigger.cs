using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fruit.Behaviour;

namespace Fruit.Map
{
    public class PaintTrigger : MonoBehaviour
    {
        public GameObject Sign;
        public GameObject PaintPanel;

        void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.CompareTag("Player") == true)
            {
                InteractionEventBus.Subscribe(InteractionEventType.SEEINGPAINT, TogglePaintPanel);
                Sign.SetActive(true);
            }        
        }

        void OnTriggerStay(Collider other) 
        {
            if (other.gameObject.CompareTag("Player") == true && Sign.activeSelf == false)
            {
                InteractionEventBus.Subscribe(InteractionEventType.SEEINGPAINT, TogglePaintPanel);
                Sign.SetActive(true);
            }        
        }

        void OnTriggerExit(Collider other) 
        {
            if (other.gameObject.CompareTag("Player") == true)
            {
                InteractionEventBus.Unsubscribe(InteractionEventType.SEEINGPAINT, TogglePaintPanel);
                Sign.SetActive(false);
            }        
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
    }
}
