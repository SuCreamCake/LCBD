using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick_Standing_ExplsionArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("BrokenWall"))
        {
            // 부서진 벽 감지 시 파괴.
            collision.gameObject.SetActive(false);
        }

        // 몬스터 감지시 데미지 입힘. (500f)
        if (collision.TryGetComponent<MonsterManager>(out var monsterManager))
        {
            monsterManager.TakeDamage(500f);
        }
    }
}
