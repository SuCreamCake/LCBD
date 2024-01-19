using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteResetButton : MonoBehaviour
{
    public Button ResetButton;
    public RouletteManager RouletteManager;

    public void OnButtonClicked()
    {
        ResetGame();
        //���� ��ư ��Ȱ��ȭ
        ResetButton.interactable = false;
        if(RouletteManager != null)
        {
            RouletteManager.MinusMoney(10);
        }
    }

    public void ResetGame()
    {
        // ��� RouletteController ��ũ��Ʈ�� ResumeSpinning �޼��� ����
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
