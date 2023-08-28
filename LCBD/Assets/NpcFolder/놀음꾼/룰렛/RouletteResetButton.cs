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
        //���� ��ư ��Ȱ��ȭ
        ResetButton.interactable = false;
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
