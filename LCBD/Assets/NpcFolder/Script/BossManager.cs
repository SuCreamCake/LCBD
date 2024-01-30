using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    PlayerTracking PlayerTracking;
    FieldOfView FieldOfView;
    EnemyMove EnemyMove;
    SpriteRenderer SpriteRenderer;
    Animator animator;
    BossAtk monsterAtk;

    bool once = false; // 한 번만
    bool findPlayer = false; // 플레이어 발견 여부

    public int MonsterID; // 몬스터 ID
    public bool pattern = false; // 몬스터 패턴 여부
    public bool Relationship = false;
    public float maxHealth_Ms; // 최대 체력
    public float health_Ms; // 현재 체력
    public int attackPower_Ms; // 공격력
    public int defense_Ms; // 방어력
    public float speed_Ms; // 이동 속도
    public int maxtenacity_Ms; // 총 강인도
    public int tenacity_Ms; // 현재 강인도
    public float attackSpeed_Ms; // 공격 속도
    //public int range_Ms;
    public int crossroads_Ms; // 사거리

    public Transform player; // Inspector에서 플레이어 오브젝트를 할당해야 합니다.
    public float trackingRange = 5f; // 플레이어를 따라다니는 범위입니다.
    public float movementSpeed = 2f; // 다가오는 속도

    private float timer = 0f;
    private bool isPaused = false;

    private SpriteRenderer spriteRenderer;

    //생존 여부
    bool alive;

    void Awake()
    {
        PlayerTracking = GetComponent<PlayerTracking>();
        EnemyMove = GetComponent<EnemyMove>();
        FieldOfView = GetComponent<FieldOfView>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        monsterAtk = GetComponent<BossAtk>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // "Player" 태그를 가진 오브젝트를 찾아서 playerTransform에 할당
        player = GameObject.FindGameObjectWithTag("Player").transform;
        alive = true;
    }

    private void Update()
    {
        if (player != null)
        {
            // 플레이어의 위치를 기준으로 SpriteRenderer의 xFlip을 설정합니다.
            if (player.position.y < transform.position.y)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }

            // 플레이어와의 거리를 계산합니다.
            float distance = Vector3.Distance(transform.position, player.position);

            // 플레이어가 일정 범위 안에 있다면 따라다니도록 합니다.
            if (distance <= trackingRange)
            {
                // 플레이어와의 거리가 0.5 이하일 때는 가까이 가지 않도록 합니다.
                if (distance > 0.5f)
                {
                    // 타이머를 증가시킵니다.
                    timer += Time.deltaTime;

                    // 5초마다 1초의 정지 타임을 추가합니다.
                    if (timer >= 5f)
                    {
                        // 1초 동안 정지
                        StartCoroutine(ResumeTimeAfterDelay(1f));
                        timer = 0f;
                    }
                    if (!isPaused)
                    {
                        // 플레이어를 따라다니는 로직을 구현합니다.
                        // 예를 들어, 플레이어 방향으로 이동하는 방식으로 구현할 수 있습니다.
                        Vector3 direction = (player.position - transform.position).normalized;
                        transform.position += direction * movementSpeed * Time.deltaTime;
                    }
                }
            }
        }

        if (tenacity_Ms <= 0)
        {
            //StartCoroutine(ten());
        }
        if (health_Ms <= 0)
        {
            // "Dead" 변수를 설정
            animator.SetBool("Dead", true);
            isPaused = true;
            monsterAtk.enabled = false;
            //PlayerTracking.enabled = false;
            alive = false;
        }
    }

    // 일정 시간 후에 시간을 다시 재개하는 코루틴
    private IEnumerator ResumeTimeAfterDelay(float delay)
    {
        isPaused = true;
        yield return new WaitForSeconds(delay);
        isPaused = false;
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(OnDamage(damage));
    }
    IEnumerator OnDamage(float damage)
    {
        SpriteRenderer.material.color = Color.red;
        Debug.Log("EnemyHit��ũ��Ʈ 28��° �� Damage��" + damage);
        health_Ms -= damage;
        tenacity_Ms -= (int)damage;
        yield return new WaitForSeconds(0.03f);
        SpriteRenderer.material.color = Color.white;
    }

    public bool getAlive()
    {
        return alive;
    }
}
