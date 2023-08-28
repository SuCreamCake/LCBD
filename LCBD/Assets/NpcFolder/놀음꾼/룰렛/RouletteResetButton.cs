using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteResetButton : MonoBehaviour
{
    public Button ResetButton;

    public void OnButtonClicked()
    {
        ResetGame();
        //리셋 버튼 비활성화
        ResetButton.interactable = false;
    }

    public void ResetGame()
    {
        // 모든 RouletteController 스크립트의 ResumeSpinning 메서드 실행
        RouletteController[] roulettes = FindObjectsOfType<RouletteController>();
        foreach (RouletteController roulette in roulettes)
        {
            roulette.ResumeSpinning();
        }
    }
    void OnDisable()
    {
        OnButtonClicked();
    }
}
