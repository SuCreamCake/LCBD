using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

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
    public float attackSpeed;
    //사거리
    public int crossroads;
    //행운
    public int luck;
    //음파 오브젝트
    public GameObject soundWave;
    private float time = 0;
    //스테이지
    public int stage;
    public string sceneName;
    //공격속도를 체크하기 위한 변수
    public float attackTime = 0;






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



        ladderJump();




        switch (stage)
        {
            case 1:
                attack();
                //동주 맨손 테스트 추후에 삭제 하면 됨
                punchAttack();
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
        if (collision.CompareTag("Potal") && stage == 1)
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
    
    //맨손 공격
    public void punchAttack()
    {
        attackTime += Time.deltaTime;
        if(attackTime > (attackSpeed / 2) && Input.GetMouseButtonDown(0))
        {
            attackTime = 0;
            //마우스의 위치 가져오기
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = camera.ScreenToWorldPoint(mousePoint);
            //현재 캐릭터의 위치 가져오기
            Vector2 characterPoint = new Vector2(this.transform.position.x, this.transform.position.y);
            //원형레이캐스팅  시작점
            float startX = (mousePoint.x + characterPoint.x) / 2;
            float startY = (mousePoint.y + characterPoint.y) / 2;
            //원형레이캐스팅
            Vector3 startAttackPoint = new Vector3(startX, startY, 0);
            RaycastHit2D raycastHit = Physics2D.CircleCast(startAttackPoint,1/3,Vector2.up,0);
            Debug.DrawRay(mousePoint, transform.forward * 10, Color.red, 0.3f);
        }
    }


    //유아기 특수공격
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
            rigid.velocity = new Vector2(rigid.velocity.x, ver * maxSpeed);
        }
    }

    private void infancy()
    {
        //이동속도
        maxSpeed = 5 - 2;
        //점프력
        jumpPower = 10 - 2;
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
}