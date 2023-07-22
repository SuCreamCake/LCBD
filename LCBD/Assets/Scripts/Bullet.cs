using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //원거리 공격 데미지
    public int damage;
    private Rigidbody2D bulletRigidbody2D;
    private float bulletSpeed = 10f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy") //여기 투사체 또는 적대 세력이 들어감)
            Destroy(gameObject);
        else if (collision.gameObject.tag == "Wall")
            Destroy(gameObject);
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
}
