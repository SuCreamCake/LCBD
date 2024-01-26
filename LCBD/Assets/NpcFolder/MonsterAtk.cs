using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAtk : MonoBehaviour
{
    public float attackSpeed = 1.0f;  // 공격 속도 조절
    public GameObject attackColliderPrefab;  // 콜라이더 프리팹을 할당하세요.
    public float attackRange = 1.0f; // 사거리

    //private bool isAttacking = false;
    private float timeSinceLastAttack = 0f;
    private bool isFlipped; // 좌우 확인

    private SpriteRenderer spriteRenderer;
    private MonsterManager MonsterManager;
    private PlayerTracking PlayerTracking;

    // 콜라이더 크기
    float colliderWidth = 1.0f; // 원하는 가로 크기
    float colliderHeight = 1.0f; // 원하는 세로 크기

    bool AtkCall;

    public bool close = false; //근거리 원거리 판별
    Transform player;

    private bool See; //플레이어 본다.
    private bool FirstAtk = true;

    void Start()
    {
        // 현재 오브젝트의 SpriteRenderer 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();
        MonsterManager = GetComponent<MonsterManager>();
        PlayerTracking = GetComponent<PlayerTracking>();


        // SpriteRenderer가 없을 경우 에러 처리
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer 컴포넌트를 찾을 수 없습니다.");
        }
        if (MonsterManager != null)
        {
            attackSpeed = MonsterManager.attackSpeed_Ms;
            attackRange = MonsterManager.crossroads_Ms;
            if (close)
            {
                attackRange = Mathf.Round(attackRange * 0.1f * 10) / 10f;
            }
        }
        AtkCall = false;
        // "Player" 태그를 가진 오브젝트를 찾아서 playerTransform에 할당
        player = GameObject.FindGameObjectWithTag("Player").transform;


    }

    // Update 함수는 프레임마다 호출됩니다.
    void Update()
    {
        if (See)
        {
            // 플레이어와 몬스터 사이의 거리 계산
            float atk = Vector2.Distance(transform.position, player.position);
            
            if (atk <= attackRange)
            {
                if (FirstAtk)
                {
                    timeSinceLastAttack = Time.time;
                    FirstAtk = false;
                } else
                {
                    // 사거리에 오면 PlayerTracking에게 쏜다.
                    PlayerTracking.AtkTrue();
                    if (Time.time - timeSinceLastAttack > 1 / attackSpeed)
                    {
                        Attack();
                        timeSinceLastAttack = Time.time;
                    }
                }
            } else
            {
                FirstAtk = true;
            }
        }
        else
        {
            AtkCall = false;
            PlayerTracking.AtkFalse();
            FirstAtk = true;
        }
    }


    // 공격 함수
    public void Attack()
    {
        Debug.Log("Monster Attack!");

        if (!AtkCall)
        {
            MonsterManager.SetFind(true);
            Invoke("OutAtkAni", 1 / attackSpeed);
            AtkCall = true;
            Invoke("LastAtk", 1 / attackSpeed);

            // flipX 값 확인
            isFlipped = spriteRenderer.flipX;
            // 좌우 방향에 따라 공격 방향을 조절
            if (isFlipped)
            {
                // 오른쪽으로 공격
                SpawnAttackCollider(transform.position + new Vector3(attackRange / 2.0f, 0f, 0f));
            }
            else
            {
                // 왼쪽으로 공격
                SpawnAttackCollider(transform.position + new Vector3(attackRange / 2.0f * -1.0f, 0f, 0f));
            }
        }
    }

    public void OutAtkAni()
    {
        MonsterManager.SetFind(false);
    }

    public void LastAtk()
    {
        AtkCall = false;
        PlayerTracking.AtkFalse();
    }

    public void FindPlayer()
    {
        See = true;
    }
    public void NotFindPlayer()
    {
        See = false;
    }

    // 콜라이더를 생성하는 함수
    private void SpawnAttackCollider(Vector3 position)
    {
        GameObject attackCollider = Instantiate(attackColliderPrefab, position, Quaternion.identity);

        // 여기에서 필요한 설정을 추가하세요. 예를 들어, 콜라이더의 크기, 속도, 방향 등을 설정할 수 있습니다.
        // Set the monster as the parent of the attack collider
        attackCollider.transform.parent = transform;

        // 콜라이더 크기 조절 예시
        colliderWidth = attackRange; // 원하는 가로 크기
        colliderHeight = 1.0f; // 원하는 세로 크기
        // BoxCollider2D가 있는 경우에만 크기 조절
        BoxCollider2D boxCollider = attackCollider.GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            // 콜라이더 크기 조절
            boxCollider.size = new Vector2(colliderWidth, colliderHeight);
        }
        //string tag = attackCollider.tag;
        if (close)
        {
            // 일정 시간이 지난 후에 콜라이더를 파괴
            Destroy(attackCollider, 0.5f/attackSpeed);
            attackCollider.AddComponent<CollisionHandler>().Initialize(MonsterManager.attackPower_Ms); // attackCollider에 OnCollisionEnter2D 이벤트 추가
        }
    }
}

// OnCollisionEnter2D 이벤트를 처리하는 스크립트
public class CollisionHandler : MonoBehaviour
{
    private int AtkDamege;
    public void Initialize(int AtkDamege)
    {
        this.AtkDamege = AtkDamege;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 대상이 Player 태그인 경우 디버그 작성
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("attackCollider가 Player 태그와 충돌했습니다.");
            BattleManager battleManager = GameObject.FindWithTag("BattleManager").GetComponent<BattleManager>();
            battleManager.GetPlayerInfo(AtkDamege);
            Destroy(gameObject);
        }
    }
}
