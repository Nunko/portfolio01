using UnityEngine;
using UnityEngine.UI;
using Fruit.Map;

namespace Fruit.UIF
{
    public class MainUI : MonoBehaviour 
    {   
        void Awake()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("보유 카드 초기화");
        }       

        public void ClickMenuButton()
        {
            Debug.Log("우측 상단 버튼 클릭");
        }        
    }
}