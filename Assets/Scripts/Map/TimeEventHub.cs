using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Fruit.Map
{
    public class TimeEventHub : MonoBehaviour
    {        
        static List<TimeEventType> TimeEventTypesExceptALL = 
            new List<TimeEventType>() {
                TimeEventType.MIDNIGHT, 
                TimeEventType.DAWN, 
                TimeEventType.MIDDAY, 
                TimeEventType.EVENING, 
                TimeEventType.DUSK, 
                TimeEventType.MOONNIGHT};

        public List<int> TimeHList;
        public int timeNumber {get; private set;}

        int timeNumberTmp, currentH, currentHTmp;
        
        void Awake()
        {
            currentH = CurrentH();
            timeNumber = TimeToIntConverter();             
        }

        void Start()
        {
            TimeEventBus.Publish(TimeEventType.ALL);
            TimeEventBus.Publish(TimeEventTypesExceptALL[timeNumber]);
            currentHTmp = currentH;
            timeNumberTmp = timeNumber; 
        }

        void LateUpdate() 
        {            
            currentH = CurrentH();
            if (currentH != currentHTmp) {
                currentHTmp = currentH;
                timeNumber = TimeToIntConverter();
                if (timeNumber != timeNumberTmp) {
                    timeNumberTmp = timeNumber;  
                    TimeEventBus.Publish(TimeEventType.ALL);
                    TimeEventBus.Publish(TimeEventTypesExceptALL[timeNumber]);
                }                
            }
        }

        int CurrentH()
        {
            int timeH = DateTime.Now.Hour; //게임 자체 시간 만들어서 적용
            return timeH;
        }

        int TimeToIntConverter()
        {
            int firstH, secondH;

            int timeNumberInConverter = -1;
            for (int i = 0; i < TimeHList.Count; i++)
            {
                firstH = TimeHList[i];
                try {
                    secondH = TimeHList[i + 1];
                } catch {
                    secondH = TimeHList[0];
                }

                if (secondH < firstH) secondH = secondH + 24;                
                
                if (currentH >= firstH && currentH < secondH) {
                    timeNumberInConverter = i;
                    break;
                }
            }

            if(timeNumberInConverter != -1) return timeNumberInConverter;           
            else return 0;
        }

        public void TransfortTimeEvents(List<TimeEventType> eventTypes, UnityAction listener)
        {                         
            if (eventTypes.Contains(TimeEventType.ALL)) {
                TransfortTimeEvent(TimeEventType.ALL, listener);
            } else {
                foreach (var eventType in eventTypes) {
                TransfortTimeEvent(eventType, listener);
                }
            }
        }

        public void TransfortTimeEvent(TimeEventType eventType, UnityAction listener)
        {
            SubscribeInEventHub(eventType, listener);           
        }

        void SubscribeInEventHub(TimeEventType eventType, UnityAction listener)
        {
            TimeEventBus.Subscribe(eventType, listener);
        }

        /*
        void OnGUI()
        {
            GUILayout.Label("Review output in the console:");
            
            GUILayout.Label("currentHH: " + currentHH.ToString());
            GUILayout.Label("timeNumber: " + timeNumber.ToString());

            if (GUILayout.Button("Publish")) {
                TimeEventBus.Publish(TimeEventTypesExceptALL[timeNumber]);
            }
        }
        */
    }
}