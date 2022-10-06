using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        public GameObject timeBarImage;

        void Awake()
        {
            _TimeEventHub = gameObject.GetComponent<TimeEventHub>();
        }

        void OnEnable()
        {
            _TimeEventHub.TransfortTimeEventSubscribtion(TimeEventType.ALL, LoadTimeNumber);
            _TimeEventHub.TransfortTimeEventSubscribtion(TimeEventType.ALL, ChangeSkybox);
            _TimeEventHub.TransfortTimeEventSubscribtion(TimeEventType.ALL, ChangeLightIntensities);
            _TimeEventHub.TransfortTimeEventSubscribtion(TimeEventType.ALL, ChangeShadowStrength);
            _TimeEventHub.TransfortTimeEventSubscribtion(TimeEventType.EVERYHOUR, FillTimeBackImage);
        }

        void OnDisable() 
        {
            _TimeEventHub.TransfortTimeEventUnsubscribtion(TimeEventType.ALL, LoadTimeNumber);
            _TimeEventHub.TransfortTimeEventUnsubscribtion(TimeEventType.ALL, ChangeSkybox);
            _TimeEventHub.TransfortTimeEventUnsubscribtion(TimeEventType.ALL, ChangeLightIntensities);
            _TimeEventHub.TransfortTimeEventUnsubscribtion(TimeEventType.ALL, ChangeShadowStrength);
            _TimeEventHub.TransfortTimeEventUnsubscribtion(TimeEventType.EVERYHOUR, FillTimeBackImage);    
        }

        public void LoadTimeNumber()
        {
            timeNumber = _TimeEventHub.timeNumber;
        }
        
        public void ChangeSkybox()
        {           
            RenderSettings.skybox = Skyboxes[timeNumber];
        }

        public void ChangeLightIntensities()
        {
            lightGObj.GetComponent<Light>().intensity = LightIntensities[timeNumber];
        }

        public void ChangeShadowStrength()
        {
            lightGObj.GetComponent<Light>().shadowStrength = ShadowStrengths[timeNumber];
        }

        void FillTimeBackImage()
        {
            timeBarImage.GetComponent<Image>().fillAmount = (float) _TimeEventHub.currentH%12/12;
        }
    }
}