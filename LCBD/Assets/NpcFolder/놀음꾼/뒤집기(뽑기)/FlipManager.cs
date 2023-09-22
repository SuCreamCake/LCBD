using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipManager : MonoBehaviour
{
    public CardFlip[] cardArray; // ī�� ������Ʈ �迭
    public Button ResetButton;

    private void Start()
    {
        ResetButton.interactable = false;
    }

    public void AllFlip()
    {
        foreach (CardFlip card in cardArray)
        {
            card.TrueFlip(); // ��� ī�忡 ���� Ŭ�� �̺�Ʈ ����
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

