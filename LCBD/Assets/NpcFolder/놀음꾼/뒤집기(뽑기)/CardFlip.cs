using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardFlip : MonoBehaviour, IPointerClickHandler
{
    public Sprite cardFrontSprite; // �ո� �̹���
    public Sprite cardBackSprite;  // �޸� �̹���

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
        // ���콺 Ŭ�� �� ī�� ������
        isCardFlipped = !isCardFlipped;
        //SetCardSide(isCardFlipped);

        StartCoroutine(RotateOverTime());
    }

    // �ո�� �޸� �̹��� ����
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

            // �ð��� ������ ���� ��� SetCardSide �Լ��� ȣ��
            if (!setCardSideCalled && timeElapsed >= rotationDuration / 2f)
            {
                setCardSideCalled = true;
                SetCardSide(isCardFlipped); // SetCardSide �Լ� ȣ��
            }
        }

        // ��Ȯ�� ��ǥ ������ ����
        transform.rotation = targetQuaternion;
    }
}
