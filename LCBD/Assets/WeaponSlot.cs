using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class WeaponSlot : MonoBehaviour
{
    public string itemName; //아이템의 이름
    public Sprite itemSprite; //슬롯에 표시될 아이템의 이미지
    public bool isUse; //사용중인지 여부
    public Image Weapon_image;

    private void Awake()
    {
        Transform child = transform.Find("Image"); // 자식 오브젝트의 이름으로 찾기
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



