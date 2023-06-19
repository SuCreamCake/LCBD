using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera camera;
    Rigidbody2D rigid;
    //이동속도
    public float maxSpeed;  
    public float jumpPower;
    SpriteRenderer spriteRenderer;
    bool isLadder;
    //체력
    public int health;
    //공격력
    public int attackPower;
    //지구력
    public int endurance;
    //방어력
    public int defense;
    //강인도
    public int tenacity;
    //공격속도
    public int attackSpeed;
    //사거리
    public int crossroads;
    //행운
    public int luck;

    //음파 오브젝트
    private float time = 0;
    public GameObject soundWave;
    //스테이지
    public int stage;

    


    private void Awake()
    {
        //camera = GameObject.Find("Main Camera").GetComponent<Camera>();  
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(stage == 1)
            infancy();
    }

    private void Update()
    {
        jump();
        stopSpeed();
        directionSprite();
        switch (stage)
        {
            case 1:
                attack();
                break;
            default:
                break;
        }
    }


    private void FixedUpdate()
    {
        work();

        upDown();

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
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
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
        Debug.Log(time);
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
    private void work()
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
            rigid.velocity = new Vector2(rigid.velocity.x, ver * maxSpeed);
        }
    }

    private void infancy()
    {
        //이동속도
        maxSpeed = 3;
        //점프력
        jumpPower = 8;
        //체력
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
        attackSpeed = 2;
        //사거리
        crossroads = 3;
        //행운
        luck = 50 + 20;

    }

    private void childhood()
    {

    }
}
