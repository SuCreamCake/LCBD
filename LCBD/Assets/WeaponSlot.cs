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

    private void Awake()
    {
        Transform child = transform.Find("Image"); // �ڽ� ������Ʈ�� �̸����� ã��
        if(child!=null)
            Weapon_image = child.GetComponent<Image>();
    }
    public WeaponSlot()
    {
        itemName = "";
        itemSprite = null;
        isUse = false;
    }
}



