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
}
