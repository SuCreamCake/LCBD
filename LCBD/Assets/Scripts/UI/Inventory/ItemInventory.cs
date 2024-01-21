using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public static ItemInventory instance;
    public ItemSlot[] Itemslots;
    public int selectedItemIndex = -1; // 선택된 아이템의 인덱스

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    public bool AddItem(string itemName, Sprite itemSprite, Item itemObject)
    {
        for (int i = 0; i < Itemslots.Length; i++) {
            if (Itemslots[i].isUse && Itemslots[i].item.item_number == itemObject.item_number) //If you have an item that's the same as one in the slots, add 1 to nowCount
            {
                Debug.Log("같은 아이템(포션류)이 들어와서 1 증가");
                Itemslots[i].item.now_Count = Mathf.Min(Itemslots[i].item.now_Count + 1, Itemslots[i].item.max_count); // nowCount 1 add.
                Itemslots[i].UpdateItemCountText(); // Text Update
                return true;
            }
        }
        for (int i = 0; i < Itemslots.Length; i++)
        {
            if (!Itemslots[i].isUse)
            {
                Itemslots[i].itemName = itemName;
                Itemslots[i].itemSprite = itemSprite;
                Itemslots[i].isUse = true;
                Itemslots[i].item = itemObject;
                // 여기에 Image 컴포넌트를 업데이트하는 코드를 추가합니다.
                Itemslots[i].Item_image.sprite = itemSprite;
                Itemslots[i].Item_image.enabled = true; // 이미지를 활성화합니다.
                Itemslots[i].item.now_Count += 1; //That's now_Count 1 Add
                Itemslots[i].UpdateItemCountText(); // Text Update
                return true; // 아이템을 성공적으로 추가했음
            }
        }
        return false; // 인벤토리가 가득 참
    }


    public void UseSelectedItem() //아이템 사용
    {
        if (selectedItemIndex >= 0 && selectedItemIndex < Itemslots.Length && Itemslots[selectedItemIndex].isUse)
        {
            Itemslots[selectedItemIndex].UseItem(); // 선택된 아이템 사용
            // 추가적인 로직, 예를 들어 아이템 슬롯 초기화
        }
    }
}
