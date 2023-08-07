using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openning : MonoBehaviour
{
    public TalkManage talkmanager; //��Ŵ���
    public BGscroll scroll1,scroll2,scroll3,scroll4; //��潺ũ�� ���ǵ弳��
    public GameObject Hole;
    public GameObject talkNext;
    SpriteRenderer render; //�÷��̾� ������
    int speed=7; //�÷��̾� �������� �ӵ�
    Animator ani; //�ִϸ��̼�
    Vector3 startPos;
    bool state; //����




    private void Awake()
    {
        render = GetComponent<SpriteRenderer>(); //�÷��̾� ������
        ani = GetComponent<Animator>(); //�ִϸ��̼�
       // scrollspeed.isMove = false; //����� �����̴� �ӵ�
        startPos = transform.position; //�÷��̾��� ������ġ�� �����ص�.
        state = false;
    }

    void Update()
    {
        OpenningAction();
        if (state)
            playerDown();
    }

    private void playerDown() //�÷��̾ �������� �޼ҵ�
    {
        talkmanager.isTalk = false;
        Vector3 curPos = transform.position; //���� ��ġ�� �����´�
        Vector3 downPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + downPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Potal"))
        {
            Debug.Log("Ȧ�� ��.");
            transform.position = startPos;
            state = false;
            speed = 0; //��ȭ�� �ȳѱ�� state�� true���¸� �����ϰԵǼ� �ϴ� �������� �ӷ���0���� �����ó�����̰�
            Hole.SetActive(false);
            scroll1.isMove = true;
            scroll2.isMove = true;
            scroll3.isMove = true;
            scroll4.isMove = true;
            ++talkmanager.clickCount; 
            //���⼭ �ٲ���� �ؽ�Ʈ�� �ٲ�...���� ++�Ѵ��ص� talkmanager�� update�� �����̾Ȱ�....
            talkmanager.TalkText.text = "���� ���� �������� ���ΰ�..\n" +
                "������ ������ ���� ��������.";
            talkmanager.isTalk = true;
        }
        if (collision.CompareTag("TestTag"))
        {
            //���⼭ �ٲ���� �ؽ�Ʈ�� �ٲ�...���� ++�Ѵ��ص� talkmanager�� update�� �����̾Ȱ�....
            talkmanager.TalkText.text = "�̶�, ���ΰ��� ���� �ؿ� �ִ� ���ۿ� ������ �ȴ�.";
            ++talkmanager.clickCount;
            talkNext.SetActive(false);
        }
    }

    private void OpenningAction() //��ȭâ�� ���� �޶����� �ִϸ��̼�
    {
        if (talkmanager.clickCount == 0)
        {
            ani.SetBool("isNightMare", true); //����� ǥ������ ��ü�ؾ���.
        }
        else if (talkmanager.clickCount == 1)
        {
            ani.SetBool("isNightMare", false); //����� ǥ������ ��ü�ؾ���.
            ani.SetBool("isBoxing", true); //���ȸ� ǥ��+������ �����ϸ� ��鸮�� ������� ��ü�ؾ���.
        }
        else if (talkmanager.clickCount == 2)
        {
            render.flipY = true; //���� �Ʒ��� ������ �ٲٰ�
            state = true;
            ani.SetBool("isBoxing", false); //���ǥ��/���� �ִϸ��̼����� ��ü�ؾ���
        }
    }
}
