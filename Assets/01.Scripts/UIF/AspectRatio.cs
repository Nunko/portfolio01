using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI 배치를 위해 화면 비율에 따라 CanvasScaler의 matchWidthOrHeight를 변경함
public class AspectRatio : MonoBehaviour
{
    // UI 캔버스을 담을 변수
    public GameObject UICanvas;
    // ReferenceResolution 값을 담을 변수
    int referenceResolutionWidth, referenceResolutionHeight;
    // ReferenceResolution 화면 비율을 담을 변수
    float referenceResolutionRatio;
    // 현재 화면 크기를 저장할 변수
    int screenWidthTmp, screenHeightTmp;

    void Start()
    {
        // 현재 ReferenceResolution 값을 담는다
        referenceResolutionWidth = (int)UICanvas.GetComponent<CanvasScaler>().referenceResolution.x;
        referenceResolutionHeight = (int)UICanvas.GetComponent<CanvasScaler>().referenceResolution.y;
        // 위 값으로 ReferenceResolution 화면 비율을 구한다
        referenceResolutionRatio = (float)referenceResolutionWidth / referenceResolutionHeight;
    }

    void Update()
    {
        // 변수를 새로 선언하여 현재 화면 크기를 담는다
        int currentScreenWidth = Screen.width;
        int currentScreenHeight = Screen.height;
        
        // 만약 화면 크기가 변화했다면
        if (currentScreenWidth != screenWidthTmp || currentScreenHeight != screenHeightTmp)
        {
            // 현재 화면 크기를 저장하고 다음 단계로 넘어간다
            screenWidthTmp = currentScreenWidth;
            screenHeightTmp = currentScreenHeight;
        }
        // 만약 화면 크기가 변화하지 않았다면
        else
        {
            // Update 메서드를 종료한다
            return;
        }

        Debug.Log("화면 비율 변화");
        
        // 위 값으로 현재 화면 비율을 구한다
        float currentScreenRatio = (float)currentScreenWidth / currentScreenHeight;

        // ReferenceResolution 화면 비율과 현재 화면 비율을 비교하여
        // 현재 화면 비율이 ReferenceResolution에 비해
        // 세로로 더 길면
        if (referenceResolutionRatio >= currentScreenRatio)
        {
            UICanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
        }
        // 가로로 더 길면
        else
        {
            UICanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
        }
    }
}
