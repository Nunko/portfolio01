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

        public List<int> TimeHHList;
        public int timeNumber {get; private set;}

        int timeNumberTmp, currentHH, currentHHTmp;
        
        void Awake()
        {
            currentHH = CurrentHH();
            timeNumber = TimeToIntConverter();             
        }

        void Start()
        {
            TimeEventBus.Publish(TimeEventTypesExceptALL[timeNumber]);
            currentHHTmp = currentHH;
            timeNumberTmp = timeNumber; 
        }

        void LateUpdate() 
        {            
            currentHH = CurrentHH();
            if (currentHH != currentHHTmp) {
                currentHHTmp = currentHH;
                timeNumber = TimeToIntConverter();
                if (timeNumber != timeNumberTmp) {
                    timeNumberTmp = timeNumber;  
                    TimeEventBus.Publish(TimeEventTypesExceptALL[timeNumber]);
                }                
            }
        }

        int CurrentHH()
        {
            int timeHH = DateTime.Now.Hour; //게임 자체 시간 만들어서 적용
            return DateTime.Now.Hour;
        }

        int TimeToIntConverter()
        {
            int firstHH, secondHH;

            int timeNumberInConverter = -1;
            for (int i = 0; i < TimeHHList.Count; i++)
            {
                firstHH = TimeHHList[i];
                try {
                    secondHH = TimeHHList[i + 1];
                } catch {
                    secondHH = TimeHHList[0];
                }

                if (secondHH < firstHH) secondHH = secondHH + 24;                
                
                if (currentHH >= firstHH && currentHH < secondHH) {
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
            if (eventType == TimeEventType.ALL) {
                foreach (var time in TimeEventTypesExceptALL) {
                    SubscribeInEventHub(time, listener);
                }
            } else {
                SubscribeInEventHub(eventType, listener);
            }            
        }

        void SubscribeInEventHub(TimeEventType eventType, UnityAction listener)
        {
            TimeEventBus.Subscribe(eventType, listener);
        }

        
        void OnGUI()
        {
            GUILayout.Label("Review output in the console:");
            
            GUILayout.Label("timeNumber: " + currentHH.ToString());
            GUILayout.Label("timeNumber: " + timeNumber.ToString());

            if (GUILayout.Button("Publish")) {
                TimeEventBus.Publish(TimeEventTypesExceptALL[timeNumber]);
            }
        }
          
    }
}