using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public static WeaponInventory instance;
    public WeaponSlot[] Weaponslots;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    public bool AddItem(string itemName, Sprite itemSprite, Item itemObejct)
    {
        for (int i = 0; i < Weaponslots.Length; i++)
        {
            if (!Weaponslots[i].isUse)
            {
                Weaponslots[i].itemName = itemName;
                Weaponslots[i].itemSprite = itemSprite;
                Weaponslots[i].isUse = true;
                Weaponslots[i].item = itemObejct;
                // 여기에 Image 컴포넌트를 업데이트하는 코드를 추가합니다.
                Weaponslots[i].Weapon_image.sprite = itemSprite;
                Weaponslots[i].Weapon_image.enabled = true; // 이미지를 활성화합니다.

                return true; // 아이템을 성공적으로 추가했음
            }
        }
        return false; // 인벤토리가 가득 참
    }

    public string[] GetAllName()
    {
        string[] names = new string[Weaponslots.Length];

        for (int i = 0; i < Weaponslots.Length; i++)
        {
            names[i] = Weaponslots[i].itemName;
        }

        return names;
    }
}
