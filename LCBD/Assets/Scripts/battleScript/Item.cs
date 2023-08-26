using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public int itemID; //아이템 ID값 중복 불가
    public string itemName; // 아이템 이름 중복간으
    public string itemDescription;  //아이템 설명
    public int itemCount;  //소지 개수
    public Sprite itemIcon;  //아이템의 아이콘
    public ItemType itemType;
    public float waitingTime;   //아이템 대기시간

    public enum ItemType
    {
        Immediate, 
        Ready, 
        Toggle, 
        Throw
    }

    public Item(int _itemID, string _itemName, string _itemDes,ItemType _itemType, int _itemCount = 1, float _waitingTime = 0)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemType = _itemType;
        itemCount = _itemCount;
        waitingTime = _waitingTime;
        itemIcon = Resources.Load("ItemIcon/"+itemID.ToString(),typeof(Sprite)) as Sprite;

    }
}
