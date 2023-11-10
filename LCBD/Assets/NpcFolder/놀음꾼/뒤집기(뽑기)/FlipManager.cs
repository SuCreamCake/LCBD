using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipManager : MonoBehaviour
{
    public CardFlip[] cardArray; // ī�� ������Ʈ �迭
    public Button ResetButton;
    public Button UesButton;
    private int CheckforReset;
    public GameObject prefab;

    private void Start()
    {
        ResetButton.interactable = false;

        for(int i = 0; i < cardArray.Length; i++)
        {
            cardArray[i].canClickOff();
        }
        CheckforReset = 0;
    }

    public void AllClickOff()
    {
        for (int i = 0; i < cardArray.Length; i++)
        {
            cardArray[i].canClickOff();
        }

        CheckforReset++;
        if (CheckforReset >= cardArray.Length)
        {
            StartCoroutine(ButtonDelay(5f, ResetButton));
            
        } else
        {
            UesButton.interactable = true;
        }
        
    }

    IEnumerator ButtonDelay(float delay, Button button)
    {
        yield return new WaitForSeconds(delay);
        button.interactable = true;
    }

    public void AllReset()
    {
        for (int i = 0; i < cardArray.Length; i++)
        {
            cardArray[i].ResetCard();
        }
        StartCoroutine(ButtonDelay(5f, UesButton));
        ResetButton.interactable = false;
        CheckforReset = 0;
    }

    private void OnDisable()
    {
        //AllReset();
    }

    private void OnEnable()
    {
        Start();
    }

    public void UseButtonClick()
    {
        Debug.Log("�̱� ��밡����");
        for (int i = 0; i < cardArray.Length; i++)
        {
            cardArray[i].canClickOn();
        }
        UesButton.interactable = false;
    }

    public void Reward()
    {
        SpawnObjectAtPlayerPosition();
    }

    void SpawnObjectAtPlayerPosition()
    {
        // �÷��̾� �±׸� ���� ������Ʈ�� ã���ϴ�.
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            // �÷��̾� ������Ʈ�� ��ġ�� �������� �������� �ν��Ͻ�ȭ�մϴ�.
            Vector3 playerPosition = playerObject.transform.position;
            Vector3 NewPalyerPosition = playerPosition;
            NewPalyerPosition.z -= 0.1f;

            if (prefab != null)
            {
                Instantiate(prefab, NewPalyerPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogError("������ �ε� ����: ");
            }
        }
        else
        {
            Debug.LogError("�÷��̾� ������Ʈ�� ã�� �� �����ϴ�. 'Player'�� ����.");
        }
    }
}

