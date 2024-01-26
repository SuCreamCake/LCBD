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
    private Player PlayerScript;

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

        // "Player" 태그를 가진 첫 번째 오브젝트를 찾아 스크립트 가져오기
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            PlayerScript = playerObject.GetComponent<Player>();
        }
        else
        {
            Debug.Log("Player 태그를 가진 오브젝트를 찾을 수 없습니다.");
        }
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
            //15원 성공 머니
            PlusMoney(15);
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
        if (PlayerScript != null)
        {
            PlayerScript.plusMoney(money);
            Debug.Log("성공 Money : " + money);
        }
    }

    public bool MinusMoney(int money)
    {
        if (PlayerScript != null)
        {
            if (PlayerScript.GetMoney() >= money)
            {
                PlayerScript.minusMoney(money);
                Debug.Log("차감 Money : " + money);
                return true;
            }
        }
        return false;
    }
}
