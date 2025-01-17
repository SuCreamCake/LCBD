using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongDistance_Ms : MonoBehaviour
{
    //원거리 공격 데미지
    public float damage;
    private Rigidbody2D bulletRigidbody2D;
    public float bulletSpeed = 10f;
    private float distanceTime = 0f;
    private bool isDistanceOver = false;
    private int crossroads_Ms;
    private int attackPower_Ms;
    MonsterManager monsterManager;
    public LayerMask backMask; // 바닥 레이어

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((backMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            //여기 투사체 또는 적대 세력이 들어감)
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어 공격 성공");
            BattleManager battleManager = GameObject.FindWithTag("BattleManager").GetComponent<BattleManager>();
            battleManager.GetPlayerInfo(monsterManager.attackPower_Ms);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 부모 오브젝트에서 MonsterManager 스크립트의 컴포넌트를 가져옵니다.
        monsterManager = transform.parent.GetComponent<MonsterManager>();
        if (monsterManager != null)
        {
            crossroads_Ms = monsterManager.crossroads_Ms;
            attackPower_Ms = monsterManager.attackPower_Ms;
            // MonsterManager의 위치를 총알의 생성 위치로 설정합니다.
            transform.position = monsterManager.transform.position;
        }
        bulletRigidbody2D = GetComponent<Rigidbody2D>();
        // 플레이어의 위치를 가져옵니다.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();

            // 총알의 방향과 속도를 설정합니다.
            bulletRigidbody2D.velocity = direction * bulletSpeed;
        }

        // 공격 몬스터의 어택 데미지
        this.damage = attackPower_Ms;

        //자기 자신을 삭제한 메서드
        if (gameObject != null)
        {
            Destroy(gameObject, 10);
        }
    }
    private void Update()
    {
        distanceTime += Time.deltaTime;
        // 몬스터 사거리 넣기
        if ((crossroads_Ms < distanceTime * bulletSpeed) && !isDistanceOver)
        {
            this.bulletRigidbody2D.gravityScale = 1f;
            isDistanceOver = true;
        }

    }

}
