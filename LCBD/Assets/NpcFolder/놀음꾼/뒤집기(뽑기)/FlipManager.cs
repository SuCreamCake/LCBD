using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipManager : MonoBehaviour
{
    public CardFlip[] cardArray; // 카드 오브젝트 배열
    public Button ResetButton;
    public Button UesButton;

    public GameObject prefab;

    private void Start()
    {
        ResetButton.interactable = false;

        for(int i = 0; i < cardArray.Length; i++)
        {
            cardArray[i].canClickOff();
        }

    }

    public void AllClickOff()
    {
        for (int i = 0; i < cardArray.Length; i++)
        {
            cardArray[i].canClickOff();
        }


        UesButton.interactable = true;
    }

    IEnumerator EnableResetButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetButton.interactable = true;
    }

    public void AllReset()
    {
        foreach (CardFlip card in cardArray)
        {
            card.ResetCard();
        }
        ResetButton.interactable = false;
    }

    private void OnDisable()
    {
        AllReset();
    }

    private void OnEnable()
    {
        Start();
    }

    public void UseButtonClick()
    {
        Debug.Log("뽑기 비용가져감");
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
        // 플레이어 태그를 가진 오브젝트를 찾습니다.
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            // 플레이어 오브젝트의 위치를 기준으로 프리팹을 인스턴스화합니다.
            Vector3 playerPosition = playerObject.transform.position;
            Vector3 NewPalyerPosition = playerPosition;
            NewPalyerPosition.z -= 0.1f;

            if (prefab != null)
            {
                Instantiate(prefab, NewPalyerPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogError("프리팹 로드 실패: ");
            }
        }
        else
        {
            Debug.LogError("플레이어 오브젝트를 찾을 수 없습니다. 'Player'가 없다.");
        }
    }
}

