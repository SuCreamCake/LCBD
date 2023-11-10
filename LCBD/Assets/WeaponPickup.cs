using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject slotitem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Inventory inven = collision.GetComponent<Inventory>();
            for (int i = 0; i < inven.Weaponslots.Count; i++)
            {
                if (inven.Weaponslots[i].isEmpty) //아이템창이 비어있으면 아이템 넣기
                {
                    Instantiate(slotitem, inven.Weaponslots[i].slotObj.transform);
                    inven.Weaponslots[i].isEmpty = false;
                    Destroy(this.gameObject);
                    break;
                }
            }
        }
    }
}