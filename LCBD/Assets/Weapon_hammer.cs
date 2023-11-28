using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_hammer : MonoBehaviour
{
    public string itemName; // 아이템의 이름
    public Sprite itemSprite; // 인벤토리에 표시할 아이템의 이미지

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (WeaponInventory.instance.AddItem(itemName, itemSprite))
            {
                Destroy(gameObject); // 아이템을 씬에서 제거
            }
            else
            {
                // 인벤토리가 가득 차 있다면, 메시지를 표시하거나 다른 로직을 수행
            }
        }
    }

}
