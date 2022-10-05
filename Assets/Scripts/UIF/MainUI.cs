using UnityEngine;
using UnityEngine.UI;
using Fruit.Map;

namespace Fruit.UIF
{
    public class MainUI : MonoBehaviour 
    {
        public TimeEventHub _TimeEventHub;
        public GameObject timeBackImage;
        
        void OnEnable()
        {
            _TimeEventHub.TransfortTimeEvent(TimeEventType.EVERYHOUR, FillTimeBackImage);
        }      

        void FillTimeBackImage()
        {
            timeBackImage.GetComponent<Image>().fillAmount = (float) _TimeEventHub.currentH%12/12;
        }
    }
}