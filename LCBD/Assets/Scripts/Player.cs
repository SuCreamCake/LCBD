using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class Player : MonoBehaviour
{
    //TalkManage talkManger;
    Rigidbody2D rigid;
    //이동속도
    public float maxSpeed;  
    public float jumpPower;
    SpriteRenderer spriteRenderer;
    bool isLadder;
    //최대체력
    public int maxHealth;
    //현재체력
    public int health;
    //공격력
    public int attackPower;
    //최대지구력
    public int maxEndurance;
    //지구력
    public int endurance;
    //방어력
    public int defense;
    //강인도
    public int tenacity;
    //공격속도
    public float attackSpeed;
    //사거리
    public float crossroads;
    //행운
    public int luck;
    //음파 오브젝트
    public GameObject soundWave;
    private float time = 0;
    //스테이지
    public int stage;

    //애니메이션
    Animator ani;
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

        infancy();
        

    }

    private void Update()
    {
        AnimationMotion();

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
            //attack();
            break;
         case 4:
            ladderJump();
            break;
         default:
             break;
        }

        maxState();
        minState();

    }



    private void FixedUpdate()
    {
        walk();
        upDown();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            rigid.gravityScale = 0;
            rigid.drag = 3;
        }
        if (collision.CompareTag("Potal"))
        {
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
        if(collision.CompareTag("OralStage"))
        {
            maxHealth += 5;
            maxEndurance += 5;

        }
        if(collision.CompareTag("AnalStage"))
        {
            defense += 10;
            tenacity += 10;

        }
        if (collision.CompareTag("PhallicStage"))
        {
            attackPower += 10;
            tenacity += 5;

        }
        if (collision.CompareTag("GrowingUp"))
        {
            maxHealth += 5;
            maxSpeed += 10;
          
        }
        if(collision.CompareTag("IncubationPeriod"))
        {
            luck += 5;
            defense += 5;
           
        }
        if (collision.CompareTag("ReproductiveOrgans"))
        {
            attackSpeed += 0.2f;
            crossroads += 0.25f;

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            rigid.gravityScale = 2;
        }
        if (collision.CompareTag("TestTag"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 2f, rigid.velocity.y);
        }
    }

    private void jump()
    {
        //Jump
        if (Input.GetButtonDown("Jump") && !ani.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            ani.SetBool("isJumping", true);
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
            time = 0;
            soundWave.transform.position = new Vector2(point.x, point.y);
            if (crossroads < Mathf.Sqrt(Mathf.Pow(point.x - this.transform.position.x, 2) + Mathf.Pow(point.y - this.transform.position.y, 2)))
            {

                float maxCrossroads = crossroads / Mathf.Sqrt(Mathf.Pow(point.x - this.transform.position.x, 2) + Mathf.Pow(point.y - this.transform.position.y, 2));

                soundWave.transform.position = new Vector2(this.transform.position.x + (point.x - this.transform.position.x) * maxCrossroads
                    , this.transform.position.y + (point.y - this.transform.position.y) * maxCrossroads);
            }

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
        if (key != 0 )
            transform.localScale = new Vector3(key, 1, 1);
    }
    private void upDown()
    {
        //Updown
        if (isLadder)
        {
            float ver = Input.GetAxis("Vertical");
            rigid.velocity = new Vector2(rigid.velocity.x , ver * maxSpeed);
        }
    }

    private void infancy()
    {
        //이동속도
        maxSpeed = 5 -2;
        //점프력
        jumpPower = 10 -2;
        //체력
        maxHealth = 1000000;
        health = 1000000;
        //공격력
        attackPower = 5;
        //지구력
        endurance = 50;
        //방어력
        defense = 15;
        //강인도
        tenacity = 200;
        //공격속도
        attackSpeed = 3;
        //사거리
        crossroads = 3;
        //행운
        luck = 50 + 20;

    }

    private void childhood()
    {
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
        if (isLadder && Input.GetButtonDown("Jump") /*&& !ani.GetBool("isJumping")*/) //이것도 점프라 올라가고 점프안되게 하려는중.
        {
            InvokeRepeating("InvokeJump", 0.01f, 0.01f);
            //ani.SetBool("isJumping", true);
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





    //경훈
    void openningMove()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x>maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x > maxSpeed*(-1))
        {
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
        }

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
                for(int i=0;i<raycastHit2Ds.Length;i++)
                {
                    RaycastHit2D hit = raycastHit2Ds[i];
                    if(hit.collider.tag=="monster")
                    {
                        
                        hit.collider.GetComponent<EnemyHit>().TakeDamage(totalAttackPower);
                    }
                }
            }

            //1/3지역 원형 공격 범위
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, crossroads / 3,enemyLayers);
            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "monster")
                {
                    Debug.Log("1/3지역 피격");
                    collider.GetComponent<EnemyHit>().IsCrossroadThird();
                    collider.GetComponent<EnemyHit>().TakeDamage(attackPower/3);
                }

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
