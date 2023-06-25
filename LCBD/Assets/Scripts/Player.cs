using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Camera camera;
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

    
    

    


    private void Awake()
    {
        //camera = GameObject.Find("Main Camera").GetComponent<Camera>();  
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        switch (stage)
        {
            case 1:
                infancy();
                break;
            case 2:
                childhood();
                break;
            case 3:
                adolescence();
                break;
            case 4:
                adulthood();
                break;
            case 5:
                oldAge();
                break;
            default:
                break;
        }

    }

    private void Update()
    {
        jump();
        stopSpeed();
        directionSprite();


        
        if (isLadder && Input.GetButtonDown("Jump"))
        {
            transform.Translate(0, 3, 0);  
        }




        switch (stage)
        {
            case 1:
                attack();
                break;
            case 4:
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
        if (collision.CompareTag("Potal")&& stage == 1)
        {
            stage = 2;
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            rigid.gravityScale = 2;
        }
    }

    private void jump()
    {
        //Jump
        if (Input.GetButtonDown("Jump"))
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
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
        if (Input.GetButtonDown("Horizontal"))
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
        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < (-1) * maxSpeed)
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
}
