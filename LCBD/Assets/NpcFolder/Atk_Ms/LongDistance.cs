using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongDistance : MonoBehaviour
{
    //원거리 공격 데미지
    public float damage;
    private Rigidbody2D bulletRigidbody2D;
    private float bulletSpeed = 17f;
    private float distanceTime;
    private bool isDistanceOver = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        //레이어마스크로 변경//
        if (collision.gameObject.tag == "background") //여기 투사체 또는 적대 세력이 들어감)
            Destroy(gameObject);
        else if (collision.gameObject.tag == "Player")
        {
            Debug.Log("플레이어 공격 성공");
            collision.gameObject.GetComponent<MonsterManager>().TakeDamage((int)damage);
            Destroy(gameObject);
        }
    }

    private void Start()
    {   
        bulletRigidbody2D = GetComponent<Rigidbody2D>();
        bulletRigidbody2D.velocity = bulletSpeed * transform.right;
        SetDamage();
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
        if ((GameObject.Find("Player").GetComponent<Player>().crossroads * 2 < distanceTime * bulletSpeed) && !isDistanceOver)
        {
            this.bulletRigidbody2D.gravityScale = 1f;
            isDistanceOver = true;
        }

    }
    public void SetDamage()
    {
        // 공격 몬스터의 어택 데미지
        this.damage = GameObject.Find("Player").GetComponent<Player>().attackPower;

    }

}
