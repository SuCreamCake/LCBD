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
    //�̵��ӵ�
    public float maxSpeed;  
    public float jumpPower;
    SpriteRenderer spriteRenderer;
    bool isLadder;
    public float maxHealth;  //�ִ�ü��
    public float health;     //����ü��
    public int attackPower;    //���ݷ�
    public int maxEndurance;    //�ִ�������
    //������
    public int endurance;
    public bool enduranceOnOff;
    public float stayTime;
    public int enduranceRec;

    public int defense;    //����
    public int tenacity;    //���ε�
    public float attackSpeed;    //���ݼӵ�
    public float crossroads;    //��Ÿ�
    public float luck;    //���
    //���� ������Ʈ
    public GameObject soundWave;
    private float time = 0;
    public int stage;    //��������
    new CapsuleCollider2D collider2D;    //������ ������ ���� �ݶ��̴�

    /*���� �߰�*/
    //��Ÿ�� �ؽ�Ʈ
    public Text text_CoolTime;
    //��Ÿ�� �̹���
    public Image image_fill;
    //��ų ������� �����ð�
    private float time_current;
    //time.Time�� ���ؼ� time 
    private float time_start;
    private bool isEnded = true;
    //hp�� �ؽ�Ʈ
    public Text text_hp;
    //hp�� �̹���
    public Image img;

    Animator ani;    //�ִϸ��̼�

    //�ΰ� ����
    public int oralStack;
    private int analStack;
    private int phallicStack;
    private int growingUpStack;
    private int IncubationStack;
    private int genitalStack;

    //���� ������Ʈ
    public GameObject SoundsPlayer;
    //���� ����
    //���ݼӵ��� üũ�ϱ� ���� ����
    public float attackTime = 0;
    //���Ÿ� ���� ������Ʈ
    public GameObject bulletObject;
    //���� ��ü�� ��� �ڷ���
    public GameObject[] weapons;
    //�Ǽ� ����, ��������, ���Ÿ� ���� �ε���
    bool sDown1;
    bool sDown2;
    bool sDown3;
    bool sDown4;
    //���� �ε���
    public int weaponeIndex = -1;
    //������ġ
    public Transform attackPosition;

    GameObject equipWeapon;
    //���� ������Ʈ
    public GameObject shieldObject;
    //���� ���̺� ����
    public GameObject soundWaveAttackObject;
    //�� ���̾� ����ũ
    public LayerMask enemyLayers;
    //���� ���� �ð�
    public float soundWaveAttackTime = 0;
    //�� ���ݷ�
    public int totalAttackPower;
    //�� ��
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
            //��Ÿ�� �߰� -����-
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



        //��������Ʈ ����
        if (key != 0 && stage == 1)
        {
            transform.localScale = new Vector3(key, 1, 1);
            //������ �׽�Ʈ
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
        //�̵��ӵ�
        maxSpeed = 5 -2;
        //������
        jumpPower = 10 -2;
        //ü��
        maxHealth = 30;
        health = 30;
        //���ݷ�
        attackPower = 5;
        //������
        maxEndurance = 40;
        endurance = 40;
        enduranceRec = 4;
        //����
        defense = 50;
        //���ε�
        tenacity = 80;
        //���ݼӵ�
        attackSpeed = 3;
        //��Ÿ�
        crossroads = 3;
        //���
        luck = 15 + 20;
        
    }

    private void childhood()
    {
        //������ ����
        transform.localScale = new Vector3(1.5f, 1.5f, 0);
        collider2D.size = new Vector3(0.4f, 0.8f,0);
        //�̵��ӵ�
        maxSpeed += 2;
        //������
        jumpPower += 2;
        //���
        luck -= 20;
        //���ݷ�
        attackPower += 15;
        //��Ÿ�
        crossroads += 2;
        //�ִϸ��̼�
        ani.SetTrigger("isChildhood");
    }

    private void adolescence()
    {
        //���ݷ�
        attackPower -= 15;
        //��Ÿ�
        crossroads -= 2;
        //�̵��ӵ�
        maxSpeed += 0.5f;
        //������
        jumpPower += 1;
    }
    private void adulthood()
    {
        //���ݼӵ�
        attackSpeed += 1.7f;

    }

    private void oldAge()
    {
        //�̵��ӵ�
        maxSpeed -= 3;
        //������
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
                enduranceRec = 4 + 4 * (oralStack / 2);    //������ Ư���ɷ�
            //�ΰ� ������ ���� ���� ������ 10�� �ʴ� ȸ���Ǵ� ������ 4 ����

        }
        if (collision.CompareTag("AnalStage"))
        {
            defense += 10;
            tenacity += 10;
            analStack++;

            //�׹��� Ư�� �ɷ�
            //������ 10%��ŭ ���ε� �ִ�ġ ����
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
            //���ٱ� Ư�� �ɷ�
            //�ΰ� �������� ���� ���ݷ� 10% ����
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
            //����� Ư�� �ɷ�
            //(���� �ΰ� ���� * 5)��ŭ ����� �ִ�ġ ����
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
            //�ẹ�� Ư�� �ɷ�
            //�ǰ� �� ����� Ȯ���� �� 10% ����
            //�ǰ� �̱������� ����
        }
        if (collision.CompareTag("GenitalStage"))
        {
            attackSpeed += 0.2f;
            crossroads += 0.25f;
            genitalStack++;
            //���ı� Ư�� �ɷ�
            //���ݼӵ� 1�� ���ݷ� 20 ����
            if (genitalStack == 5)
                attackPower += (int)attackSpeed * 20;
            if (genitalStack > 5)
                attackPower = attackPower - (int)(attackSpeed - 1) * 20 + (int)attackSpeed * 20;
        }
        
    }


    /*����*/
    //image_fill�� fillAmount�� 360�� �ð� �ݴ� �������� ȸ���ϰ� ����
    private void Init_UI()
    {
        image_fill.type = Image.Type.Filled;
        image_fill.fillMethod = Image.FillMethod.Radial360;
        image_fill.fillOrigin = (int)Image.Origin360.Top;
        image_fill.fillClockwise = false;
    }
    //��Ÿ�� ����
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
    //��Ÿ���� ������ ��ų ������ �������� ����
    private void End_CoolTime()
    {
        Set_FillAmount(0);
        isEnded = true;
        text_CoolTime.gameObject.SetActive(false);
    }
    //��Ÿ�� Ÿ�̸� ����
    private void Reset_CoolTime()
    {
        text_CoolTime.gameObject.SetActive(true);
        time_current = attackSpeed;
        time_start = Time.time;
        Set_FillAmount(attackSpeed);
        isEnded = false;
    }
    //��ų ���� �ð� �ð�ȭ
    private void Set_FillAmount(float _value)
    {
        image_fill.fillAmount = _value / attackSpeed;
        string txt = _value.ToString("0.0");
        text_CoolTime.text = txt;
    }
    //HP�� ���� UI ǥ�� �ʱ�ȭ
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
    //hp���� �Ű� ������ ���� float ���� ����
    private void Change_HP(float _value)
    {
        health += _value;
        Set_HP(health);
    }
    //hp�� �Ű������� ���� float ������ ����
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
                    //Debug.Log("���� ��");
                    ani.SetBool("isJumping", false);
                }
            }
        }
    }

    //����
    //�Ǽ� ����
    public void punchAttack()
    {
        if (attackTime > (attackSpeed / 2) && Input.GetMouseButtonDown(0))
        {
            attackTime = 0;
            //���콺�� ��ġ ��������
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);
            //���� ĳ������ ��ġ ��������
            Vector2 characterPoint = new(transform.position.x, transform.position.y);
            //startX, startY��ǥ ���ϱ� ����, �Ÿ��� ����
            float rangeRadius = crossroads / 6.0f; //���� ������ 1/3 1/2 == 1/6
            float rangeRadian = Mathf.Atan2(mousePoint.y - characterPoint.y, mousePoint.x - characterPoint.x);
            //��������ĳ����  ������ (=�߽���)
            float startX = characterPoint.x + rangeRadius * Mathf.Cos(rangeRadian);
            float startY = characterPoint.y + rangeRadius * Mathf.Sin(rangeRadian);
            //��������ĳ����
            Vector2 startAttackPoint = new(startX, startY);
            //���� ������ ���̾ �߰��ϰ� �ش� ���̾ �����ϵ��� ���̾� �߰��ϰ� ���̾� ���� ���� ���� �ʿ� (���� ���̾ ���� ����) (�ϴ� �÷��̾� ������ ��� ���̾� ����)
            //int layerMask = 1 << LayerMask.NameToLayer("monster");
            ////layerMask = ~layerMask;
            //RaycastHit2D raycastHit = Physics2D.CircleCast(startAttackPoint, rangeRadius, Vector2.right, 0f, layerMask);
            //if (raycastHit.collider != null)    //��� �����Ǹ�
            //{
            //    Debug.Log("�Ǽ� ���ݿ� ������ ��� ������Ʈ: " + raycastHit.collider.gameObject.tag);
            //    //��¥ �����ؼ� ������ ��� ü�� ����ֱ�
            //}

            //Collider2D hitEnemys = Physics2D.OverlapCircle(startAttackPoint,rangeRadius,enemyLayers);

            RaycastHit2D raycastHit = Physics2D.CircleCast(startAttackPoint, rangeRadius, Vector2.right, 0f, enemyLayers);
            if (raycastHit.collider != null)
            {
                CalDamage();
                raycastHit.collider.GetComponent<EnemyHit>().TakeDamage(totalAttackPower);
                Debug.Log("���� ����");

            }
                

            //��ġ �����
            Debug.Log("mousePoint: " + mousePoint);
            Debug.Log("characterPoint: " + characterPoint);
            Debug.Log("rangeRadian: " + rangeRadian);
            Debug.Log("startAttackPoint: " + startAttackPoint);

            //����ĳ��Ʈ ���� �׸��� ����׿� ���� ����
            Debug.DrawRay(characterPoint, new Vector2(rangeRadius * Mathf.Cos(rangeRadian), rangeRadius * Mathf.Sin(rangeRadian)).normalized * crossroads, Color.white, 0.3f);      //ĳ���� ���� ~ ���� ��Ÿ�
            Debug.DrawRay(characterPoint, new Vector2(rangeRadius * Mathf.Cos(rangeRadian), rangeRadius * Mathf.Sin(rangeRadian)).normalized * crossroads / 3f, Color.green, 0.3f); //ĳ���� ���� ~ �Ǽ� ��Ÿ�
            Debug.DrawRay(characterPoint, new Vector2(rangeRadius * Mathf.Cos(rangeRadian), rangeRadius * Mathf.Sin(rangeRadian)).normalized * rangeRadius, Color.black, 0.3f);     //ĳ���� ���� ~ �� ���� �������� �Ÿ�
            Debug.DrawRay(startAttackPoint, Vector2.up * rangeRadius, Color.red, 0.3f);                     //���� �� ���� ����
            Debug.DrawRay(startAttackPoint, Vector2.down * rangeRadius, Color.red, 0.3f);                   //���� �� �Ʒ��� ����
            Debug.DrawRay(startAttackPoint, Vector2.right * rangeRadius, Color.red, 0.3f);                  //���� �� ������ ����
            Debug.DrawRay(startAttackPoint, Vector2.left * rangeRadius, Color.red, 0.3f);                   //���� �� ���� ����
            Debug.DrawRay(startAttackPoint, Vector2.one.normalized * rangeRadius, Color.red, 0.3f);         //���� �� ����� �밢�� ����
            Debug.DrawRay(startAttackPoint, new Vector2(1, -1).normalized * rangeRadius, Color.red, 0.3f);  //���� �� ������ �밢�� ����
            Debug.DrawRay(startAttackPoint, new Vector2(-1, 1).normalized * rangeRadius, Color.red, 0.3f);  //���� �� �»��� �밢�� ����
            Debug.DrawRay(startAttackPoint, -Vector2.one.normalized * rangeRadius, Color.red, 0.3f);        //���� �� ������ �밢�� ����
        }
    }
    //���� ����
    private void meleeAttack()
    {
        if (attackTime > attackSpeed && Input.GetMouseButtonDown(0))
        {
            attackTime = 0;
            //���콺�� ��ġ ��������
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

            //���ݹ���
            Vector2 attackForce = mousePoint - (Vector2)transform.position;
            attackForce = attackForce.normalized;

            //���� ����
            float xRange = crossroads * 0.3f;
            float yRange = 0.5f;
            Vector2 boxSize = new Vector2(xRange, yRange);

            float angle = Mathf.Atan2(attackForce.y, attackForce.x) * Mathf.Rad2Deg;
            //���� �ݶ��̴� ����
            Collider2D[] colliders = Physics2D.OverlapBoxAll(attackPosition.position, boxSize , angle, enemyLayers);
            Debug.Log(angle);
            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.tag);
                if(collider.tag=="monster")
                {
                    Debug.Log("���� ����");
                    collider.GetComponent<EnemyHit>().TakeDamage(totalAttackPower);
                }
            }
            Debug.Log("���ݽ���");
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(attackPosition.position,new Vector2(crossroads * 0.3f, 0.7f));
    }


    //���Ÿ����� �޼���
    private void longDistanceAttack()
    {
        if (attackTime > attackSpeed && Input.GetMouseButtonDown(0))
        {
            attackTime = 0;
            //���콺�� ��ġ ��������
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

    //�������� Ű �Է�
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
            Debug.Log("F����");
        }
    }
    //���� Ÿ�� �ε���
    private void swapWeapon()
    {
        if (sDown1)
        {
            weaponeIndex = 0;
            Debug.Log("��ư 1Ȱ��ȭ");
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

    //��� ��� �޼���
    private void shield()
    {
        if (Input.GetMouseButtonDown(0))
        {
            equipWeapon.SetActive(true);
            //���콺�� ��ġ ��������
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);
            Vector3 playerPos = transform.position;
            Vector2 direVec = mousePoint - (Vector2)playerPos;
            direVec = direVec.normalized;
            equipWeapon.transform.position = direVec+(Vector2)transform.position;
            
            //������ ���
            if (Vector3.Dot(transform.right, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
            {
                Debug.Log("���� ���� ����");
                equipWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            //������ ���
            else if (Vector3.Dot(transform.up, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
            {
                Debug.Log("�������� ����");
                equipWeapon.transform.rotation = Quaternion.Euler(0, 0, 90f);
            }
            else if (Vector3.Dot(-transform.right, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
            {
                Debug.Log("���� ���� ����");
                equipWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);


            }
            else
            {
                Debug.Log("�ϴ� ���� ����");
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
            //���콺�� ��ġ ��������
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

            //���ݹ���
            Vector2 attackForce = mousePoint - (Vector2)transform.position;
            attackForce = attackForce.normalized;
            Debug.Log(attackPower);
            float startAngle = -attackPower / 2;

            //�Ϲ� ���� ����
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

            //1/3���� ���� ���� ����
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, crossroads / 3, enemyLayers);
            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "monster")
                {
                    Debug.Log("1/3���� �ǰ�");
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
    //������ ����
    private void CalDamage()
    {
        totalAttackPower+=0/*���� ���ݷ� ���� �� ����*/;
    }
    //��� ����
    private void TotalShield()
    {
        totalShield += 0;/*���⵵ ����������*/;
    }
}
