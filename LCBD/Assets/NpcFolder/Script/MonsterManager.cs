
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterManager : MonoBehaviour
{
    PlayerTracking PlayerTracking;
    FieldOfView FieldOfView;
    EnemyMove EnemyMove;
    SpriteRenderer SpriteRenderer;
    Animator animator;
    MonsterAtk monsterAtk;

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
    public int elite; // 엘리트 여부 (1: 일반, 2: 엘리트, 3: 보스)
    //public bool method; // 공격 방식 (true: 근접, false: 원거리)
    public float smallViewRadius; // 작은 시야 반지름
    public float viewRadius; // 시야 반지름
    [Range(0, 360)]
    public int viewAngle; // 시야 각도
    public float waiteTime; // 대기 시간
    private string stageString; // 현재 스테이지 이름
    private int stageNum; // 현재 스테이지 번호

    // Start is called before the first frame update
    void Awake()
    {
        PlayerTracking = GetComponent<PlayerTracking>();
        EnemyMove = GetComponent<EnemyMove>();
        FieldOfView = GetComponent <FieldOfView>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        monsterAtk = GetComponent<MonsterAtk>();

        if (elite == 2)
        {
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

            maxHealth_Ms = maxHealth_Ms * 1.2f;
            attackPower_Ms = Round(attackPower_Ms);
            defense_Ms = Round(defense_Ms);
            tenacity_Ms = Round(tenacity_Ms);
        }

        health_Ms = maxHealth_Ms;

        FieldOfView.smallViewRadius = smallViewRadius;
        FieldOfView.viewAngle = viewAngle;
        FieldOfView.viewRadius = viewRadius;

        // 스테이지 이름에서 숫자 부분을 추출하여 현재 스테이지 번호를 확인
        stageString = SceneManager.GetActiveScene().name;

        if (stageString.StartsWith("stage"))
        {
            string numberPart = stageString.Substring(5);
            int.TryParse(numberPart, out stageNum);
        }
        else
            stageNum = 0;

        waiteTime = (speed_Ms + 2 + (2 * stageNum)) / speed_Ms;
    }

    private void Start()
    {
        if (!Relationship)
        {
            PlayerTracking.enabled = false;
            if (monsterAtk != null)
            {
                monsterAtk.enabled = false;
            }
        } else
        {
            PlayerTracking.enabled = false;
            monsterAtk.enabled = false;
            FieldOfView.CoroutineStop();
            FieldOfView.enabled = false;
            EnemyMove.enabled = true;
            
        }

    }

    private void Update()
    {
        if (tenacity_Ms <= 0)
        {
            StartCoroutine(ten());
        }
        if (health_Ms <= 0)
        {
            StartCoroutine(DeadMotion());
            monsterAtk.enabled = false;
        }
    }

    IEnumerator DeadMotion()
    {
        EnemyMove.moveSpeed = 0;
        PlayerTracking.moveSpeed = 0;
        SetDead(true);
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
    IEnumerator ten()
    {
        SetStun(true);
        //속도 줄이고 = 0
        // int a = ?
        monsterAtk.enabled = false;
        float walkspeed1 = speed_Ms;
        EnemyMove.moveSpeed = 0;
        PlayerTracking.moveSpeed = 0;
        yield return new WaitForSeconds(3.0f);
        //속도 원상복귀 = 원래대로
        tenacity_Ms = maxtenacity_Ms;
        EnemyMove.moveSpeed = walkspeed1;
        PlayerTracking.moveSpeed = walkspeed1;
        monsterAtk.enabled = true;
        SetStun(false);
    }

    public void AppearPlayer()
    {
        EnemyMove.StopThink();
        EnemyMove.enabled = false;
        PlayerTracking.enabled = true;
        if(monsterAtk != null)
        {
            monsterAtk.enabled = true;
        }
        once = false;
        monsterAtk.FindPlayer();
        findPlayer = true;
    }

    public void DisAppearPlayer()
    {
        if (!once && pattern)
        {
            PlayerTracking.noPlayer = false;
            monsterAtk.NotFindPlayer();
        }
    }

    public void SearchMode()
    {
        EnemyMove.enabled = true;
        PlayerTracking.enabled = false;
        if(monsterAtk != null)
        {
            monsterAtk.enabled = false;
        }
        once = true;
        if (findPlayer)
        {
            EnemyMove.SearchPlayer();
        }
    }

    public int Round(int Value)
    {
        float floatValue = Value * 1.2f;

        return (int)floatValue;
    }

    public void BehaviorPattern()
    {
        pattern = true; // 행동 패턴 활성화
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
        tenacity_Ms -= (int) damage;
        yield return new WaitForSeconds(0.03f);
        SpriteRenderer.material.color = Color.white;
    }

    public void ChangeState(int newState)
    {
        switch (newState)
        {
            case 0:
                //대기 모션
                animator.SetInteger("State", 0);
                break;

            case 1:
                //이동 모션
                animator.SetInteger("State", 1);
                break;
            default:
                // 지정된 상태가 없는 경우의 처리
                Debug.LogWarning("Unknown state: " + newState);
                break;
        }
    }

    public void SetStun(bool isStun)
    {
        // "Stun" 변수를 설정
        animator.SetBool("Stun", isStun);
    }

    public void SetDead(bool isDead)
    {
        // "Dead" 변수를 설정
        animator.SetBool("Dead", isDead);
    }

    public void SetFind(bool isFind)
    {
        // "Find" 변수를 설정
        animator.SetBool("Find", isFind);
    }

    public void RelationshipFalse()
    {
        Relationship = false;
        Start();
    }
}