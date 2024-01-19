using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public string itemName; //아이템의 이름
    public Sprite itemSprite; //슬롯에 표시될 아이템의 이미지
    public bool isUse; //사용중인지 여부
    public Image Item_image;
    public Item item; // 슬롯에 있는 아이템

    private void Awake()
    {
        Transform child = transform.Find("Image"); // 자식 오브젝트의 이름으로 찾기
        if (child != null)
            Item_image = child.GetComponent<Image>();
    }
    public ItemSlot()
    {
        itemName = "";
        itemSprite = null;
        isUse = false;
    }

    public void UseItem() //아이템 사용
    {
        if (item != null)
        {
            item.Use_Effect(); // 아이템의 사용 효과를 발동
            if(item.item_type == Item.Item_Type.Potion_Parts) //포션류 아이템이면 하나 제거
            {
                item.now_Count -= 1;
            }
     
            // 아이템 사용 후 추가적인 로직, 예를 들어 아이템 제거나 아이템 슬롯 업데이트
            if(item.now_Count < 1) //아이템 갯수가 1보다 작아지면
            {
                ClearSlot(); // 슬롯을 초기화

            }
        }
    }

    private void ClearSlot() //아이템 사용후 초기화나 갯수차감 로직구현예정
    {
        itemName = "";
        itemSprite = null;
        Item_image.sprite = null;
        Item_image.enabled = false;
        isUse = false;
        item = null;
    }
}
