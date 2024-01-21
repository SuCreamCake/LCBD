using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMove : MonoBehaviour
{
    public float speed = 5f; // �̵� �ӵ� ����
    public float maxHealth =100;  //�ִ�ü��
    public float health = 80;     //����ü��
    public int attackPower = 10;    //���ݷ�
    //������
    public int maxEndurance = 10;
    public float endurance = 50;   //���׹̳�  
    public int enduranceOnOff = 0;
    public float stayTime = 0;
    public int enduranceRec =0;
    public bool drained = false;

    public int defense = 0;    //����
    public int tenacity = 0;    //���ε�
    public float attackSpeed = 5;    //���ݼӵ�
    public float crossroads = 5;    //��Ÿ�
    public float luck = 0;    //���

    float nomalSpeed = 5; //�̵��ӵ�

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxHealth = 100;
        health = 90;
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

    public void addSpeed(float addSpeed) //�̵��ӵ� �߰� ����
    {
        this.nomalSpeed += addSpeed;
    }

    void Jump()
    {
        // ���� ���� ����
        // ���⼭�� ������ Rigidbody2D�� AddForce�� ����Ͽ� ���� ���� ���ϴ� ������� ������ �����Ͽ����ϴ�.
        rb.AddForce(new Vector2(0f, 50f), ForceMode2D.Impulse);
    }
}