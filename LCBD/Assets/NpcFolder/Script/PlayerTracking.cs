using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트의 Transform을 저장할 변수
    public float moveSpeed = 1f;
    private float raycastDistance = 0.5f;
    MonsterManager MonsterManager;
    SpriteRenderer spriteRenderer;

    public bool noPlayer = true;

    private Transform lastPlayer;

    private Rigidbody2D rb;
    float k;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        MonsterManager = GetComponent<MonsterManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        lastPlayer = player.transform;
        k = rb.velocity.y;
        noPlayer = true;
    }

    private void Update()
    {
        if(noPlayer)
            lastPlayer = player.transform;

        //자신의 한 칸 앞 지형을 탐색
        Vector2 frontVec = new Vector2(rb.position.x + 0.4f, rb.position.y);
        //자신의 한 칸 앞 지형을 탐색
        Vector2 frontVec2 = new Vector2(rb.position.x - 0.4f, rb.position.y);

        // 플레이어와 몬스터의 위치를 비교하여 이동 방향을 결정
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
        // 몬스터가 레이캐스트를 그리도록 디버그로 표시
        Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.red);

        // 이동 방향에 따라 몬스터 이동
        Vector2 moveDirection = new Vector2(horizontalInput, k);


        // x값의 거리만 계산
        float distanceToPlayerX = Mathf.Abs(transform.position.x - lastPlayer.position.x);

        // x값의 거리를 기준으로 떨림을 멈추는 조건 추가 (예: x값 거리가 일정 값 이내일 때)
        float stopDistanceX = 0.2f; // x값 거리를 기준으로 떨림을 멈추는 거리 임계값 설정

        if (distanceToPlayerX < stopDistanceX)
        {
            // 몬스터가 플레이어와 x값 거리가 일정 값 이내에 있을 때 떨림 멈추기
            rb.velocity = Vector2.zero;
            if (!noPlayer)
            {
                noPlayer = true;
                MonsterManager.SearchMode();
            }

        }else {
            rb.velocity = moveDirection * moveSpeed;
        }


        //한칸 앞 부분아래 쪽으로 ray를 쏨
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        //한칸 앞 부분아래 쪽으로 ray를 쏨
        Debug.DrawRay(frontVec2, Vector3.down, new Color(0, 1, 0));

       if (spriteRenderer.flipX)
        {
            //레이를 쏴서 맞은 오브젝트를 탐지 
            RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("background"));
            //탐지된 오브젝트가 null : 그 앞에 지형이 없음
            if (raycast.collider == null)
            {
                // 이동 방향이 0.1보다 작을 경우 몬스터 멈추도록 설정
                rb.velocity = Vector2.zero;
                if (!noPlayer)
                {
                    noPlayer = true;
                    MonsterManager.SearchMode();
                }
            }
        }
        else
        {
            //레이를 쏴서 맞은 오브젝트를 탐지 
            RaycastHit2D raycast2 = Physics2D.Raycast(frontVec2, Vector3.down, 1, LayerMask.GetMask("background"));
                        //탐지된 오브젝트가 null : 그 앞에 지형이 없음
        if (raycast2.collider == null)
            {
                // 이동 방향이 0.1보다 작을 경우 몬스터 멈추도록 설정
                rb.velocity = Vector2.zero;
                if (!noPlayer)
                {
                    noPlayer = true;
                    MonsterManager.SearchMode();
                }
            }
        }
    }
}

