using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelryInventory: MonoBehaviour
{
    public static JewelryInventory instance;
    public JewelrySlot[] Jewelrylots;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    public bool AddItem(string itemName, Sprite itemSprite)
    {
        for (int i = 0; i < Jewelrylots.Length; i++)
        {
            if (!Jewelrylots[i].isUse)
            {
                Jewelrylots[i].itemName = itemName;
                Jewelrylots[i].itemSprite = itemSprite;
                Jewelrylots[i].isUse = true;
                // 여기에 Image 컴포넌트를 업데이트하는 코드를 추가합니다.
                Jewelrylots[i].Jewelry_image.sprite = itemSprite;
                Jewelrylots[i].Jewelry_image.enabled = true; // 이미지를 활성화합니다.
                return true; // 아이템을 성공적으로 추가했음
            }
        }
        return false; // 인벤토리가 가득 참
    }
}
