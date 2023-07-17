using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class Player : MonoBehaviour
{
    public Camera camera;
    //TalkManage talkManger;
    Rigidbody2D rigid;
    //�̵��ӵ�
    public float maxSpeed;  
    public float jumpPower;
    SpriteRenderer spriteRenderer;
    bool isLadder;
    //ü��
    public int health;
    //���ݷ�
    public int attackPower;
    //������
    public int endurance;
    //����
    public int defense;
    //���ε�
    public int tenacity;
    //���ݼӵ�
    public float attackSpeed;
    //��Ÿ�
    public int crossroads;
    //���
    public int luck;
    //���� ������Ʈ
    public GameObject soundWave;
    private float time = 0;
    //��������
    public int stage;
    public string sceneName;

    //�ִϸ��̼�
    Animator ani;
    

    private void Awake()
    {

        //camera = GameObject.Find("Main Camera").GetComponent<Camera>();  
        ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        infancy();
        

    }

    private void Update()
    {
        AnimationMotion();


        jump();
        stopSpeed();
        directionSprite();
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
                    SceneManager.LoadScene(sceneName);
                    sceneName = "stage3";
                    break;
                case 2:
                    stage = 3;
                    adolescence();
                    SceneManager.LoadScene(sceneName);
                    sceneName = "stage4";
                    break;
                case 3:
                    stage = 4;
                    adulthood();
                    SceneManager.LoadScene(sceneName);
                    sceneName = "stage5";
                    break;
                case 4:
                    stage = 5;
                    oldAge();
                    SceneManager.LoadScene(sceneName);
                    break;
            }
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
    private void directionSprite()
    {
        //Direction Sprite
        //ĳ������ sprite�� �̵��ϴ� ������ �ٶ󺸵��� ����
        if (Input.GetButtonDown("Horizontal")) //������ �ٶ󺼶�
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
    }
    private void attack()
    {
        //attack
        time += Time.deltaTime;
        Vector3 point = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
        Input.mousePosition.y, -camera.transform.position.z));
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
        //Move Speed
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //maxSpeed
        if (rigid.velocity.x > maxSpeed) //rigidbody�� ����ӵ��� �ְ�ӵ����� �������
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); //�ְ�ӵ��� ���ѱ�� ����
        else if (rigid.velocity.x < (-1) * maxSpeed) //-1�� ���ϸ�(����) �����϶�
            rigid.velocity = new Vector2((-1) * maxSpeed, rigid.velocity.y);
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
}
