using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardFlip : MonoBehaviour, IPointerClickHandler
{
    public FlipManager FlipManager;
    public Sprite cardFrontSprite; // �ո� �̹���
    public Sprite cardBackSprite;  // �޸� �̹���

    private bool canClick = true; // Ŭ�� ������ �������� ����

    private bool Clicker = false; // Ŭ���� ī������ Ȯ��

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
            return; // Ŭ�� ���� ������ �� Ŭ�� �̺�Ʈ ó�� �ߴ�

        canClick = !canClick;
        Clicker = true;
        // ���콺 Ŭ�� �� ī�� ������
        isCardFlipped = !isCardFlipped;
        //SetCardSide(isCardFlipped);
        Debug.Log(cardFrontSprite.name);
        FlipManager.AllFlip();
        StartCoroutine(RotateOverTime(true));
    }

    // �ո�� �޸� �̹��� ����
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
            yield return new WaitForSeconds(1f); // 1�� ���� ��ٸ�

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
        // �������� �ʱ� ���·� ����
        canClick = true;
        Clicker = false;
        isCardFlipped = false;

        // Set the card's rotation around the Y-axis to 0
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        // ī�带 �޸����� ����
        SetCardSide(isCardFlipped);
    }

}
