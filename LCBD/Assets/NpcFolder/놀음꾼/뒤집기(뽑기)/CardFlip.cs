using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardFlip : MonoBehaviour, IPointerClickHandler
{
    public Sprite cardFrontSprite; // 앞면 이미지
    public Sprite cardBackSprite;  // 뒷면 이미지

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
        // 마우스 클릭 시 카드 뒤집기
        isCardFlipped = !isCardFlipped;
        //SetCardSide(isCardFlipped);

        StartCoroutine(RotateOverTime());
    }

    // 앞면과 뒷면 이미지 설정
    private void SetCardSide(bool isFlipped)
    {
        if (!isFlipped)
            imageComponent.sprite = cardBackSprite;
        else
            imageComponent.sprite = cardFrontSprite;
    }

    private IEnumerator RotateOverTime()
    {
        float timeElapsed = 0f;
        float half = rotationDuration / 2f;
        bool setCardSideCalled = false;

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
}
