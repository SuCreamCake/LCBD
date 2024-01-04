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
}
