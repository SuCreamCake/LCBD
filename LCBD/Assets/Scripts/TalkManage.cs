using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManage : MonoBehaviour
{
    public GameObject talkPanel; //��ȭâ UI�ǳ�
    public GameObject CharPanel; //ĳ�����̸� UI�ǳ�
    public Text TalkText; //�ؽ�Ʈ�� �� ���뺯��
    public bool isTalk; //��ȭ���¿���
    public Text CharText;//ĳ�����̸� ��ȭ����
    int clickCount = 0; //Ŭ���� ī��Ʈ ����;
    public Collider2D scanObject; //�浹�� ������Ʈ
    //Animator ani; //�ִϸ����� �����ȵ�
    private void Awake()
    {
        //ani = GetComponent<Animator>();
        talkPanel.SetActive(false);
        isTalk = false;
    }

    private void Update()
    {
        if (isTalk)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            {
                Debug.Log("��ȭâ �ѱ��");
                clickCount++;
                TalkUpdate(); //�Է��� ���� ��� ��ȭ�� �̾���ϱ⶧���� ������Ʈ���� �־���.
            }
        }
    }

    public void Talk(Collider2D collision)
    {
        scanObject = collision; //��߰� �浹�� ��ü�̸��� ������
        isTalk = true; //��ȭ�� Ȱ��ȭ��
        TalkUpdate(); //�浹�� �۵��ϱ⶧���� ��ȭ �ٷ� ù��° ����
    }

    private void TalkUpdate()
    {

        talkPanel.SetActive(true); //�Լ� ����� ���ÿ� ���� ��ȭ����
        if (clickCount == 0) //ù��° ��ȭ
        {
            TalkText.text = "���õ� �Ǽ��� �߳�....������ ���� ������ �� �� ������\n";
            CharText.text = "���ΰ�";
        }
        else if (clickCount == 1) //Ű �Է��� ī��Ʈ �ɶ����� ��ȭ�� �ٲ�
        {
            TalkText.text = "�� �׷� ������ ����?\n" +
                "(������ ������ �ϸ�..���� ũ�� ��鸰��..)";

                //ani.SetBool("isLook", true);
        }
        else if (clickCount == 2)
        {
            TalkText.text = "���ƾƾƾƾ�!!!!!!!!!\n" + "(���� ������ ��������.)";
            //ani.SetBool("isLook", false);
        }
        else if (clickCount == 3)
        {
            CharPanel.SetActive(false);
            TalkText.text = "�̶�, ���ΰ��� ���� �ؿ� �ִ� ���ۿ� ������ �ȴ�.";
        }
        else if (clickCount == 4)
        {
            TalkText.text = "���� ���� �������� ���ΰ�..\n" +
                "������ ������ ���� ��������.";
        }
        else if (clickCount == 5)
        {
            CharPanel.SetActive(true);
            TalkText.text = "������ �ö� ������ �߸� �Ȱɱ�?\n" +
                "������ �ö��� ����...." +
                "�� �߸� �����߳�...";
        }
        else if (clickCount == 6)
        {
            TalkText.text = "(���� ������)�̷��� �״±���...";
        }
        else if (clickCount == 7)
        {
            CharPanel.SetActive(false);
            TalkText.text = "��...!";
        }
        else //Ű �Է��� ���� ����� ��Ȱ��ȭ ������Ʈ�Լ������� �����.
        {
            talkPanel.SetActive(false);
            isTalk = false;
        }
    }
}
