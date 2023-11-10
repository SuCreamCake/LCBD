using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject slotitem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Inventory inven = collision.GetComponent<Inventory>();
            for (int i = 0; i < inven.Itemslots.Count; i++)
            {
                if (inven.Itemslots[i].isEmpty) //������â�� ��������� ������ �ֱ�
                {
                    Instantiate(slotitem, inven.Itemslots[i].slotObj.transform);
                    inven.Itemslots[i].isEmpty = false;
                    Destroy(this.gameObject);
                    break;
                }
            }
        }
    }
}
