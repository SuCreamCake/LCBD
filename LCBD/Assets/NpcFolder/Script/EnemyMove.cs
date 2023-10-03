using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    Rigidbody2D rigid;
    public int nextMove;//다음 행동지표를 결정할 변수
    Animator animator;
    SpriteRenderer spriteRenderer;
    MonsterManager MonsterManager;

    bool searching = false;

    public float waitingtime = 5.0f;

    int monsterID; //받아오는 몬스터 id

    // Start is called before the first frame update
    private void Awake()
    {
        MonsterManager = GetComponent<MonsterManager>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 5); // 초기화 함수 안에 넣어서 실행될 때 마다(최초 1회) nextMove변수가 초기화 되도록함 

    }

    private void Start()
    {
        monsterID = MonsterManager.MonsterID;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!searching)
        {
            //Move
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y); //nextMove 에 0:멈춤 -1:왼쪽 1:오른쪽 으로 이동 

            //자신의 한 칸 앞 지형을 탐색해야하므로 position.x + nextMove(-1,1,0이므로 적절함)
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.4f, rigid.position.y);

            //한칸 앞 부분아래 쪽으로 ray를 쏨
            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

            //레이를 쏴서 맞은 오브젝트를 탐지 
            RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("background"));

            //탐지된 오브젝트가 null : 그 앞에 지형이 없음
            if (raycast.collider == null)
            {
                Turn();
            }
        } else
        {
            // 몬스터 멈추도록 설정
            rigid.velocity = Vector2.zero;
        }




    }


    public void Think()
    {
        float time = 3.0f;
        //몬스터가 스스로 생각해서 판단 (-1:왼쪽이동 ,1:오른쪽 이동 ,0:멈춤  으로 3가지 행동을 판단)

        //Random.Range : 최소<= 난수 <최대 /범위의 랜덤 수를 생성(최대는 제외이므로 주의해야함)
        nextMove = Random.Range(-1, 2);

        ////Sprite Animation
        ////WalkSpeed변수를 nextMove로 초기화
        //animator.SetInteger("WalkSpeed", nextMove);


        //Flip Sprite
        if (nextMove != 0) //서있을 때 굳이 방향을 바꿀 필요가 없음 
            spriteRenderer.flipX = nextMove == 1; //nextmove 가 1이면 방향을 반대로 변경

        if(nextMove != 0)
        {
            time = 3.0f; //생각하는 시간
        } else
        {
            time = waitingtime; // 몬스터의 이동속도 능력치 +2 + (2*스테이지 수) / 몬스터의 이동속도 능력치
        }
        //Think(); : 재귀함수 : 딜레이를 쓰지 않으면 CPU과부화 되므로 재귀함수쓸 때는 항상 주의 ->Think()를 직접 호출하는 대신 Invoke()사용
        Invoke("Think", time); //매개변수로 받은 함수를 time초의 딜레이를 부여하여 재실행
        
        if(nextMove == 0 && monsterID >= 1000)
        {
            SearchPalyer();
        }
    }

    void Turn()
    {

        nextMove = nextMove * (-1); //우리가 직접 방향을 바꾸어 주었으니 Think는 잠시 멈추어야함
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke(); //think를 잠시 멈춘 후 재실행
        Invoke("Think", 2);//

    }

    public void StopThink()
    {
        CancelInvoke();
    }

    public void SearchPalyer()
    {
        searching = true;
        CancelInvoke(); //think를 잠시 멈춘 후 재실행

        // 2초마다 MyFunction 메서드를 호출합니다.
        InvokeRepeating("Serch", 0.0f, 2.0f);
        Invoke("StopSerch", waitingtime);
    }

    void Serch()
    {
        Debug.Log(monsterID);
        if (spriteRenderer.flipX)
            nextMove = 1;
            else
            nextMove = -1;
        nextMove = nextMove * (-1); //우리가 직접 방향을 바꾸어 주었으니 Think는 잠시 멈추어야함
        spriteRenderer.flipX = nextMove == 1;
    }
    
    void StopSerch()
    {
        CancelInvoke("Serch");
        Invoke("Think", 3);
        searching = false;
    }


}