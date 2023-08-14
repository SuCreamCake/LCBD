using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class Player : MonoBehaviour
{
    //TalkManage talkManger;
    Rigidbody2D rigid;
    //�̵��ӵ�
    public float maxSpeed;  
    public float jumpPower;
    SpriteRenderer spriteRenderer;
    bool isLadder;
    //�ִ�ü��
    public int maxHealth;
    //����ü��
    public int health;
    //���ݷ�
    public int attackPower;
    //�ִ�������
    public int maxEndurance;
    //������
    public int endurance;
    //����
    public int defense;
    //���ε�
    public int tenacity;
    //���ݼӵ�
    public float attackSpeed;
    //��Ÿ�
    public float crossroads;
    //���
    public int luck;
    //���� ������Ʈ
    public GameObject soundWave;
    private float time = 0;
    //��������
    public int stage;

    //�ִϸ��̼�
    Animator ani;
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

        //��������Ʈ ����
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
        //�̵��ӵ�
        maxSpeed = 5 -2;
        //������
        jumpPower = 10 -2;
        //ü��
        maxHealth = 1000000;
        health = 1000000;
        //���ݷ�
        attackPower = 5;
        //������
        endurance = 50;
        //����
        defense = 15;
        //���ε�
        tenacity = 200;
        //���ݼӵ�
        attackSpeed = 3;
        //��Ÿ�
        crossroads = 3;
        //���
        luck = 50 + 20;

    }

    private void childhood()
    {
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
        if (isLadder && Input.GetButtonDown("Jump") /*&& !ani.GetBool("isJumping")*/) //�̰͵� ������ �ö󰡰� �����ȵǰ� �Ϸ�����.
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





    //����
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
                for(int i=0;i<raycastHit2Ds.Length;i++)
                {
                    RaycastHit2D hit = raycastHit2Ds[i];
                    if(hit.collider.tag=="monster")
                    {
                        
                        hit.collider.GetComponent<EnemyHit>().TakeDamage(totalAttackPower);
                    }
                }
            }

            //1/3���� ���� ���� ����
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, crossroads / 3,enemyLayers);
            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "monster")
                {
                    Debug.Log("1/3���� �ǰ�");
                    collider.GetComponent<EnemyHit>().IsCrossroadThird();
                    collider.GetComponent<EnemyHit>().TakeDamage(attackPower/3);
                }

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
