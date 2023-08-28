using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMove : MonoBehaviour
{
    public float speed = 5f; // �̵� �ӵ� ����

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // ���� �̵� �Է� �ޱ�
        float moveY = Input.GetAxis("Vertical"); // ���� �̵� �Է� �ޱ�

        Vector2 movement = new Vector2(moveX, moveY);
        rb.velocity = movement * speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        // ���� ���� ����
        // ���⼭�� ������ Rigidbody2D�� AddForce�� ����Ͽ� ���� ���� ���ϴ� ������� ������ �����Ͽ����ϴ�.
        rb.AddForce(new Vector2(0f, 50f), ForceMode2D.Impulse);
    }
}