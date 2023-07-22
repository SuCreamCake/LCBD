using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //원거리 공격 데미지
    public int damage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle") //여기 투사체 또는 적대 세력이 들어감)
            Destroy(gameObject);
        else if (collision.gameObject.tag == "Enemy")
            Destroy(gameObject);

    }
}
