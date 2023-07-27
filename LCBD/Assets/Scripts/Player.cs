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
    public float luck;
    //���� ������Ʈ
    public GameObject soundWave;
    private float time = 0;
    //��������
    public int stage;

    //�ִϸ��̼�
    Animator ani;
    

    private void Awake()
    { 
        ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        infancy();
        

    }

    private void Update()
    {
        //AnimationMotion();

        jump();
        stopSpeed();

        switch (stage)
        {
         case 1:
            attack();
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
        if (Input.GetButtonDown("Jump") && rigid.velocity.y == 0)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            ani.SetTrigger("isJumping");
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
        if (key != 0)
            transform.localScale = new Vector3(key, 1, 1);

        if (key == 0)
            ani.SetBool("isWalking", false);
        else
            ani.SetBool("isWalking", true);

        
    }
    private void upDown()
    {
        //Updown
        if (isLadder)
        {
            float ver = Input.GetAxis("Vertical");
            rigid.velocity = new Vector2(rigid.velocity.x , ver * maxSpeed);
            if (Mathf.Abs(ver) != 0)
                ani.SetBool("isLadder", true);
        }
        else
            ani.SetBool("isLadder", false);
    }

    private void infancy()
    {
        //�̵��ӵ�
        maxSpeed = 3;
        //������
        jumpPower = 8;
        //ü��
        maxHealth = 30;
        health = 30;
        //���ݷ�
        attackPower = 15;
        //������
        endurance = 40;
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

    //private void AnimationMotion()
    //{
    //    if (Mathf.Abs(rigid.velocity.normalized.x) < 0.2)
    //    {
    //        ani.SetBool("isRunning", false);
    //    }
    //    else
    //    {
    //        ani.SetBool("isRunning", true);
    //    }

    //    if (rigid.velocity.y < 0)
    //    {
    //        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
    //        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("platform"));
    //        if (rayHit.collider != null)
    //        {
    //            if (rayHit.distance < 0.5f)
    //            {
    //                //Debug.Log("���� ��");
    //                ani.SetBool("isJumping", false);
    //            }
    //        }
    //    }
    //}



}
