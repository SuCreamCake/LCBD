using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Potion : MonoBehaviour
{
    public string itemName; // �������� �̸�
    public Sprite itemSprite; // �κ��丮�� ǥ���� �������� �̹���


    public void Use()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ItemInventory.instance.AddItem(itemName, itemSprite))
            {
                Destroy(gameObject); // �������� ������ ����
            }
            else
            {
                // �κ��丮�� ���� �� �ִٸ�, �޽����� ǥ���ϰų� �ٸ� ������ ����
            }
        }
    }
}