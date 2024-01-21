using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public static ItemInventory instance;
    public ItemSlot[] Itemslots;
    public int selectedItemIndex = -1; // ���õ� �������� �ε���

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
                Debug.Log("���� ������(���Ƿ�)�� ���ͼ� 1 ����");
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
                // ���⿡ Image ������Ʈ�� ������Ʈ�ϴ� �ڵ带 �߰��մϴ�.
                Itemslots[i].Item_image.sprite = itemSprite;
                Itemslots[i].Item_image.enabled = true; // �̹����� Ȱ��ȭ�մϴ�.
                Itemslots[i].item.now_Count += 1; //That's now_Count 1 Add
                Itemslots[i].UpdateItemCountText(); // Text Update
                return true; // �������� ���������� �߰�����
            }
        }
        return false; // �κ��丮�� ���� ��
    }


    public void UseSelectedItem() //������ ���
    {
        if (selectedItemIndex >= 0 && selectedItemIndex < Itemslots.Length && Itemslots[selectedItemIndex].isUse)
        {
            Itemslots[selectedItemIndex].UseItem(); // ���õ� ������ ���
            // �߰����� ����, ���� ��� ������ ���� �ʱ�ȭ
        }
    }
}
