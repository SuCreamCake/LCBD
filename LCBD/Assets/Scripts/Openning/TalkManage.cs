using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TalkManage : MonoBehaviour
{
    public GameObject talkPanel; //��ȭâ UI�ǳ�
    public GameObject CharPanel; //ĳ�����̸� UI�ǳ�
    public Text TalkText; //�ؽ�Ʈ�� �� ���뺯��
    public bool isTalk; //��ȭ���¿���
    public Text CharText;//ĳ�����̸� ��ȭ����
    public int clickCount = 0; //Ŭ���� ī��Ʈ ����;
    public GameObject blackPanel;

    private void Awake()
    {
        blackPanel.SetActive(false);
        //ani = GetComponent<Animator>();
        isTalk = true;
        TalkUpdate();
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
        }
        else if (clickCount == 2)
        {
            TalkText.text = "���ƾƾƾƾ�!!!!!!!!!\n" + "(���� ������ ��������.)";
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
            blackPanel.SetActive(true);
            TalkText.text = "��...!";
        }
        else if(clickCount ==8)
        {
            SceneManager.LoadScene("RandomMap");
        }
        else //Ű �Է��� ���� ����� ��Ȱ��ȭ ������Ʈ�Լ������� �����.
        {
            talkPanel.SetActive(false);
            isTalk = false;
        }
    }

    private void skipBtn()
    {
        SceneManager.LoadScene("RandomMap");
    }
}
