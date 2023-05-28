using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    //�̵��ӵ�
    public float maxSpeed = 3;  
    public float jumpPower;
    SpriteRenderer spriteRenderer;
    bool isLadder;
    //ü��
    public int health = 1000000;
    //���ݷ�
    public int attackPower = 5;
    //������
    public int endurance = 50;
    //����
    public int defense = 15;
    //���ε�
    public int tenacity = 200;
    //���ݼӵ�
    public int attackSpeed= 2;
    //��Ÿ�
    public int crossroads = 3;
    //���
    public int luck = 50;

    //���� ������Ʈ
    public GameObject soundWave;



    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        infancy();

    }

    private void Update()
    {
        //Jump
        if (Input.GetButtonDown("Jump"))
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        //stop speed
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        //Direction Sprite
        if (Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        //attack
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, -Camera.main.transform.position.z));
        if (Input.GetMouseButtonDown(0))
        {
            soundWave.transform.position = new Vector2(point.x, point.y);
            if( crossroads < Mathf.Sqrt(Mathf.Pow(point.x, 2) + Mathf.Pow(point.y, 2)))
            {
                float maxCrossroads = crossroads / Mathf.Sqrt(Mathf.Pow(point.x, 2) + Mathf.Pow(point.y, 2));
                soundWave.transform.position = new Vector2(point.x * maxCrossroads, point.y * maxCrossroads);
            }
                
            Instantiate(soundWave);
        }
    }

    private void FixedUpdate()
    {
        //Move Speed
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //maxSpeed
        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < (-1) * maxSpeed)
            rigid.velocity = new Vector2((-1) * maxSpeed, rigid.velocity.y);

        if (isLadder)
        {
            float ver = Input.GetAxis("Vertical");
            rigid.velocity = new Vector2(rigid.velocity.x, ver * maxSpeed);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            rigid.gravityScale = 0;
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

    private void infancy()
    {
        //�̵��ӵ�
        maxSpeed = 3;
        //������
        jumpPower = 8;
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
        attackSpeed = 2;
        //��Ÿ�
        crossroads = 3;
        //���
        luck = 50 + 20;
    }
}
