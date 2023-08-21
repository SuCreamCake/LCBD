using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //TalkManage talkManger;
    Rigidbody2D rigid;
    //이동속도
    public float maxSpeed;  
    public float jumpPower;
    SpriteRenderer spriteRenderer;
    bool isLadder;
    public float maxHealth;  //최대체력
    public float health;     //현재체력
    public int attackPower;    //공격력
    public int maxEndurance;    //최대지구력
    //지구력
    public int endurance;
    public bool enduranceOnOff;
    public float stayTime;
    public int enduranceRec;

    public int defense;    //방어력
    public int tenacity;    //강인도
    public float attackSpeed;    //공격속도
    public float crossroads;    //사거리
    public float luck;    //행운
    //음파 오브젝트
    public GameObject soundWave;
    private float time = 0;
    public int stage;    //스테이지
    new CapsuleCollider2D collider2D;    //사이즈 변경을 위한 콜라이더

    /*지학 추가*/
    //쿨타임 텍스트
    public Text text_CoolTime;
    //쿨타임 이미지
    public Image image_fill;
    //스킬 재사용까지 남은시간
    private float time_current;
    //time.Time과 비교해서 time 
    private float time_start;
    private bool isEnded = true;
    //hp바 텍스트
    public Text text_hp;
    //hp바 이미지
    public Image img;

    Animator ani;    //애니메이션

    //인격 스택
    public int oralStack;
    private int analStack;
    private int phallicStack;
    private int growingUpStack;
    private int IncubationStack;
    private int genitalStack;

    //사운드 오브젝트
    public GameObject SoundsPlayer;
    //동주 전투
    //공격속도를 체크하기 위한 변수
    public float attackTime = 0;
    //원거리 공격 오브젝트
    public GameObject bulletObject;
    //무기 객체를 담는 자료형
    public GameObject[] weapons;
    //맨손 공격, 근접공격, 원거리 공격 인덱스
    bool sDown1;
    bool sDown2;
    bool sDown3;
    bool sDown4;
    //무기 인덱스
    public int weaponeIndex = -1;
    //공격위치
    public Transform attackPosition;

    GameObject equipWeapon;
    //쉴드 오브젝트
    public GameObject shieldObject;
    //사운드 웨이브 공격
    public GameObject soundWaveAttackObject;
    //적 레이어 마스크
    public LayerMask enemyLayers;
    //음파 공격 시간
    public float soundWaveAttackTime = 0;
    //총 공격량
    public int totalAttackPower;
    //총 방어량
    public int totalShield;




    private void Awake()
    { 
        ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<CapsuleCollider2D>();
        
        infancy();
       
    }
    void Start()
    {
        Init_UI();
        Init_HP();
        SetFunction_UI();
    }

    private void Update()
    {
        

        attackTime += Time.deltaTime;
        jump();
        stopSpeed();
        getInputBattleKeyKode();
        swapWeapon();
        battleLogic();
        getInputSoundWaveAttack();
        soundWaveAttackTime += Time.deltaTime;
        switch (stage)
        {
            case 1:
                attack();
                break;
            case 3:
                ladderJump();
                break;
            case 4:
                ladderJump();
                break;
            default:
                break;
        }
        
        maxState();
        minState();
        Check_CoolTime();
        SetFunction_UI();
        Set_HP(health);

    }



    private void FixedUpdate()
    {
        walk();
        upDown();
        enduranceSystem();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            rigid.gravityScale = 0;
            rigid.drag = 3;
        }
        if (collision.CompareTag("StagePortal"))
        {

            DontDestroyOnLoad(SoundsPlayer);
            SoundsPlayer.GetComponent<SoundsPlayer>().InteractionSound(0);  // Portal Sound
            switch (stage)
            {
                case 1:
                    stage = 2;
                    childhood();
                    
                    SceneManager.LoadScene("stage2");
                    break;
                case 2:
                    stage = 3;
                    adolescence();
                    SceneManager.LoadScene("stage3");
                    break;
                case 3:
                    stage = 4;
                    adulthood();
                    SceneManager.LoadScene("stage4");
                    break;
                case 4:
                    stage = 5;
                    oldAge();
                    SceneManager.LoadScene("stage5");
                    break;
            }
        }
        personality(collision);
     
    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            rigid.gravityScale = 2;
            ani.SetBool("isLadder", false);
        }
        if (collision.CompareTag("TestTag"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 2f, rigid.velocity.y);
        }
    }


    private void jump()
    {
        //Jump
        if (Input.GetButtonDown("Jump") && rigid.velocity.y == 0)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            ani.SetTrigger("isJumping");

            SoundsPlayer.GetComponent<SoundsPlayer>().JumpSound(0);     // Jump Sound 
        }

    }
    private void stopSpeed()
    {
        //stop speed
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 2f, rigid.velocity.y);
    }
    private void attack()
    {
        //attack
        time += Time.deltaTime;
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
        Input.mousePosition.y, -Camera.main.transform.position.z));
        if (time >= attackSpeed && Input.GetMouseButtonDown(0))
        {
            //쿨타임 추가 -지학-
            Reset_CoolTime();
            time = 0;
            soundWave.transform.position = new Vector2(point.x, point.y);
            if (crossroads < Mathf.Sqrt(Mathf.Pow(point.x - this.transform.position.x, 2) + Mathf.Pow(point.y - this.transform.position.y, 2)))
            {

                float maxCrossroads = crossroads / Mathf.Sqrt(Mathf.Pow(point.x - this.transform.position.x, 2) + Mathf.Pow(point.y - this.transform.position.y, 2));

                soundWave.transform.position = new Vector2(this.transform.position.x + (point.x - this.transform.position.x) * maxCrossroads
                    , this.transform.position.y + (point.y - this.transform.position.y) * maxCrossroads);
            }
            SoundsPlayer.GetComponent<SoundsPlayer>().AttackSound(0);
            Instantiate(soundWave);
        }
    }
    private void walk()
    {
        int key = 0;
        if (Input.GetKey(KeyCode.A)) key = -1;
        if (Input.GetKey(KeyCode.D)) key = 1;

        float speedx = Mathf.Abs(this.rigid.velocity.x);

        if (speedx < maxSpeed)
            this.rigid.AddForce(transform.right * key * 30);



        //스프라이트 반전
        if (key != 0 && stage == 1)
        {
            transform.localScale = new Vector3(key, 1, 1);
            //지구력 테스트
            endurance--;
            enduranceOnOff = false;
        }

  

        if (key != 0 && stage == 2)
            transform.localScale = new Vector3(-key * 1.5f, 1.5f, 0);


        if (key == 0)
            ani.SetBool("isWalking", false);
        else
            ani.SetBool("isWalking", true);

        // Walk Sound 
        if (stage == 1)
            SoundsPlayer.GetComponent<SoundsPlayer>().WalkSound(0);
        else if (stage == 2)
            SoundsPlayer.GetComponent<SoundsPlayer>().WalkSound(1);



    }
    private void upDown()
    {
        //Updown
        if (isLadder)
        {
            SoundsPlayer.GetComponent<SoundsPlayer>().LadderSound(0);       // Jump Sound
            float ver = Input.GetAxis("Vertical");
            rigid.velocity = new Vector2(rigid.velocity.x , ver * maxSpeed);
            if(ver != 0)
                ani.SetBool("isLadder", true);
            
        }
    }
    private void infancy()
    {
        //이동속도
        maxSpeed = 5 -2;
        //점프력
        jumpPower = 10 -2;
        //체력
        maxHealth = 30;
        health = 30;
        //공격력
        attackPower = 5;
        //지구력
        maxEndurance = 40;
        endurance = 40;
        enduranceRec = 4;
        //방어력
        defense = 50;
        //강인도
        tenacity = 80;
        //공격속도
        attackSpeed = 3;
        //사거리
        crossroads = 3;
        //행운
        luck = 15 + 20;
        
    }

    private void childhood()
    {
        //사이즈 변경
        transform.localScale = new Vector3(1.5f, 1.5f, 0);
        collider2D.size = new Vector3(0.4f, 0.8f,0);
        //이동속도
        maxSpeed += 2;
        //점프력
        jumpPower += 2;
        //행운
        luck -= 20;
        //공격력
        attackPower += 15;
        //사거리
        crossroads += 2;
        //애니메이션
        ani.SetTrigger("isChildhood");
    }

    private void adolescence()
    {
        //공격력
        attackPower -= 15;
        //사거리
        crossroads -= 2;
        //이동속도
        maxSpeed += 0.5f;
        //점프력
        jumpPower += 1;
    }
    private void adulthood()
    {
        //공격속도
        attackSpeed += 1.7f;

    }

    private void oldAge()
    {
        //이동속도
        maxSpeed -= 3;
        //점프력
        jumpPower -= 3;
    }

    private void ladderJump()
    {
        if (isLadder && Input.GetButtonDown("Jump"))
        {
            InvokeRepeating("InvokeJump", 0.01f, 0.01f);
        }
    }
    private void InvokeJump()
    {
        if (isLadder)
            transform.Translate(0, 0.5f, 0);
        else
        {
            CancelInvoke("InvokeJump");
            transform.Translate(0, -1.5f, 0);
        }
    }

    private void maxState()
    {
        if (maxHealth > 1000000)
            maxHealth = 1000000;
        if (attackPower > 1000000)
            attackPower = 1000000;
        if (defense > 100000)
            defense = 100000;
        if (maxSpeed > 100)
            maxSpeed = 100;
        if (tenacity > 200)
            tenacity = 200;
        if (attackSpeed > 5)
            attackSpeed = 5;
        if (crossroads > 30)
            crossroads = 30;
        if (luck > 100)
            luck = 100;
        if (endurance > maxEndurance)
            endurance = maxEndurance;

    }
    private void minState()
    {
        if (attackPower < 0)
            attackPower = 0;
        if (endurance < 0)
            endurance = 0;
        if (defense <0)
            defense = 0;
        if (maxSpeed < 0)
            maxSpeed = 0;
        if (tenacity < 0)
            tenacity = 0;
        if (attackSpeed < 0)
            attackSpeed = 0;
        if (crossroads <0)
            crossroads = 0;
        if (luck < 0)
            luck = 0;

    }

    private void enduranceRecovery()
    {

        if (endurance < maxEndurance)
            endurance += enduranceRec;
        else
            endurance = maxEndurance;
    }

    private void enduranceSystem()
    {
        if (enduranceOnOff == false)
        {
            stayTime += Time.deltaTime;
            CancelInvoke("enduranceRecovery");
        }
        if (stayTime > 3)
        {
            enduranceOnOff = true;
            stayTime = 0;
            InvokeRepeating("enduranceRecovery", 1, 1);
        }
    }

    private void personality(Collider2D collision)
    {
        
        if (collision.CompareTag("OralStage"))
        {
            maxHealth += 5;
            maxEndurance += 5;
            oralStack++;
            if (oralStack >= 5)
                enduranceRec = 4 + 4 * (oralStack / 2);    //구강기 특수능력
            //인격 조각을 통해 얻은 지구력 10당 초당 회복되는 지구력 4 증가

        }
        if (collision.CompareTag("AnalStage"))
        {
            defense += 10;
            tenacity += 10;
            analStack++;

            //항문기 특수 능력
            //방어력의 10%만큼 강인도 최대치 증가
            if (analStack == 5)
                tenacity += defense / 10;
            if (analStack > 5)
                tenacity = tenacity - (defense - 10) / 10 + (defense / 10);

        }
        if (collision.CompareTag("PhallicStage"))
        {
            attackPower += 10;
            tenacity += 5;
            phallicStack++;
            //남근기 특수 능력
            //인격 조각으로 얻은 공격력 10% 증가
            if (phallicStack == 5)
                attackPower += phallicStack;
            if (phallicStack > 5)
                attackPower = attackPower - (phallicStack - 1) + phallicStack;

        }
        if (collision.CompareTag("GrowingUp"))
        {
            maxHealth += 5;
            maxSpeed += 10;
            growingUpStack++;
            //성장기 특수 능력
            //(먹은 인격 개수 * 5)만큼 생명력 최대치 증가
            if (growingUpStack == 5)
                maxHealth += growingUpStack * 5;
            if (growingUpStack > 5)
                maxHealth = maxHealth - (growingUpStack - 1) * 5 + growingUpStack * 5;

        }
        if (collision.CompareTag("IncubationPeriod"))
        {
            luck += 5;
            defense += 5;
            IncubationStack++;
            //잠복기 특수 능력
            //피격 시 행운의 확률로 방어량 10% 증가
            //피격 미구현으로 보류
        }
        if (collision.CompareTag("GenitalStage"))
        {
            attackSpeed += 0.2f;
            crossroads += 0.25f;
            genitalStack++;
            //생식기 특수 능력
            //공격속도 1당 공격력 20 증가
            if (genitalStack == 5)
                attackPower += (int)attackSpeed * 20;
            if (genitalStack > 5)
                attackPower = attackPower - (int)(attackSpeed - 1) * 20 + (int)attackSpeed * 20;
        }
        
    }


    /*지학*/
    //image_fill의 fillAmount를 360도 시계 반대 방향으로 회전하게 설정
    private void Init_UI()
    {
        image_fill.type = Image.Type.Filled;
        image_fill.fillMethod = Image.FillMethod.Radial360;
        image_fill.fillOrigin = (int)Image.Origin360.Top;
        image_fill.fillClockwise = false;
    }
    //쿨타임 리셋
    private void Check_CoolTime()
    {
        time_current = Time.time - time_start;
        if (time_current < attackSpeed)
        {
            Set_FillAmount(attackSpeed - time_current);
        }
        else if (!isEnded)
        {
            End_CoolTime();
        }
    }
    //쿨타임이 끝나서 스킬 재사용이 가능해진 시점
    private void End_CoolTime()
    {
        Set_FillAmount(0);
        isEnded = true;
        text_CoolTime.gameObject.SetActive(false);
    }
    //쿨타임 타이머 시작
    private void Reset_CoolTime()
    {
        text_CoolTime.gameObject.SetActive(true);
        time_current = attackSpeed;
        time_start = Time.time;
        Set_FillAmount(attackSpeed);
        isEnded = false;
    }
    //스킬 재사용 시간 시각화
    private void Set_FillAmount(float _value)
    {
        image_fill.fillAmount = _value / attackSpeed;
        string txt = _value.ToString("0.0");
        text_CoolTime.text = txt;
    }
    //HP의 값과 UI 표시 초기화
    private void Init_HP()
    {
        Set_HP(maxHealth);
    }

    private void SetFunction_UI()
    {
        //Fill Amount Type
        img.type = Image.Type.Filled;
        img.fillMethod = Image.FillMethod.Horizontal;
        img.fillOrigin = (int)Image.OriginHorizontal.Left;
    }
    //hp에서 매개 변수로 받은 float 값을 더함
    private void Change_HP(float _value)
    {
        health += _value;
        Set_HP(health);
    }
    //hp를 매개변수로 받은 float 값으로 변경
    private void Set_HP(float _value)
    {
        health = _value;
    }
    private void AnimationMotion()
    {
        if (Mathf.Abs(rigid.velocity.normalized.x) < 0.2)
        {
            ani.SetBool("isRunning", false);
        }
        else
        {
            ani.SetBool("isRunning", true);
        }

        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    //Debug.Log("점프 끝");
                    ani.SetBool("isJumping", false);
                }
            }
        }
    }

    //동주
    //맨손 공격
    public void punchAttack()
    {
        if (attackTime > (attackSpeed / 2) && Input.GetMouseButtonDown(0))
        {
            attackTime = 0;
            //마우스의 위치 가져오기
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);
            //현재 캐릭터의 위치 가져오기
            Vector2 characterPoint = new(transform.position.x, transform.position.y);
            //startX, startY좌표 구하기 위한, 거리와 각도
            float rangeRadius = crossroads / 6.0f; //원의 반지름 1/3 1/2 == 1/6
            float rangeRadian = Mathf.Atan2(mousePoint.y - characterPoint.y, mousePoint.x - characterPoint.x);
            //원형레이캐스팅  시작점 (=중심점)
            float startX = characterPoint.x + rangeRadius * Mathf.Cos(rangeRadian);
            float startY = characterPoint.y + rangeRadius * Mathf.Sin(rangeRadian);
            //원형레이캐스팅
            Vector2 startAttackPoint = new(startX, startY);
            //공격 가능한 레이어를 추가하고 해당 레이어만 감지하도록 레이어 추가하고 레이어 감지 인자 수정 필요 (여러 레이어도 감지 가능) (일단 플레이어 제외한 모든 레이어 감지)
            //int layerMask = 1 << LayerMask.NameToLayer("monster");
            ////layerMask = ~layerMask;
            //RaycastHit2D raycastHit = Physics2D.CircleCast(startAttackPoint, rangeRadius, Vector2.right, 0f, layerMask);
            //if (raycastHit.collider != null)    //대상 감지되면
            //{
            //    Debug.Log("맨손 공격에 감지된 대상 오브젝트: " + raycastHit.collider.gameObject.tag);
            //    //진짜 공격해서 감지한 대상 체력 깎아주기
            //}

            //Collider2D hitEnemys = Physics2D.OverlapCircle(startAttackPoint,rangeRadius,enemyLayers);

            RaycastHit2D raycastHit = Physics2D.CircleCast(startAttackPoint, rangeRadius, Vector2.right, 0f, enemyLayers);
            if (raycastHit.collider != null)
            {
                CalDamage();
                raycastHit.collider.GetComponent<EnemyHit>().TakeDamage(totalAttackPower);
                Debug.Log("몬스터 맞춤");

            }
                

            //수치 디버깅
            Debug.Log("mousePoint: " + mousePoint);
            Debug.Log("characterPoint: " + characterPoint);
            Debug.Log("rangeRadian: " + rangeRadian);
            Debug.Log("startAttackPoint: " + startAttackPoint);

            //레이캐스트 범위 그리기 디버그용 추후 삭제
            Debug.DrawRay(characterPoint, new Vector2(rangeRadius * Mathf.Cos(rangeRadian), rangeRadius * Mathf.Sin(rangeRadian)).normalized * crossroads, Color.white, 0.3f);      //캐릭터 중점 ~ 원래 사거리
            Debug.DrawRay(characterPoint, new Vector2(rangeRadius * Mathf.Cos(rangeRadian), rangeRadius * Mathf.Sin(rangeRadian)).normalized * crossroads / 3f, Color.green, 0.3f); //캐릭터 중점 ~ 맨손 사거리
            Debug.DrawRay(characterPoint, new Vector2(rangeRadius * Mathf.Cos(rangeRadian), rangeRadius * Mathf.Sin(rangeRadian)).normalized * rangeRadius, Color.black, 0.3f);     //캐릭터 중점 ~ 원 범위 중점까지 거리
            Debug.DrawRay(startAttackPoint, Vector2.up * rangeRadius, Color.red, 0.3f);                     //대충 원 위쪽 범위
            Debug.DrawRay(startAttackPoint, Vector2.down * rangeRadius, Color.red, 0.3f);                   //대충 원 아래쪽 범위
            Debug.DrawRay(startAttackPoint, Vector2.right * rangeRadius, Color.red, 0.3f);                  //대충 원 오른쪽 범위
            Debug.DrawRay(startAttackPoint, Vector2.left * rangeRadius, Color.red, 0.3f);                   //대충 원 왼쪽 범위
            Debug.DrawRay(startAttackPoint, Vector2.one.normalized * rangeRadius, Color.red, 0.3f);         //대충 원 우상향 대각선 범위
            Debug.DrawRay(startAttackPoint, new Vector2(1, -1).normalized * rangeRadius, Color.red, 0.3f);  //대충 원 우하향 대각선 범위
            Debug.DrawRay(startAttackPoint, new Vector2(-1, 1).normalized * rangeRadius, Color.red, 0.3f);  //대충 원 좌상향 대각선 범위
            Debug.DrawRay(startAttackPoint, -Vector2.one.normalized * rangeRadius, Color.red, 0.3f);        //대충 원 좌하향 대각선 범위
        }
    }
    //근접 공격
    private void meleeAttack()
    {
        if (attackTime > attackSpeed && Input.GetMouseButtonDown(0))
        {
            attackTime = 0;
            //마우스의 위치 가져오기
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

            //공격방향
            Vector2 attackForce = mousePoint - (Vector2)transform.position;
            attackForce = attackForce.normalized;

            //공격 범위
            float xRange = crossroads * 0.3f;
            float yRange = 0.5f;
            Vector2 boxSize = new Vector2(xRange, yRange);

            float angle = Mathf.Atan2(attackForce.y, attackForce.x) * Mathf.Rad2Deg;
            //공격 콜라이더 생성
            Collider2D[] colliders = Physics2D.OverlapBoxAll(attackPosition.position, boxSize , angle, enemyLayers);
            Debug.Log(angle);
            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.tag);
                if(collider.tag=="monster")
                {
                    Debug.Log("몬스터 맞춤");
                    collider.GetComponent<EnemyHit>().TakeDamage(totalAttackPower);
                }
            }
            Debug.Log("공격실행");
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(attackPosition.position,new Vector2(crossroads * 0.3f, 0.7f));
    }


    //원거리공격 메서드
    private void longDistanceAttack()
    {
        if (attackTime > attackSpeed && Input.GetMouseButtonDown(0))
        {
            attackTime = 0;
            //마우스의 위치 가져오기
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

            Vector3 playerPos = transform.position;

            Vector2 direVec = mousePoint - (Vector2)playerPos;
            direVec = direVec.normalized;
            GameObject tempObeject = Instantiate(bulletObject);
            tempObeject.transform.position = transform.position;
            tempObeject.transform.right = direVec;


        }
    }

    //전투관련 키 입력
    private void getInputBattleKeyKode()
    {
        sDown1 = Input.GetKeyDown(KeyCode.F1);
        sDown2 = Input.GetKeyDown(KeyCode.F2);
        sDown3 = Input.GetKeyDown(KeyCode.F3);
        sDown4 = Input.GetKeyDown(KeyCode.F4);
    }

    private void getInputSoundWaveAttack()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            soundWaveAttack();
            Debug.Log("F누름");
        }
    }
    //공격 타입 인덱스
    private void swapWeapon()
    {
        if (sDown1)
        {
            weaponeIndex = 0;
            Debug.Log("버튼 1활성화");
        }
        if (sDown2) weaponeIndex = 1;
        if (sDown3) weaponeIndex = 2;
        if (sDown4) weaponeIndex = 3;
        if (sDown1 || sDown2 || sDown3 || sDown4)
        {
            if (equipWeapon != null)
                equipWeapon.SetActive(false);
            equipWeapon = weapons[weaponeIndex];
            if(weaponeIndex != 3)
                equipWeapon.SetActive(true);
        }
    }
    private void battleLogic()
    {
        if (weaponeIndex == 0)
            meleeAttack();
        else if (weaponeIndex == 1)
            longDistanceAttack();
        else if (weaponeIndex == 2)
            punchAttack();
        else if(weaponeIndex == 3)
            shield();

    }

    //방어 방법 메서드
    private void shield()
    {
        if (Input.GetMouseButtonDown(0))
        {
            equipWeapon.SetActive(true);
            //마우스의 위치 가져오기
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);
            Vector3 playerPos = transform.position;
            Vector2 direVec = mousePoint - (Vector2)playerPos;
            direVec = direVec.normalized;
            equipWeapon.transform.position = direVec+(Vector2)transform.position;
            
            //우측일 경우
            if (Vector3.Dot(transform.right, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
            {
                Debug.Log("우측 방패 생성");
                equipWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            //위쪽일 경우
            else if (Vector3.Dot(transform.up, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
            {
                Debug.Log("위측방패 생성");
                equipWeapon.transform.rotation = Quaternion.Euler(0, 0, 90f);
            }
            else if (Vector3.Dot(-transform.right, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
            {
                Debug.Log("좌측 방패 생성");
                equipWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);


            }
            else
            {
                Debug.Log("하단 방패 생성");
                equipWeapon.transform.rotation = Quaternion.Euler(0, 0, 90f);
            }

        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            
            equipWeapon.SetActive(false);
        }
    }
    private void soundWaveAttack()
    {

        if (soundWaveAttackTime >= 3.0f)
        {
            soundWaveAttackTime = 0;
            RaycastHit2D[] raycastHit2Ds;
            soundWaveAttackTime = 0;
            //마우스의 위치 가져오기
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

            //공격방향
            Vector2 attackForce = mousePoint - (Vector2)transform.position;
            attackForce = attackForce.normalized;
            Debug.Log(attackPower);
            float startAngle = -attackPower / 2;

            //일반 음파 공격
            for (float startAngleIndex = startAngle; startAngleIndex <= attackPower / 2; startAngleIndex += 0.5f)
            {
                attackForce = Quaternion.Euler(0, 0, startAngleIndex) * attackForce;
                Debug.Log(startAngleIndex);
                raycastHit2Ds = Physics2D.RaycastAll(transform.position, attackForce, crossroads, enemyLayers);
                for (int i = 0; i < raycastHit2Ds.Length; i++)
                {
                    RaycastHit2D hit = raycastHit2Ds[i];
                    if (hit.collider.tag == "monster")
                    {

                        hit.collider.GetComponent<EnemyHit>().TakeDamage(totalAttackPower);
                    }
                }
            }

            //1/3지역 원형 공격 범위
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, crossroads / 3, enemyLayers);
            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "monster")
                {
                    Debug.Log("1/3지역 피격");
                    collider.GetComponent<EnemyHit>().IsCrossroadThird();
                    collider.GetComponent<EnemyHit>().TakeDamage(attackPower / 3);
                }
                string txt = "";
                if (health <= 0)
                {
                    health = 0;
                    txt = "Dead";
                }
                else
                {
                    if (health > maxHealth)
                        health = maxHealth;
                    txt = string.Format("{0}/{1}", health, maxHealth);
                }
                img.fillAmount = health / maxHealth;

                text_hp.text = txt;

            }
        }
    }
    //데미지 공식
    private void CalDamage()
    {
        totalAttackPower+=0/*여기 공격량 공식 들어갈 예정*/;
    }
    //방어 공식
    private void TotalShield()
    {
        totalShield += 0;/*여기도 마찬가지임*/;
    }
}
