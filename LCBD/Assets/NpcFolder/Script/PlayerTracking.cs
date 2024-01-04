using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 저장하는 변수
    public float moveSpeed = 1f; // 이동 속도
    private float raycastDistance = 0.5f; // 레이캐스트 거리
    MonsterManager MonsterManager;
    SpriteRenderer spriteRenderer;
    public LayerMask backMask; // 바닥 레이어

    public bool noPlayer = true; // 플레이어가 없는지 여부를 나타내는 변수

    private Transform lastPlayer; // 마지막으로 본 플레이어의 Transform을 저장하는 변수

    private Rigidbody2D rb; // Rigidbody2D 컴포넌트
    float k;

    private float attackRange = 1f;// 몬스터 사거리 계산

    private void Awake()
    {
        spriteRenderer = GetComponent <SpriteRenderer>();
        MonsterManager = GetComponent<MonsterManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        lastPlayer = player.transform;
        k = rb.velocity.y;
        noPlayer = true;

        moveSpeed = MonsterManager.speed_Ms; // 이동 속도를 몬스터 매니저의 속도로 설정
        attackRange = MonsterManager.crossroads_Ms;
        MonsterManager.ChangeState(1);
    }

    private void Update()
    {
        if (noPlayer)
            lastPlayer = player.transform;

        // 앞쪽 레이캐스트를 생성
        Vector2 frontVec = new Vector2(rb.position.x + 0.4f, rb.position.y);
        // 뒷쪽 레이캐스트를 생성
        Vector2 frontVec2 = new Vector2(rb.position.x - 0.4f, rb.position.y);

        // 플레이어와 몬스터의 상대적인 가로 입력을 계산
        float horizontalInput = 0f;
        if (player != null)
        {
            if (lastPlayer.position.x < transform.position.x)
            {
                horizontalInput = -1f;
                spriteRenderer.flipX = false;
            }
            else if (lastPlayer.position.x > transform.position.x)
            {
                horizontalInput = 1f;
                spriteRenderer.flipX = true;
            }
        }
        // 땅과의 레이를 디버그로 표시
        Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.red);

        // 이동 방향 벡터 생성
        Vector2 moveDirection = new Vector2(horizontalInput, k);

        // 플레이어와의 가로 거리 계산
        float distanceToPlayerX = Mathf.Abs(transform.position.x - lastPlayer.position.x);

        // 멈추는 거리를 설정
        float stopDistanceX = 0.2f;

        if (distanceToPlayerX < stopDistanceX)
        {
            // 플레이어와의 거리가 멈추는 거리보다 작으면 멈춤
            rb.velocity = Vector2.zero;
            if (!noPlayer)
            {
                noPlayer = true;
                MonsterManager.SearchMode(); // 몬스터 매니저의 탐색 모드 활성화
            }
        }
        else
        {
            rb.velocity = moveDirection * moveSpeed; // 이동
        }

        // 앞쪽과 뒷쪽의 레이를 디버그로 표시
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        Debug.DrawRay(frontVec2, Vector3.down, new Color(0, 1, 0));

        if (spriteRenderer.flipX)
        {
            // 앞쪽으로 레이를 쏴서 땅을 감지
            RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1, backMask);
            // 감지된 땅이 없으면 멈춤
            if (raycast.collider == null)
            {
                rb.velocity = Vector2.zero;
                if (!noPlayer)
                {
                    noPlayer = true;
                    MonsterManager.SearchMode(); // 몬스터 매니저의 탐색 모드 활성화
                }
            }
        }
        else
        {
            // 뒷쪽으로 레이를 쏴서 땅을 감지
            RaycastHit2D raycast2 = Physics2D.Raycast(frontVec2, Vector3.down, 1, backMask);
            // 감지된 땅이 없으면 멈춤
            if (raycast2.collider == null)
            {
                rb.velocity = Vector2.zero;
                if (!noPlayer)
                {
                    noPlayer = true;
                    MonsterManager.SearchMode(); // 몬스터 매니저의 탐색 모드 활성화
                }
            }
        }
        MonsterManager.ChangeState(1);
    }
}
