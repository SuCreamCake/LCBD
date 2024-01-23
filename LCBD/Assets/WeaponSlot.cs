using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class WeaponSlot : MonoBehaviour
{
    public string itemName; //�������� �̸�
    public Sprite itemSprite; //���Կ� ǥ�õ� �������� �̹���
    public bool isUse; //��������� ����
    public Image Weapon_image;
    public Item item; // ���Կ� �ִ� ������

    private void Awake()
    {
        Transform child = transform.Find("Image"); // �ڽ� ������Ʈ�� �̸����� ã��
        if(child!=null)
            Weapon_image = child.GetComponent<Image>();
    }

    public void UseItem() //������ ���
    {
        if (item != null)
        {
            item.Use_Effect(); // �������� ��� ȿ���� �ߵ�
            // ������ ��� �� �߰����� ����, ���� ��� ������ ���ų� ������ ���� ������Ʈ
            //ClearSlot(); // ������ �ʱ�ȭ
        }
    }
    public WeaponSlot()
    {
        itemName = "";
        itemSprite = null;
        isUse = false;
    }

    public void ClearSlot() //������ ����� �ʱ�ȭ�� �������� ������������
    {
        itemName = "";
        itemSprite = null;
        Weapon_image.sprite = null;
        Weapon_image.enabled = false;
        isUse = false;
        item = null;
    }
}



