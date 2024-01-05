using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipManager : MonoBehaviour
{
    public CardFlip[] cardArray; // 카드 오브젝트 배열
    public Button ResetButton;
    public Button UseButton;
    public GameObject Window;
    private int CheckforReset;
    public GameObject[] prefabs; // 여러 개의 프리팹을 저장하는 배열
    

    private void Start()
    {
        if(Window != null)
        {
            Window.SetActive(false);
        }
        ResetButton.interactable = false;

        for (int i = 0; i < cardArray.Length; i++)
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
            StartCoroutine(ButtonDelay(4f, ResetButton));
        }
        else
        {
            UseButton.interactable = true;
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
        UseButton.interactable = true;
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
        MinusMoney(2000);
        for (int i = 0; i < cardArray.Length; i++)
        {
            cardArray[i].canClickOn();
        }
        UseButton.interactable = false;
    }

    public void Reward()
    {
        SpawnObjectAtPlayerPosition();
    }

    void SpawnObjectAtPlayerPosition()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Vector3 playerPosition = playerObject.transform.position;
            Vector3 newPlayerPosition = playerPosition;
            newPlayerPosition.z -= 0.1f;

            for (int i = 0; i < prefabs.Length; i++)
            {
                if (prefabs[i] != null)
                {
                    Instantiate(prefabs[i], newPlayerPosition, Quaternion.identity);
                    newPlayerPosition.x += 1f; // 프리팹 간의 간격 조절
                }
                else
                {
                    Debug.LogError("프리팹을 찾을 수 없습니다: " + i);
                }
            }
        }
        else
        {
            Debug.LogError("플레이어 오브젝트를 찾을 수 없습니다. 'Player' 태그를 확인해주세요.");
        }
    }

    public void PlusMoney(int money)
    {
        Debug.Log("성공 Money : " + money);
    }

    public void MinusMoney(int money)
    {
        Debug.Log("차감 Money : " + money);
    }
}
