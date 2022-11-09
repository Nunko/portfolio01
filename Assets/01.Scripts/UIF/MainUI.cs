using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fruit.Map;

namespace Fruit.UIF
{
    public class MainUI : MonoBehaviour 
    {
        public static MainUI instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = FindObjectOfType<MainUI>();
                }

                return m_instance;
            }
        }
        private static MainUI m_instance; //싱글턴이 할당될 변수

        public TextMeshProUGUI allCardCoundTextTMPro;
        public int allCardCount;
        string allCardCountText
        {
            get
            {
                if (allCardCount > 99)
                {
                    return "99+";
                }
                else
                {
                    return allCardCount.ToString();
                }
            }
        }

        public GameObject cardDisplayPanel;
        public TextMeshProUGUI cardCoundTextLTMPro;
        public TextMeshProUGUI cardCoundTextRTMPro;

        void Awake()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("보유 카드 초기화");
        }

        void Start()
        {
            DisplayAllCardCount();
        }

        public void ClickMenuButton()
        {
            Debug.Log("우측 상단 버튼 클릭");
        }        

        public void ClickCardButton()
        {
            if (cardDisplayPanel.activeSelf == false)
            {
                DisplayCardCount();
                cardDisplayPanel.SetActive(true);
            }
            else
            {
                cardDisplayPanel.SetActive(false);
            }
        }

        public void DisplayAllCardCount()
        {
            LoadAllCardCount();
            SetAllCardCoundText();
        }

        void LoadAllCardCount()
        {
            string alphabets = "abcdefghijklmnopqrstuvwxyz";
            foreach (var alphabet in alphabets)
            {
                string key = $"card_{alphabet}";
                if (PlayerPrefs.HasKey(key) == true)
                {
                    allCardCount += PlayerPrefs.GetInt(key);
                }
            }
        }

        void SetAllCardCoundText()
        {
            allCardCoundTextTMPro.text = allCardCountText;
        }

        public void DisplayCardCount()
        {
            SetCardCountText();
        }

        void SetCardCountText()
        {
            string alphabets = "abcdefghijklmnopqrstuvwxyz";
            string totalText = "";
            bool isFirst = true;

            for (int i = 0; i < alphabets.Length; i++)
            {
                if (i < alphabets.Length/2)
                {
                    string alphabetCountText = AlphabetCountText(alphabets[i]);
                    string alphabetText = $"{alphabets[i]}:{alphabetCountText}";
                    if (totalText.Length == 0) totalText += $"{alphabetText}";
                    else if (totalText.Length > 0) totalText += $"\n{alphabetText}";                    
                }
                else
                {
                    if (totalText.Length > 0 && isFirst == true)
                    {
                        isFirst = false;
                        cardCoundTextLTMPro.text = totalText;
                        totalText = "";
                    }

                    string alphabetCountText = AlphabetCountText(alphabets[i]);
                    string alphabetText = $"{alphabets[i]}:{alphabetCountText}";
                    if (totalText.Length == 0) totalText += $"{alphabetText}";
                    else if (totalText.Length > 0) totalText += $"\n{alphabetText}";

                    if (i == alphabets.Length - 1)
                    {
                        cardCoundTextRTMPro.text = totalText;
                    }
                }
            }
        }

        string AlphabetCountText(char alphabet)
        {
            int alphabetCount;
            string key = $"card_{alphabet}";
            string alphabetCountText;
            if (PlayerPrefs.HasKey(key) == true)
            {
                alphabetCount = PlayerPrefs.GetInt(key);
                alphabetCountText = (alphabetCount > 99) ? "99+" : alphabetCount.ToString();
            }
            else
            {
                alphabetCountText = 0.ToString();
            }

            return alphabetCountText;
        }
    }
}