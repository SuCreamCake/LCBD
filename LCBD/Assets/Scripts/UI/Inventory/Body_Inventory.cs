using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body_Inventory: MonoBehaviour
{
    public static Body_Inventory instance;
    public BodySlot[] BodySlots;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Update()
    {
        for (int i = 0; i < BodySlots.Length; i++)
        {
            if (BodySlots[i].isUse)
            {
                BodySlots[i].UseItem();
                BodySlots[i].isUse = false;
            }
        }
    }

    public bool AddItem(string itemName, Sprite itemSprite, Item itemObejct)
    {
        for (int i = 0; i < BodySlots.Length; i++)
        {
            if (!BodySlots[i].isUse)
            {
                BodySlots[i].itemName = itemName;
                BodySlots[i].itemSprite = itemSprite;
                BodySlots[i].isUse = true;
                BodySlots[i].item = itemObejct;
                // 여기에 Image 컴포넌트를 업데이트하는 코드를 추가합니다.
                BodySlots[i].BodyParts_image.sprite = itemSprite;
                BodySlots[i].BodyParts_image.enabled = true; // 이미지를 활성화합니다.

                return true; // 아이템을 성공적으로 추가했음
            }
        }
        return false; // 인벤토리가 가득 참
    }
}
