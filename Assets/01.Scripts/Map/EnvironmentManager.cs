using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Fruit.Map
{
    public class EnvironmentManager : MonoBehaviour
    {                
        public List<Material> Skyboxes;

        public GameObject lightGObj;
        public List<float> LightIntensities;
        public List<float> ShadowStrengths;

        TimeEventHub _TimeEventHub;
        int timeNumber;

        public GameObject timeImage;
        public List<Sprite> TimeImages;
        public TextMeshProUGUI timeText;

        void Awake()
        {
            _TimeEventHub = this.GetComponent<TimeEventHub>();
        }

        void OnEnable()
        {
            _TimeEventHub.TransfortTimeEventSubscribtion(TimeEventType.ALL, LoadTimeNumber);
            _TimeEventHub.TransfortTimeEventSubscribtion(TimeEventType.ALL, ChangeSkybox);
            _TimeEventHub.TransfortTimeEventSubscribtion(TimeEventType.ALL, ChangeLightIntensities);
            _TimeEventHub.TransfortTimeEventSubscribtion(TimeEventType.ALL, ChangeShadowStrength);
            _TimeEventHub.TransfortTimeEventSubscribtion(TimeEventType.ALL, ChangeTimeImage);
            _TimeEventHub.TransfortTimeEventSubscribtion(TimeEventType.EVERYHOUR, ChangeTimeText);
        }

        void OnDisable() 
        {
            _TimeEventHub.TransfortTimeEventUnsubscribtion(TimeEventType.ALL, LoadTimeNumber);
            _TimeEventHub.TransfortTimeEventUnsubscribtion(TimeEventType.ALL, ChangeSkybox);
            _TimeEventHub.TransfortTimeEventUnsubscribtion(TimeEventType.ALL, ChangeLightIntensities);
            _TimeEventHub.TransfortTimeEventUnsubscribtion(TimeEventType.ALL, ChangeShadowStrength);
            _TimeEventHub.TransfortTimeEventUnsubscribtion(TimeEventType.ALL, ChangeTimeImage);
            _TimeEventHub.TransfortTimeEventUnsubscribtion(TimeEventType.EVERYHOUR, ChangeTimeText);
        }

        void LoadTimeNumber()
        {
            timeNumber = _TimeEventHub.timeNumber;
        }
        
        void ChangeSkybox()
        {           
            RenderSettings.skybox = Skyboxes[timeNumber];
        }

        void ChangeLightIntensities()
        {
            lightGObj.GetComponent<Light>().intensity = LightIntensities[timeNumber];
        }

        void ChangeShadowStrength()
        {
            lightGObj.GetComponent<Light>().shadowStrength = ShadowStrengths[timeNumber];
        }

        void ChangeTimeText()
        {
            int currentH = _TimeEventHub.currentH; 

            int time = currentH%12;
            if (time == 0) time = 12;
            string timeString = (time >= 10) ? time.ToString() : $"0{time.ToString()}";

            string ampm;                        
            if (currentH < 12 || currentH == 24) ampm = "AM";             
            else ampm = "PM";
            
            timeText.GetComponent<TextMeshProUGUI>().text = $"{timeString}\n{ampm}";
        }

        void ChangeTimeImage()
        {
            timeImage.GetComponent<Image>().sprite = TimeImages[timeNumber];
        }
    }
}