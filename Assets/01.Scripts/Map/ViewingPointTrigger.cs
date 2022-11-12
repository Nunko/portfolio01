using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fruit.CameraM;
using Fruit.Behaviour;

namespace Fruit.Map
{
    public class ViewingPointTrigger : MonoBehaviour
    {
        public GameObject requestPanel;

        public Material[] materials;

        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player") == true)
            {
                PlayerInputController.instance.trigger = "VIEWINGPOINT";
                this.GetComponentInParent<MeshRenderer>().material = materials[1];

                InteractionEventBus.Subscribe(InteractionEventType.VIEWINGPOINT, ToggleRequestPanel);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player") == true)
            {
                this.GetComponentInParent<MeshRenderer>().material = materials[0];

                InteractionEventBus.Unsubscribe(InteractionEventType.VIEWINGPOINT, ToggleRequestPanel);
            }
        }

        void ToggleRequestPanel()
        {
            requestPanel.SetActive(!requestPanel.activeSelf);
        }
    }
}
