using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDatabaseManager : MonoBehaviour
{
    public static ItemDatabaseManager instance;
    public List<Item> itemList = new List<Item>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    void Start() 
    {
        itemList.Add(new Item(10001, "즉시 발동", "즉시발동하는 아이템", Item.ItemType.Immediate));
        itemList.Add(new Item(10002, "준비 발동", "준비발동하는 아이템", Item.ItemType.Ready));
        itemList.Add(new Item(10003, "투척 아이템", "아이템 던지기", Item.ItemType.Throw));
    }

   
}
