using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class JewelrySlot : MonoBehaviour
{
    public string itemName; //�������� �̸�
    public Sprite itemSprite; //���Կ� ǥ�õ� �������� �̹���
    public bool isUse; //��������� ����
    public Image Jewelry_image;
    //public Image Item_image;
    public Item item; // ���Կ� �ִ� ������

    public void UseItem() //������ ���
    {
        if (item != null)
        {
            item.Use_Effect(); // �������� ��� ȿ���� �ߵ�
            // ������ ��� �� �߰����� ����, ���� ��� ������ ���ų� ������ ���� ������Ʈ
            ClearSlot(); // ������ �ʱ�ȭ
        }
    }
    private void Awake()
    {
        Transform child = transform.Find("Image"); // �ڽ� ������Ʈ�� �̸����� ã��
        if (child != null)
            Jewelry_image = child.GetComponent<Image>();
    }
    public JewelrySlot()
    {
        itemName = "";
        itemSprite = null;
        isUse = false;
    }
    private void ClearSlot() //������ ����� �ʱ�ȭ�� �������� ������������
    {
        itemName = "";
        itemSprite = null;
        Jewelry_image.sprite = null;
        Jewelry_image.enabled = false;
        isUse = false;
        item = null;
    }
}
