using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAtk : MonoBehaviour
{
    public float attackSpeed = 1.0f;  // 공격 속도 조절
    public GameObject attackColliderPrefab;  // 콜라이더 프리팹을 할당하세요.
    public float attackRange = 1.0f; // 사거리

    private bool isAttacking = false;
    private float timeSinceLastAttack = 0f;
    private bool isFlipped; // 좌우 확인

    private SpriteRenderer spriteRenderer;
    private MonsterManager MonsterManager;

    // 콜라이더 크기
    float colliderWidth = 1.0f; // 원하는 가로 크기
    float colliderHeight = 1.0f; // 원하는 세로 크기

    bool AtkCall;

    Transform player;

    void Start()
    {
        // 현재 오브젝트의 SpriteRenderer 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();
        MonsterManager = GetComponent<MonsterManager>();

        // SpriteRenderer가 없을 경우 에러 처리
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer 컴포넌트를 찾을 수 없습니다.");
        }
        if (MonsterManager != null)
        {
            attackSpeed = MonsterManager.attackSpeed_Ms;
            attackRange = MonsterManager.crossroads_Ms;
        }
        AtkCall = false;
        // "Player" 태그를 가진 오브젝트를 찾아서 playerTransform에 할당
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update 함수는 프레임마다 호출됩니다.
    void Update()
    {
        // 플레이어와 몬스터 사이의 거리 계산
        float atk = Vector2.Distance(transform.position, player.position);

        // Z 키를 눌렀을 때 공격 실행
        if (atk <= attackRange)
        {
            // 만약 공격 속도에 만족한다면
            if (Time.time - timeSinceLastAttack > 1 / attackSpeed)
            {
                Attack();
                timeSinceLastAttack = Time.time;
                Invoke("OutAtkAni", 0.3f);
            }
        }
    }


    // 공격 함수
    public void Attack()
    {
        Debug.Log("Monster Attack!");

        if (!AtkCall)
        {
            MonsterManager.SetFind(true);
            Invoke("OutAtkAni", 0.3f);
            AtkCall = true;
            Invoke("LastAtk", attackSpeed);

            // flipX 값 확인
            isFlipped = spriteRenderer.flipX;
            // 좌우 방향에 따라 공격 방향을 조절
            if (isFlipped)
            {
                // 오른쪽으로 공격
                SpawnAttackCollider(transform.position + new Vector3(1f, 0f, 0f));
            }
            else
            {
                // 왼쪽으로 공격
                SpawnAttackCollider(transform.position + new Vector3(-1f, 0f, 0f));
            }
        }
    }


    // 콜라이더를 생성하는 함수
    private void SpawnAttackCollider(Vector3 position)
    {
        GameObject attackCollider = Instantiate(attackColliderPrefab, position, Quaternion.identity);

        // 여기에서 필요한 설정을 추가하세요. 예를 들어, 콜라이더의 크기, 속도, 방향 등을 설정할 수 있습니다.
        // Set the monster as the parent of the attack collider
        attackCollider.transform.parent = transform;

        // 콜라이더 크기 조절 예시
        colliderWidth = 1.0f; // 원하는 가로 크기
        colliderHeight = 1.0f; // 원하는 세로 크기

        // BoxCollider2D가 있는 경우에만 크기 조절
        BoxCollider2D boxCollider = attackCollider.GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            // 콜라이더 크기 조절
            boxCollider.size = new Vector2(colliderWidth, colliderHeight);
        }

        // 일정 시간이 지난 후에 콜라이더를 파괴
        Destroy(attackCollider, 0.5f);
    }

    public void OutAtkAni()
    {
        MonsterManager.SetFind(false);
    }

    public void LastAtk()
    {
        AtkCall = false;
    }
}
