using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro�� ����ϱ� ���� �߰�

public class ShopManager : MonoBehaviour
{
    public TMP_Text CountNumText; // ����
    public Button increaseButton; // ���� �� ��ư
    public Button decreaseButton; // ���� �ٿ� ��ư
    public TMP_Text TotalPriceText; // ����

    private int Count = 1; // ���� ���� ����

    private void Start()
    {
        // TextMeshPro Text ������Ʈ�� �ʱ� ���� ǥ��
        UpdateNumberText();
        // ��ư Ŭ�� �̺�Ʈ ����
        increaseButton.onClick.AddListener(IncreaseNumber);
        decreaseButton.onClick.AddListener(DecreaseNumber);
    }

    public void IncreaseNumber()
    {
        // �� ��ư Ŭ�� �� ���ڸ� 1 ����
        Count++;
        UpdateNumberText();
    }

    public void DecreaseNumber()
    {
        // �Ʒ� ��ư Ŭ�� �� ���ڸ� 1 ���� (0���� �۾����� �ʵ���)
        if (Count > 1)
        {
            Count--;
            UpdateNumberText();
        }
    }

    private void UpdateNumberText()
    {
        // TextMeshPro Text ������Ʈ�� ���� ���ڸ� ǥ�
        CountNumText.text = Count.ToString();
        Total();
    }

    private void Total()
    {
        if (TotalPriceText != null)
        {
            int a = 100 * Count;
            // 텍스트를 변경합니다.
            TotalPriceText.text = a.ToString() + "G";
        }
    }
}


