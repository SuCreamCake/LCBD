using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro를 사용하기 위해 추가

public class ShopManager : MonoBehaviour
{
    public TMP_Text CountNumText; // 수량
    public Button increaseButton; // 수량 업 버튼
    public Button decreaseButton; // 수량 다운 버튼

    private int Count = 1; // 수량 숫자 저장

    private void Start()
    {
        // TextMeshPro Text 엘리먼트에 초기 숫자 표시
        UpdateNumberText();

        // 버튼 클릭 이벤트 설정
        increaseButton.onClick.AddListener(IncreaseNumber);
        decreaseButton.onClick.AddListener(DecreaseNumber);
    }

    public void IncreaseNumber()
    {
        // 위 버튼 클릭 시 숫자를 1 증가
        Count++;
        UpdateNumberText();
    }

    public void DecreaseNumber()
    {
        // 아래 버튼 클릭 시 숫자를 1 감소 (0보다 작아지지 않도록)
        if (Count > 1)
        {
            Count--;
            UpdateNumberText();
        }
    }

    private void UpdateNumberText()
    {
        // TextMeshPro Text 엘리먼트에 현재 숫자를 표시
        CountNumText.text = Count.ToString();
    }
}


