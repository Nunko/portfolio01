using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fruit.Map
{
    public class EnvironmentManager : MonoBehaviour
    {                
        public List<Material> Skyboxes;

        TimeEventHub _TimeEventHub;

        void Awake()
        {
            _TimeEventHub = gameObject.GetComponent<TimeEventHub>();
        }

        void OnEnable()
        {
            _TimeEventHub.TransfortTimeEvent(TimeEventType.ALL, ChangeSkybox);
        }
        
        public void ChangeSkybox()
        {           
            int timeNumber = _TimeEventHub.timeNumber;
            RenderSettings.skybox = Skyboxes[timeNumber];
        }
    }
}