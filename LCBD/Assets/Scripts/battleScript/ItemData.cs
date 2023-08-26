using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName ="Scriptable Object")]
public class ItemData : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();
    void Start() 
    {
        itemList.Add(new Item(10001, "��� �ߵ�", "��ùߵ��ϴ� ������", Item.ItemType.Immediate));
    }
}
