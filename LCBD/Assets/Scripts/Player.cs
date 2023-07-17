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

    //애니메이션
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
        //캐릭터의 sprite가 이동하는 방향을 바라보도록 설정
        if (Input.GetButtonDown("Horizontal")) //왼쪽을 바라볼때
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
        if (rigid.velocity.x > maxSpeed) //rigidbody의 현재속도가 최고속도보다 높을경우
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); //최고속도를 못넘기게 설정
        else if (rigid.velocity.x < (-1) * maxSpeed) //-1을 곱하면(음수) 왼쪽일때
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
        //이동속도
        maxSpeed = 5 -2;
        //점프력
        jumpPower = 10 -2;
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
}
