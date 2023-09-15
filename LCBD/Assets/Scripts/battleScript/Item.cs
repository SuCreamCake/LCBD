using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Item
{
    public int itemID; //������ ID�� �ߺ� �Ұ�
    public string itemName; // ������ �̸� �ߺ�����
    public string itemDescription;  //������ ����
    public int itemCount;  //���� ����
    public Sprite itemIcon;  //�������� ������
    public ItemType itemType;
    public float waitingTime;   //������ ���ð�

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
        //itemIcon = Resources.Load("ItemIcon/"+itemID.ToString(),typeof(Sprite)) as Sprite;
    }

   
}
