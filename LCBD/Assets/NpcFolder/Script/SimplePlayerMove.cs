using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMove : MonoBehaviour
{
    public float speed = 5f; // 이동 속도 설정

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    void Jump()
    {
        // 점프 로직 구현
        // 여기서는 간단히 Rigidbody2D의 AddForce를 사용하여 위로 힘을 가하는 방식으로 점프를 구현하였습니다.
        rb.AddForce(new Vector2(0f, 50f), ForceMode2D.Impulse);
    }
}