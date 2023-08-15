using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //원거리 공격 데미지
    public int damage;
    private Rigidbody2D bulletRigidbody2D;
    private float bulletSpeed = 10f;
    private float distanceTime;
    private bool isDistanceOver = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle") //여기 투사체 또는 적대 세력이 들어감)
            Destroy(gameObject);
        else if (collision.gameObject.tag == "monster")
        {
            Debug.Log("몬스터와 충돌!");
            Destroy(gameObject);
        }
    }
 
    private void Start()
    {
        bulletRigidbody2D = GetComponent<Rigidbody2D>();
        bulletRigidbody2D.velocity = bulletSpeed * transform.right;
        //자기 자신을 삭제한 메서드
        if (gameObject != null)
        {
            Destroy(gameObject, 10);
        }
    }
    private void Update()
    {
        distanceTime += Time.deltaTime;
        if((GameObject.Find("Player").GetComponent<Player>().crossroads < distanceTime*bulletSpeed)&& !isDistanceOver)
        {
            this.bulletRigidbody2D.gravityScale = 1f;
            isDistanceOver = true;
        }
        
    }
}
