using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelryPickUp : MonoBehaviour
{
    public GameObject slotitem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Inventory inven = collision.GetComponent<Inventory>();
            for (int i = 0; i < inven.Jewelryslots.Count; i++)
            {
                if (inven.Jewelryslots[i].isEmpty) //������â�� ��������� ������ �ֱ�
                {
                    Instantiate(slotitem, inven.Jewelryslots[i].slotObj.transform);
                    inven.Jewelryslots[i].isEmpty = false;
                    Destroy(this.gameObject);
                    break;
                }
            }
        }
    }
}
