using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManage : MonoBehaviour
{
    public GameObject talkPanel;
    public Text TalkText; //�ؽ�Ʈ�� �� ���뺯��
    public bool isTalk; //��ȭ���¿���
    public Text CharText;//ĳ�����̸� ��ȭ����
    int clickCount = 0; //Ŭ���� ī��Ʈ ����;
    public Collider2D scanObject;

    private void Awake()
    {
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
            TalkText.text = "where is here?";
            CharText.text = scanObject.name;
        }
        else if (clickCount == 1) //Ű �Է��� ī��Ʈ �ɶ����� ��ȭ�� �ٲ�
        {
            TalkText.text = "successs????";
            CharText.text = "Suv Charactor";
        }
        else //Ű �Է��� ���� ����� ��Ȱ��ȭ ������Ʈ�Լ������� �����.
        {
            talkPanel.SetActive(false);
            isTalk = false;
        }
    }
}
