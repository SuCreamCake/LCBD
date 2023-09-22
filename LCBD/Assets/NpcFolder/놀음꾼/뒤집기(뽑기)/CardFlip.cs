using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardFlip : MonoBehaviour, IPointerClickHandler
{
    public FlipManager FlipManager;
    public Sprite cardFrontSprite; // 앞면 이미지
    public Sprite cardBackSprite;  // 뒷면 이미지

    private bool canClick = true; // 클릭 가능한 상태인지 여부

    private bool Clicker = false; // 클릭한 카드인지 확인

    private Image imageComponent;
    private bool isCardFlipped = false;


    private float targetRotation = 180f;
    private float rotationDuration = 2f;

    private Quaternion startRotation;
    private Quaternion targetQuaternion;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        SetCardSide(isCardFlipped);

        startRotation = transform.rotation;
        targetQuaternion = Quaternion.Euler(0f, targetRotation, 0f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canClick)
            return; // 클릭 금지 상태일 때 클릭 이벤트 처리 중단

        canClick = !canClick;
        Clicker = true;
        // 마우스 클릭 시 카드 뒤집기
        isCardFlipped = !isCardFlipped;
        //SetCardSide(isCardFlipped);
        Debug.Log(cardFrontSprite.name);
        FlipManager.AllFlip();
        StartCoroutine(RotateOverTime(true));
    }

    // 앞면과 뒷면 이미지 설정
    private void SetCardSide(bool isFlipped)
    {
        if (!isFlipped)
            imageComponent.sprite = cardBackSprite;
        else
            imageComponent.sprite = cardFrontSprite;
    }

    private IEnumerator RotateOverTime(bool select)
    {

        float timeElapsed = 0f;
        float half = rotationDuration / 2f;
        bool setCardSideCalled = false;

        if (!select)
            yield return new WaitForSeconds(1f); // 1초 동안 기다림

        while (timeElapsed < rotationDuration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / rotationDuration;
            transform.rotation = Quaternion.Lerp(startRotation, targetQuaternion, t);
            yield return null;

            // 시간의 절반을 지난 경우 SetCardSide 함수를 호출
            if (!setCardSideCalled && timeElapsed >= rotationDuration / 2f)
            {
                setCardSideCalled = true;
                SetCardSide(isCardFlipped); // SetCardSide 함수 호출
            }
        }

        // 정확히 목표 각도로 설정
        transform.rotation = targetQuaternion;
    }

    public void TrueFlip()
    {
        if (Clicker)
        {
            return;
        }
        canClick = !canClick;
        isCardFlipped = !isCardFlipped;
        StartCoroutine(RotateOverTime(false));
    }

    public void ResetCard()
    {
        // 변수들을 초기 상태로 설정
        canClick = true;
        Clicker = false;
        isCardFlipped = false;

        // Set the card's rotation around the Y-axis to 0
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        // 카드를 뒷면으로 설정
        SetCardSide(isCardFlipped);
    }

}
