using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName ="Scriptable Object")]
public class ItemData : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();
    void Start() 
    {
        itemList.Add(new Item(10001, "즉시 발동", "즉시발동하는 아이템", Item.ItemType.Immediate));
    }
}
