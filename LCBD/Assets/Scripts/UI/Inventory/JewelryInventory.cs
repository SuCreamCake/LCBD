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
                // ���⿡ Image ������Ʈ�� ������Ʈ�ϴ� �ڵ带 �߰��մϴ�.
                Jewelrylots[i].Jewelry_image.sprite = itemSprite;
                Jewelrylots[i].Jewelry_image.enabled = true; // �̹����� Ȱ��ȭ�մϴ�.
                return true; // �������� ���������� �߰�����
            }
        }
        return false; // �κ��丮�� ���� ��
    }
}
