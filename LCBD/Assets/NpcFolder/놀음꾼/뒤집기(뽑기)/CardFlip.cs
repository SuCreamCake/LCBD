using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardFlip : MonoBehaviour, IPointerClickHandler
{
    public FlipManager FlipManager;
    public Sprite cardFrontSprite; // 카드 앞면 이미지
    public Sprite cardBackSprite;  // 카드 뒷면 이미지

    private bool canClick = true; // 클릭 가능한지 여부를 확인

    private bool Clicker = false; // 클릭이 이루어졌는지 확인

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
            return; // 클릭 가능하지 않으면 클릭 이벤트를 무시

        if (Clicker)
        {
            return; //클릭이 이루어졌다면 반환
        }

        canClick = !canClick;
        Clicker = true;
        // 마우스 클릭 시 카드를 뒤집음
        isCardFlipped = !isCardFlipped;
        //SetCardSide(isCardFlipped);
        Debug.Log(cardFrontSprite.name);
        //FlipManager.AllFlip();
        FlipManager.AllClickOff();
        StartCoroutine(RotateOverTime(true));

        FlipManager.Reward();
    }

    // 카드 앞면 또는 뒷면 이미지 설정
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

            // 시간이 절반 지나면 카드 이미지 SetCardSide 함수 호출
            if (!setCardSideCalled && timeElapsed >= rotationDuration / 2f)
            {
                setCardSideCalled = true;
                SetCardSide(isCardFlipped); // SetCardSide 함수 호출
            }
        }

        // 최종적으로 목표 지점으로 회전
        transform.rotation = targetQuaternion;
    }

    public void ResetCard()
    {
        // 클릭 가능상태로 초기화
        canClickOff();
        Clicker = false;
        isCardFlipped = false;

        // Set the card's rotation around the Y-axis to 0
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        // 카드를 뒷면으로 변경
        SetCardSide(isCardFlipped);
    }

    public void canClickOn()
    {
        canClick = true;
    }

    public void canClickOff()
    {
        canClick = false;
    }
}
