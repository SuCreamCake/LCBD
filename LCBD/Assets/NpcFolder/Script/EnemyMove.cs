using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove; // 다음 움직임 방향을 나타내는 변수
    Animator animator;
    SpriteRenderer spriteRenderer;
    MonsterManager MonsterManager;
    public int moveSpeed; // 이동 속도

    bool searching = false;

    public float waitingtime;

    int monsterID; // 몬스터의 ID

    float time = 3.0f; // 좌우 변경 랜덤으로 하자

    private Coroutine myCoroutine; // time 코루틴 돌리기

    // Start is called before the first frame update
    private void Awake()
    {
        MonsterManager = GetComponent<MonsterManager>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent <SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", time); // 일정 시간 뒤에 Think 함수를 호출하여 다음 움직임을 결정
        myCoroutine = StartCoroutine(RandomTimeCoroutine());
    }

    private void Start()
    {
        monsterID = MonsterManager.MonsterID;
        moveSpeed = MonsterManager.speed_Ms;
        waitingtime = MonsterManager.waiteTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!searching)
        {
            // 이동
            rigid.velocity = new Vector2(nextMove * moveSpeed, rigid.velocity.y); // nextMove 값에 따라 좌우로 이동

            // 캐릭터의 정면 위치 계산 (nextMove에 따라 좌우로 이동)
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.4f, rigid.position.y);

            // 지면과의 충돌을 검출하는 Ray를 그림
            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

            // 지면과의 충돌을 감지
            RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("background"));

            // 지면과 충돌하지 않으면 방향을 바꿈
            if (raycast.collider == null)
            {
                Turn();
            }
        }
        else
        {
            // 적 탐색 중에는 움직이지 않음
            rigid.velocity = Vector2.zero;
        }
    }

    public void Think()
    {

        // 다음 움직임 방향을 무작위로 결정 (-1: 왼쪽, 1: 오른쪽, 0: 정지, 3초마다 변경)

        // Random.Range: min 이상 max 미만의 난수를 생성 (정수 난수를 얻으려면 Mathf.RoundToInt 사용)
        nextMove = Random.Range(-1, 2);

        // 캐릭터의 스프라이트 애니메이션을 설정 (WalkSpeed 변수를 통해 움직임 방향을 전달)
        // animator.SetInteger("WalkSpeed", nextMove);

        // 스프라이트를 뒤집음
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;

        if (nextMove != 0)
        {
            time = 3.0f; // 이동 시간 설정
        }
        else
        {
            time = waitingtime; // 정지 시간 설정
        }
        // Think()를 재귀적으로 호출하지 말고, Invoke()를 사용하여 다음 움직임을 일정 시간 후에 결정
        Invoke("Think", time);

        if (nextMove == 0 && monsterID >= 1000)
        {
            SearchPlayer();
        }
    }

    void Turn()
    {
        nextMove = nextMove * (-1); // 이동 방향을 반대로 변경하여 Think 함수를 다시 호출
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke(); // Think 함수 호출 예약 취소
        Invoke("Think", time);
    }

    public void StopThink()
    {
        CancelInvoke(); // Think 함수 호출 예약 취소
    }

    public void SearchPlayer()
    {
        searching = true;
        CancelInvoke(); // Think 함수 호출 예약 취소

        // Serch 함수를 0초부터 시작하여 2초 간격으로 반복 호출
        InvokeRepeating("Serch", 0.0f, 2.0f);
        Invoke("StopSearch", waitingtime);
    }

    void Serch()
    {
        Debug.Log(monsterID);
        if (spriteRenderer.flipX)
            nextMove = 1;
        else
            nextMove = -1;
        nextMove = nextMove * (-1); // 이동 방향을 반대로 변경하여 다시 호출
        spriteRenderer.flipX = nextMove == 1;
    }

    void StopSearch()
    {
        CancelInvoke("Serch");
        Invoke("Think", time);
        searching = false;
    }

    IEnumerator RandomTimeCoroutine()
    {
        while (true)
        {
            // 2초에서 4초 사이의 랜덤한 시간 간격을 계산합니다.
            time = Random.Range(2.0f, 4.0f);

            // 1초에 한 번 실행되도록 대기합니다.
            yield return new WaitForSeconds(2.0f);
        }
    }
}
