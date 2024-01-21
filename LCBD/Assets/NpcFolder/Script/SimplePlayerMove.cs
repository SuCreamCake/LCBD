using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMove : MonoBehaviour
{
    public float speed = 5f; // 이동 속도 설정
    public float maxHealth =100;  //최대체력
    public float health = 80;     //현재체력
    public int attackPower = 10;    //공격력
    //지구력
    public int maxEndurance = 10;
    public float endurance = 50;   //스테미나  
    public int enduranceOnOff = 0;
    public float stayTime = 0;
    public int enduranceRec =0;
    public bool drained = false;

    public int defense = 0;    //방어력
    public int tenacity = 0;    //강인도
    public float attackSpeed = 5;    //공격속도
    public float crossroads = 5;    //사거리
    public float luck = 0;    //행운

    float nomalSpeed = 5; //이동속도

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxHealth = 100;
        health = 90;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // 수평 이동 입력 받기
        float moveY = Input.GetAxis("Vertical"); // 수직 이동 입력 받기

        Vector2 movement = new Vector2(moveX, moveY);
        rb.velocity = movement * speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void addSpeed(float addSpeed) //이동속도 추가 로직
    {
        this.nomalSpeed += addSpeed;
    }

    void Jump()
    {
        // 점프 로직 구현
        // 여기서는 간단히 Rigidbody2D의 AddForce를 사용하여 위로 힘을 가하는 방식으로 점프를 구현하였습니다.
        rb.AddForce(new Vector2(0f, 50f), ForceMode2D.Impulse);
    }
}