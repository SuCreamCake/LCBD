using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopObjectInfo : MonoBehaviour
{
    private int price;
    private int itemNumber;
    private string itemName;
    private string rank;
    private int max_count;
    private Sprite item_sprite;

    public int Price
    {
        get { return price; }
        set { price = value; }
    }

    public int ItemNumber
    {
        get { return itemNumber; }
        set { itemNumber = value; }
    }

    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }

    public string Rank
    {
        get { return rank; }
        set { rank = value; }
    }

    public int MaxCount
    {
        get { return max_count; }
        set { max_count = value; }
    }

    public Sprite Item_Sprite
    {
        get { return item_sprite; }
        set { item_sprite = value; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
