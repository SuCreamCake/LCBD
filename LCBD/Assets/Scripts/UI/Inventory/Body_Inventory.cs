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
                BodySlots[i].UseItem(); //아이템을 사용
                BodySlots[i].isUse = true;
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
                BodySlots[i].BodyParts_image.enabled = true;  // 이미지를 활성화합니다.

                return true; // 아이템을 성공적으로 추가했음
            }
        }
        return false; // 인벤토리가 가득 참
    }

    public bool IsInventoryFull()
    {
        foreach (var slot in BodySlots) // 'slots'는 해당 인벤토리의 슬롯 리스트
        {
            if (!slot.isUse) // 슬롯이 비어있으면
            {
                return false; // 인벤토리가 가득 차지 않았음
            }
        }
        return true; // 모든 슬롯이 사용 중이면, 인벤토리가 가득 참
    }

    public string[] GetAllName()
    {
        string[] names = new string[BodySlots.Length];

        for (int i = 0; i < BodySlots.Length; i++)
        {
            names[i] = BodySlots[i].itemName;
        }
        Debug.Log("바디인벤토리 접근"); // 디버그 로그로 names 배열 출력
        return names;
    }
}
