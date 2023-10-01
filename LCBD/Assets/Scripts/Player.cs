﻿using System.Collections;
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
    float nomalSpeed;
    public float jumpPower;
    SpriteRenderer spriteRenderer;
    bool isLadder;
    public bool downA, downD;
    public float downTime;
    public bool downAA, downDD;

    public float maxHealth;  //최대체력
    public float health;     //현재체력
    public int attackPower;    //공격력
    //지구력
    public int maxEndurance;   
    public float endurance;     
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

    Animator ani;    //애니메이션

    //인격 스택
    public int oralStack;
    private int analStack;
    private int phallicStack;
    private int growingUpStack;
    private int IncubationStack;
    private int genitalStack;


    SoundsPlayer SFXPlayer;
    BattleManager battleManager;

    internal object text_hp;
    internal object img;






    private void Awake()
    { 
        ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<CapsuleCollider2D>();

        //soundPlayer = SoundsPlayer.GetComponent<SoundsPlayer>();

        infancy();
        SFXPlayer = GameObject.Find("SFXPlayer").GetComponent<SoundsPlayer>();
        battleManager = GameObject.Find("battleManager").GetComponent<BattleManager>();
       
    }
    void Start()
    {
        
        //attackPosition = transform.right + new Vector3(0.2f, 0.2f, 0);
    }

    private void Update()
    {
        if (endurance > 0)
        {
            jump();
        }
        
         stopSpeed();

        switch (stage)
        {
            case 1:
                //attack();
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
        run();

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
            SFXPlayer.InteractionSound(0);  // Portal Sound
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        /*필드 포탈 텔레포트 코드*/
        if (collision.CompareTag("FieldPortal"))    //FieldPortal과 충돌했고,
        {
            if (Input.GetKeyDown(KeyCode.G) && PortalManager.IsTeleporting == false)    //상호작용(G)키를 눌렀고, 텔레포트가 아닌 중에
            {
                collision.GetComponent<FieldPortal>().Teleport(this.gameObject);        //필드 포탈을 태우고 플레이어를 텔레포트 시킴
                SFXPlayer.InteractionSound(0);  // Portal Sound
            }
        }
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
        //if (Input.GetButtonDown("Jump") && rigid.velocity.y == 0)
        if (Input.GetKey(KeySetting.keys[KeyInput.JUMP]) && rigid.velocity.y == 0)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            ani.SetTrigger("isJumping");
            endurance -= maxEndurance / 5;
            enduranceOnOff = false;

            SFXPlayer.JumpSound(0);     // Jump Sound 
        }

    }
    private void stopSpeed()
    {
        //stop speed
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 2f, rigid.velocity.y);
    }
    //private void attack()
    //{
    //    //attack
    //    time += Time.deltaTime;
    //    Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
    //    Input.mousePosition.y, -Camera.main.transform.position.z));
    //    if (time >= attackSpeed && Input.GetMouseButtonDown(0))
    //    {
    //        //쿨타임 추가 -지학-
    //        //Reset_CoolTime();
    //        time = 0;
    //        soundWave.transform.position = new Vector2(point.x, point.y);
    //        if (crossroads < Mathf.Sqrt(Mathf.Pow(point.x - this.transform.position.x, 2) + Mathf.Pow(point.y - this.transform.position.y, 2)))
    //        {

    //            float maxCrossroads = crossroads / Mathf.Sqrt(Mathf.Pow(point.x - this.transform.position.x, 2) + Mathf.Pow(point.y - this.transform.position.y, 2));

    //            soundWave.transform.position = new Vector2(this.transform.position.x + (point.x - this.transform.position.x) * maxCrossroads
    //                , this.transform.position.y + (point.y - this.transform.position.y) * maxCrossroads);
    //        }
    //        SFXPlayer.AttackSound(0);       //attack sound
    //        Instantiate(soundWave);
    //    }
    //}
    private void walk()
    {
        int key = 0;
        //if (Input.GetKey(KeyCode.A))
        if (Input.GetKey(KeySetting.keys[KeyInput.LEFT])) //KeyManager스크립트를 활용한 코드
        {
            key = -1;
            downA = true;
        }
        //if (Input.GetKey(KeyCode.D))
        if (Input.GetKey(KeySetting.keys[KeyInput.RIGHT])) //KeyManager스크립트를 활용한 코드
        {
            key = 1;
            downD = true;
        }

        float speedx = Mathf.Abs(this.rigid.velocity.x);

        if ((downTime > 0.5 && key == 0) || endurance == 0)
        {
            downA = false;
            downD = false;
            downAA = false;
            downDD = false;
            downTime = 0;
        }
        if (downAA || downDD)
        {
            maxSpeed = nomalSpeed * 1.4f;
            ani.SetBool("isRunning", true);
            enduranceOnOff = false;
            endurance -= 0.08f;
        }
        else
        {
            maxSpeed = nomalSpeed;
            ani.SetBool("isRunning", false);
        }

        if (speedx < maxSpeed)
            this.rigid.AddForce(transform.right * key * maxSpeed * 10);

        //스프라이트 반전
        if (key != 0 && stage == 1)
            transform.localScale = new Vector3(key, 1, 1);
        if (key != 0 && stage != 1)
            transform.localScale = new Vector3(-key * 1.5f, 1.5f, 0);

        if (key == 0)
            ani.SetBool("isWalking", false);
        else
            ani.SetBool("isWalking", true);

        // Walk Sound 
        if (stage == 1)
            SFXPlayer.WalkSound(0); 
        else if (stage == 2)
            SFXPlayer.WalkSound(1);  



    }

    private void run()
    {
        if (stage != 1)
        {
            if (downA || downD)
            {
                downTime += Time.deltaTime;
            }
            //if (downTime > 0.01 && downA && Input.GetKeyDown(KeyCode.A))
            if (downTime > 0.01 && downA && Input.GetKeyDown(KeySetting.keys[KeyInput.LEFT])) //KeyManager스크립트를 활용한 코드
                downAA = true;
           // if (downTime > 0.01 && downD && Input.GetKeyDown(KeyCode.D))
            if (downTime > 0.01 && downD && Input.GetKeyDown(KeySetting.keys[KeyInput.RIGHT])) //KeyManager스크립트를 활용한 코드
                downDD = true;
        }
    }
    private void upDown()
    {
        //Updown
        if (isLadder)
        {
            SFXPlayer.LadderSound(0);       // Jump Sound
            float ver = Input.GetAxis("Vertical");
            rigid.velocity = new Vector2(rigid.velocity.x , ver * maxSpeed);
            if(ver != 0)
                ani.SetBool("isLadder", true);
            
        }
    }
    private void infancy()
    {
        //이동속도
        nomalSpeed = 5 -2;
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
        nomalSpeed += 2;
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
        nomalSpeed += 0.5f;
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
        nomalSpeed -= 3;
        //점프력
        jumpPower -= 3;
    }

    private void ladderJump()
    {
        //if (isLadder && Input.GetButtonDown("Jump"))
        if (isLadder && Input.GetKey(KeySetting.keys[KeyInput.JUMP])) //KeyManager스크립트를 활용한 코드
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



}
