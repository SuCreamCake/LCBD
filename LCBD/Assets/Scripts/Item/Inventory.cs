using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<SlotData> Weaponslots = new List<SlotData>();
    public List<SlotData> Itemslots = new List<SlotData>();
    public List<SlotData> Jewelryslots = new List<SlotData>();
    private int WeaponMaxSlot = 2;
    private int ItemMaxSlot = 3;
    private int JewelryMaxSlot = 4;

    public GameObject WeaponSlotPrefab;
    public GameObject ItemSlotPrefab;
    public GameObject JewelrySlotPrefab;

    private void Start()
    {
        GameObject WeaponPanel = GameObject.Find("WeaponPanel");
        GameObject ItemPanel = GameObject.Find("ItemPanel");
        GameObject JewelryPanel = GameObject.Find("JewelryPanel");

        for (int i=0; i < WeaponMaxSlot; i++)
        {
            GameObject go = Instantiate(WeaponSlotPrefab, WeaponPanel.transform, false);
            go.name = "slot_" + i;
            SlotData slot = new SlotData();
            slot.isEmpty = true;
            slot.slotObj = go;
            Weaponslots.Add(slot);
        }
        for (int i = 0; i < ItemMaxSlot; i++)
        {
            GameObject go = Instantiate(ItemSlotPrefab, ItemPanel.transform, false);
            go.name = "slot_" + i;
            SlotData slot = new SlotData();
            slot.isEmpty = true;
            slot.slotObj = go;
            Itemslots.Add(slot);
        }
        for (int i = 0; i < JewelryMaxSlot; i++)
        {
            GameObject go = Instantiate(JewelrySlotPrefab, JewelryPanel.transform, false);
            go.name = "slot_" + i;
            SlotData slot = new SlotData();
            slot.isEmpty = true;
            slot.slotObj = go;
            Jewelryslots.Add(slot);
        }

    }
}
