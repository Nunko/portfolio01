using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fruit.Map
{
    public class EnvironmentManager : MonoBehaviour
    {                
        public List<int> TimeHHList;
        public List<Material> TimeSkybox;

        int timeNumberTmp;
        int timeNumber;

        void OnEnable()
        {
            timeNumber = TimeToIntConverter();
            ChangeSkybox();
            timeNumberTmp = timeNumber;  
        }

        void LateUpdate() 
        {            
            timeNumber = TimeToIntConverter();
            if (timeNumber != timeNumberTmp) ChangeSkybox();
        }

        int TimeToIntConverter()
        {
            int timeHH = DateTime.Now.Hour; //게임 자체 시간 만들어서 적용
            int firstNum;
            int secondNum;

            int timeNumberInConverter = -1;
            for (int i = 0; i < TimeHHList.Count; i++)
            {
                firstNum = i;
                secondNum = i + 1;
                if (secondNum == TimeHHList.Count) secondNum = 0;
                if (TimeHHList[secondNum] < TimeHHList[firstNum]) secondNum = TimeHHList[secondNum] + 24;
                
                if (timeHH >= firstNum && timeHH < secondNum)
                {
                    timeNumberInConverter = i;
                }
            }

            if(timeNumberInConverter != -1) return timeNumberInConverter;           
            else return 0;
        }
        
        public void ChangeSkybox()
        {           
            RenderSettings.skybox = TimeSkybox[timeNumber];
        }

        /*
        void OnGUI()
        {
            GUILayout.Label("Review output in the console:");
            
            GUILayout.Label("TimeNumber " + timeNumber.ToString());
            
            if (GUILayout.Button("ChangeSkybox")) {
                ChangeSkybox();
            }            
        }
        */
    }
}