using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteManager : MonoBehaviour
{
    private int[] dataArray = new int[3];
    private int index;
    public Button ResetButton;

    private void Start()
    {
        index = 0;
        // 배열을 0으로 초기화합니다.
        for (int i = 0; i < dataArray.Length; i++)
        {
            dataArray[i] = 0;
        }
        //리셋 버튼 비활성화
        ResetButton.interactable = false;
    }

    public void UpdateRouletteResult(int value)
    {
        dataArray[index] = value;

        bool hasTwo = false;
        bool allOnes = true;

        for (int i = 0; i < dataArray.Length; i++)
        {
            if (dataArray[i] == 2)
            {
                hasTwo = true;
                break; // 하나라도 2가 있다면 검사 중단
            }

            if (dataArray[i] != 1)
            {
                allOnes = false;
            }
        }

        if (hasTwo)
        {
            Debug.Log("원금 손실");
            ResetArray();
            AllStop();
            //리셋 버튼 활성화
            ResetButton.interactable = true;
        }
        else if (allOnes)
        {
            Debug.Log("상금 획득");
            ResetArray();
            //리셋 버튼 활성화
            ResetButton.interactable = true;
        }

        index = (index + 1) % dataArray.Length; // index가 배열 길이를 초과할 경우 원형으로 돌아가도록 처리
    }

    public void ResetArray()
    {
        index = 0;
        for (int i = 0; i < dataArray.Length; i++)
        {
            dataArray[i] = 0;
        }

    }

    public void AllStop()
    {
        // 모든 RouletteController 스크립트의 ResumeSpinning 메서드 실행
        RouletteController[] roulettes = FindObjectsOfType<RouletteController>();
        foreach (RouletteController roulette in roulettes)
        {
            roulette.AllStop();
        }
    }

    void OnDisable()
    {
        ResetArray();
    }
}
