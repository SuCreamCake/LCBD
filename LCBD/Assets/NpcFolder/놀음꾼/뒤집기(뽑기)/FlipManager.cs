using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipManager : MonoBehaviour
{
    public CardFlip[] cardArray; // 카드 오브젝트 배열
    public Button ResetButton;

    private void Start()
    {
        ResetButton.interactable = false;
    }

    public void AllFlip()
    {
        foreach (CardFlip card in cardArray)
        {
            card.TrueFlip(); // 모든 카드에 대해 클릭 이벤트 실행
        }

        StartCoroutine(EnableResetButtonAfterDelay(3f));
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
}

