using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Potion : Item
{
    public string itemName = "HP_Potion"; // �������� �̸�
    public Sprite itemSprite; // �κ��丮�� ǥ���� �������� �̹���
    public GameObject hpPotionPrefab; // HP_Potion ������ ����
    public override void DestraoyAfterTime()
    {
        
    }

    public override void Use_Effect()
    {
        Debug.Log("ü�� ���ǻ��");
        GameObject findPlayer = GameObject.Find("Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.health += 10; // ü���� 10(��ĭ)ȸ����Ŵ
                Debug.Log("ü�� 10ȸ��! ��ĭ��.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ItemInventory.instance.AddItem(itemName, itemSprite, this))
            {
                //Destroy(gameObject); // �������� ������ ����
                gameObject.SetActive(false);
            }
            else
            {
                // �κ��丮�� ���� �� �ִٸ�, �޽����� ǥ���ϰų� �ٸ� ������ ����
            }
        }
    }
}
