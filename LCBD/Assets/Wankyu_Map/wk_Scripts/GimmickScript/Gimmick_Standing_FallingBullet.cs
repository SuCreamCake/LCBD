using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick_Standing_FallingBullet : MonoBehaviour
{
    private float speed;
    private Rigidbody2D bulletRigid;
    private Player player;
    private BuffDebuffManager debuffManager;

    private void Start()
    {
        speed = UnityEngine.Random.Range(1f, 3f);   // 떨어지는 총알 속도 1.0f~2.99f
        bulletRigid = GetComponent<Rigidbody2D>();
        bulletRigid.velocity = transform.right * speed;

        debuffManager = FindObjectOfType<BuffDebuffManager>();  // 플레이어한테 넣을 것임.

        Destroy(gameObject, 12.5f / speed);    // 12.5f / speed 후 파괴. // 시간 = 12.5칸 / 속도. => (12.5칸 도달시 파괴).
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어(태그)와 충돌 시.
        if (collision.CompareTag("Player"))
        {
            player = FindObjectOfType<Player>();
            player.health -= 2; // 2의 고정 데미지.

            BuffDebuffManager.SetWeightOfLife_duration(2f); // '디버프: 삶의 무게' 기본 지속시간 2초 부여.
            Player_Buff_Debuff.SetWeightOfLife(true);       // 디버프 켜기.

            Destroy(gameObject);    // 오브젝트 파괴.
        }
    }

}
