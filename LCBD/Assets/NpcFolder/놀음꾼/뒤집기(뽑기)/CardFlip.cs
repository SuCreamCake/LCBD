using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using UnityEditor;

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
    private float rotationDuration = 1.5f;

    private Quaternion startRotation;
    private Quaternion targetQuaternion;
    GameObject RewardObject;
    Sprite sprite = null;

    void Awake()
    {
        imageComponent = GetComponent<Image>();
        SetCardSide(isCardFlipped);

        startRotation = transform.rotation;
        targetQuaternion = Quaternion.Euler(0f, targetRotation, 0f);
    }

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

        FlipManager.Reward(RewardObject);
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
        FlipManager.RotationEvent();
    }

    public void ResetCard(GameObject matchingPrefab)
    {
        imageComponent = GetComponent<Image>();
        // 클릭 가능상태로 초기화
        canClickOff();
        Clicker = false;
        isCardFlipped = false;

        // Set the card's rotation around the Y-axis to 0
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        sprite = null;
        // 카드를 뒷면으로 변경
        SetCardSide(isCardFlipped);
        ChangeCard(matchingPrefab);
    }

    public void ChangeCard(GameObject matchingPrefab)
    {
        // 프리팹 컴포넌트에서 스크립트를 가져옵니다.
        Item script = matchingPrefab.GetComponent<Item>();
        sprite = script.item_sprite;
        cardFrontSprite = sprite;
        RewardObject = matchingPrefab;

        //string[] filePaths = Directory.GetFiles("Assets/Scripts/Item", "*" + scriptName, SearchOption.AllDirectories);
        //script = null;
        //if (filePaths.Length > 0)
        //{
        //    string scriptPath = filePaths[0];
        //    script = AssetDatabase.LoadAssetAtPath<MonoScript>(scriptPath);
        //}

        //// 스크립트를 자식 오브젝트에 추가합니다.
        //if (script != null)
        //{
        //    gameObject.AddComponent(script.GetClass());
        //    // Body_Parts_Item, Hand_Parts_Item, Potion_Parts_Item을 미리 선언합니다.
        //    Body_Parts_Item Body_Parts_Item = null;
        //    Hand_Parts_Item Hand_Parts_Item = null;
        //    Potion_Parts_Item Potion_Parts_Item = null;
        //    string itemName;

        //    Body_Parts_Item = gameObject.GetComponent<Body_Parts_Item>();
        //    if (Body_Parts_Item == null)
        //    {
        //        Hand_Parts_Item = gameObject.GetComponent<Hand_Parts_Item>();
        //        if (Hand_Parts_Item == null)
        //        {
        //            Potion_Parts_Item = gameObject.GetComponent<Potion_Parts_Item>();
        //            // 필드에 접근하여 값 가져오기
        //            itemName = Potion_Parts_Item.item_Name;
        //            //sprite도 추가 필요
        //            //sprite = Potion_Parts_Item.item_sprite;
        //            //cardBackSprite = sprite;
        //            //imageComponent.sprite = cardBackSprite;
        //            //
        //            if (itemName.Equals("SanSam"))
        //            {
        //                sprite = Potion_Parts_Item.item_sprite;
        //                cardFrontSprite = sprite;
        //            }
        //            Debug.Log("Item Name: " + itemName);
        //            // 스크립트 컴포넌트 삭제
        //            Destroy(Potion_Parts_Item);
        //        }
        //        else
        //        {
        //            itemName = Hand_Parts_Item.item_Name;
        //            //sprite도 추가 필요
        //            if (itemName.Equals("SanSam"))
        //            {
        //                sprite = Potion_Parts_Item.item_sprite;
        //                cardFrontSprite = sprite;
        //            }
        //            Debug.Log("Item Name: " + itemName);
        //            // 스크립트 컴포넌트 삭제
        //            Destroy(Hand_Parts_Item);
        //        }
        //    }
        //    else
        //    {
        //        itemName = Body_Parts_Item.item_Name;
        //        //sprite도 추가 필요, detail
        //        if (itemName.Equals("SanSam"))
        //        {
        //            sprite = Potion_Parts_Item.item_sprite;
        //            cardFrontSprite = sprite;
        //        }
        //        Debug.Log("Item Name: " + itemName);
        //        Destroy(Body_Parts_Item);
        //    }
        //}
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
