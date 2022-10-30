using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEditor.AddressableAssets;

namespace Fruit.Map
{
    public class PaintAddressable : MonoBehaviour
    {        
        List<string> answers;
        List<int> currentAnswers;

        void Awake()
        {
            FillAnswers();
            currentAnswers = new List<int>();
            // 저장 데이터에서 불러오는 코드 추후 추가
        }

        void FillAnswers()
        {
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            foreach (var group in settings.groups)
            {
                if (group.Name != "Paint") continue;

                answers = new List<string>();

                foreach (var entry in group.entries)
                {
                    answers.Add(entry.address.ToString());
                }

                break;                
            }
        }

        public string PickUpAnswer()
        {
            int randNumber = Random.Range(0, answers.Count);
            string answer = answers[randNumber];

            if (currentAnswers.Contains(randNumber) == true)
            {
                while (currentAnswers.FindAll(x => x == randNumber).Count > 3)
                {
                    randNumber = Random.Range(0, answers.Count);
                }                             
            }            

            return answer;
        }
    }
}
