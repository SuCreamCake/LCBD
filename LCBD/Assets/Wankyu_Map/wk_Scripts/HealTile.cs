using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어(태그)와 충돌 시.
        if (collision.CompareTag("Player"))
        {
            Player_Buff_Debuff.SetHealthRegeneration(true);       // 버프 '체력 재생' 켜기.
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 플레이어(태그)와 충돌 시.
        if (collision.CompareTag("Player"))
        {
            Player_Buff_Debuff.SetHealthRegeneration(true);       // 버프 '체력 재생' 켜기.
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 플레이어(태그)와 충돌 시.
        if (collision.CompareTag("Player"))
        {
            Player_Buff_Debuff.SetHealthRegeneration(false);       // 버프 '체력 재생' 끄기.
        }
    }
}
