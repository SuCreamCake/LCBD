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
        itemList.Add(new Item(10001, "��� �ߵ�", "��ùߵ��ϴ� ������", Item.ItemType.Immediate));
        itemList.Add(new Item(10002, "�غ� �ߵ�", "�غ�ߵ��ϴ� ������", Item.ItemType.Ready, 3, 10f));
        itemList.Add(new Item(10003, "��ô ������", "������ ������", Item.ItemType.Throw));
    }

   
}
