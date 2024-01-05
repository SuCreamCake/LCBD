using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteManager : MonoBehaviour
{
    private int[] dataArray = new int[3];
    private int index;
    public Button ResetButton;
    public GameObject Window;

    private void Start()
    {
        if (Window != null)
        {
            Window.SetActive(false);
        }
        index = 0;
        // �迭�� 0���� �ʱ�ȭ�մϴ�.
        for (int i = 0; i < dataArray.Length; i++)
        {
            dataArray[i] = 0;
        }
        AllStop();
        //���� ��ư ��Ȱ��ȭ
        //ResetButton.interactable = false;
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
                break; // �ϳ��� 2�� �ִٸ� �˻� �ߴ�
            }

            if (dataArray[i] != 1)
            {
                allOnes = false;
            }
        }

        if (hasTwo)
        {
            Debug.Log("전체 실패");
            ResetArray();
            AllStop();
            //���� ��ư Ȱ��ȭ
            ResetButton.interactable = true;
        }
        else if (allOnes)
        {
            Debug.Log("전체 성공");
            ResetArray();
            //���� ��ư Ȱ��ȭ
            ResetButton.interactable = true;
            //1000원 성공 머니
            PlusMoney(1000);
        }

        index = (index + 1) % dataArray.Length; // index�� �迭 ���̸� �ʰ��� ��� �������� ���ư����� ó��
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
        // ��� RouletteController ��ũ��Ʈ�� ResumeSpinning �޼��� ����
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

    public void PlusMoney(int money)
    {
        // 돈 추가 메소드에 연결
        Debug.Log("성공 Money : " + money);
    }

    public void MinusMoney(int money)
    {
        // 돈 빼기 메소드에 연결\
        Debug.Log("차감 Money : " + money);
    }
}
