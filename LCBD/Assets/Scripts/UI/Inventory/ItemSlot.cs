using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public string itemName; //�������� �̸�
    public Sprite itemSprite; //���Կ� ǥ�õ� �������� �̹���
    public bool isUse; //��������� ����
    public Image Item_image;
    public Item item; // ���Կ� �ִ� ������
    public Text itemCountText; // ������ ���� �ؽ�Ʈ
    

    private void Awake()
    {
        Transform child = transform.Find("Image"); // �ڽ� ������Ʈ�� �̸����� ã��
        if (child != null)
            Item_image = child.GetComponent<Image>();
    }
    public ItemSlot()
    {
        itemName = "";
        itemSprite = null;
        isUse = false;
    }

    public void UseItem() //������ ���
    {
        if (item != null)
        {
            item.Use_Effect(); // �������� ��� ȿ���� �ߵ�
            if(item.item_type == Item.Item_Type.Potion_Parts) //���Ƿ� �������̸� �ϳ� ����
            {
                item.now_Count -= 1;
                UpdateItemCountText(); // ������ ���� �ؽ�Ʈ ������Ʈ
            }
     
            // ������ ��� �� �߰����� ����, ���� ��� ������ ���ų� ������ ���� ������Ʈ
            if(item.now_Count <= 0) //������ ������ 1���� �۾�����
            {
                ClearSlot(); // ������ �ʱ�ȭ

            }
        }
    }

    public void ClearSlot() //������ ����� �ʱ�ȭ�� �������� ������������
    {
        itemName = "";
        itemSprite = null;
        Item_image.sprite = null;
        Item_image.enabled = false;
        isUse = false;
        item = null;
    }

    public void UpdateItemCountText() //Text Update
    {
        if (item != null)
        {
            itemCountText.text = item.now_Count.ToString();
        }
    }
}
