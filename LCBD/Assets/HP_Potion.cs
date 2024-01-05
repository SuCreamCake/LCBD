using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Potion : Item
{
    public string itemName = "HP_Potion"; // 아이템의 이름
    public Sprite itemSprite; // 인벤토리에 표시할 아이템의 이미지
    public GameObject hpPotionPrefab; // HP_Potion 프리팹 참조
    public override void DestraoyAfterTime()
    {
        
    }

    public override void Use_Effect()
    {
        Debug.Log("체력 포션사용");
        GameObject findPlayer = GameObject.Find("Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.health += 10; // 체력을 10(한칸)회복시킴
                Debug.Log("체력 10회복! 한칸임.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ItemInventory.instance.AddItem(itemName, itemSprite, this))
            {
                //Destroy(gameObject); // 아이템을 씬에서 제거
                gameObject.SetActive(false);
            }
            else
            {
                // 인벤토리가 가득 차 있다면, 메시지를 표시하거나 다른 로직을 수행
            }
        }
    }
}
