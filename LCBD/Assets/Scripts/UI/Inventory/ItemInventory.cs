using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public static ItemInventory instance;
    public ItemSlot[] Itemlots;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    public bool AddItem(string itemName, Sprite itemSprite)
    {
        for (int i = 0; i < Itemlots.Length; i++)
        {
            if (!Itemlots[i].isUse)
            {
                Itemlots[i].itemName = itemName;
                Itemlots[i].itemSprite = itemSprite;
                Itemlots[i].isUse = true;
                // ���⿡ Image ������Ʈ�� ������Ʈ�ϴ� �ڵ带 �߰��մϴ�.
                Itemlots[i].Item_image.sprite = itemSprite;
                Itemlots[i].Item_image.enabled = true; // �̹����� Ȱ��ȭ�մϴ�.
                return true; // �������� ���������� �߰�����
            }
        }
        return false; // �κ��丮�� ���� ��
    }
}
