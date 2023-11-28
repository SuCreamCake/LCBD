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

    public bool AddItem(string itemName, Sprite itemSprite)
    {
        for (int i = 0; i < Weaponslots.Length; i++)
        {
            if (!Weaponslots[i].isUse)
            {
                Weaponslots[i].itemName = itemName;
                Weaponslots[i].itemSprite = itemSprite;
                Weaponslots[i].isUse = true;
                // ���⿡ Image ������Ʈ�� ������Ʈ�ϴ� �ڵ带 �߰��մϴ�.
                Weaponslots[i].Weapon_image.sprite = itemSprite;
                Weaponslots[i].Weapon_image.enabled = true; // �̹����� Ȱ��ȭ�մϴ�.
                return true; // �������� ���������� �߰�����
            }
        }
        return false; // �κ��丮�� ���� ��
    }
}
