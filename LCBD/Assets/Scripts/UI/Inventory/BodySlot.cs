using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class BodySlot : MonoBehaviour
{
    public string itemName; //아이템의 이름
    public Sprite itemSprite; //슬롯에 표시될 아이템의 이미지
    public bool isUse; //사용중인지 여부
    public Image BodyParts_image;
    public Image Item_image;
    public Item item; // 슬롯에 있는 아이템

    public void UseItem() //아이템 사용
    {
        if (item != null)
        {
            item.Use_Effect(); // 아이템의 사용 효과를 발동
            // 아이템 사용 후 추가적인 로직, 예를 들어 아이템 제거나 아이템 슬롯 업데이트
            //ClearSlot(); // 슬롯을 초기화
        }
    }
    private void Awake()
    {
        Transform child = transform.Find("Image"); // 자식 오브젝트의 이름으로 찾기
        if (child != null)
            BodyParts_image = child.GetComponent<Image>();
    }
    public BodySlot()
    {
        itemName = "";
        itemSprite = null;
        isUse = false;
    }
    public void ClearSlot() //아이템 사용후 초기화나 갯수차감 로직구현예정
    {
        itemName = "";
        itemSprite = null;
        BodyParts_image.sprite = null;
        BodyParts_image.enabled = false;
        isUse = false;
        item = null;
    }
}
