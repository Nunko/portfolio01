using System;
using System.Collections.Generic;
using UnityEngine;

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

        void Awake()
        {
            _TimeEventHub = gameObject.GetComponent<TimeEventHub>();
        }

        void OnEnable()
        {
            _TimeEventHub.TransfortTimeEvent(TimeEventType.ALL, LoadTimeNumber);
            _TimeEventHub.TransfortTimeEvent(TimeEventType.ALL, ChangeSkybox);
            _TimeEventHub.TransfortTimeEvent(TimeEventType.ALL, ChangeLightIntensities);
            _TimeEventHub.TransfortTimeEvent(TimeEventType.ALL, ChangeShadowStrength);
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
    }
}