using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGscroll : MonoBehaviour
{

    public float speed;
    public int startIndex; //�����ε���
    public int endIndex; //�� �ε���
    public Transform[] sprites;//���� �迭
    public TalkManage talkManager;
    public bool isMove;
    float viewHeight;

    void Awake() {
        isMove = false;
        viewHeight = Camera.main.orthographicSize*2; //ī�޶� ���� ������
    }

    void Update()
    {
        if(isMove)
            scrolling(); //��ũ�Ѹ� ����
    }

    private void scrolling() //�÷��̾ �������� ��ó�� ���̵��� �ϴ� �޼ҵ�
    {
        Vector3 curPos = transform.position; //���� ��ġ�� �����´�
        //�������°�ó�� ���̱����� ����� ���� �̵���Ŵ
        Vector3 nextPos = Vector3.up * speed * Time.deltaTime;
        //����� ��ġ�� ��� ���� �̵���Ŵ
        transform.position = curPos + nextPos;


        //ù��° sprite�� ī�޶���ġ�� ������ �߻�
        if (sprites[startIndex].position.y > viewHeight*1.3)
        {
            //������ sprite�� ��ġ�� �����´�.
            Vector3 backSpritePos = sprites[endIndex].localPosition;
            //ù���� sprite�� ������ġ�� ������ sprite�� �ڿ� ī�޶�ũ�⸸ŭ �Ʒ��� ���δ�.
            sprites[startIndex].transform.localPosition = backSpritePos + Vector3.down * viewHeight;


            //�̵��� �Ϸ�Ǹ� endIndex�� StartIndex ����
            //ù��° �ε����� ����
            int startIndexSave = startIndex;
            //������ �ε����� ù��° �ε����� �ִ´�
            endIndex = startIndex;
            //�迭 �ȹ����
            startIndex = (startIndexSave - 1 == -1) ? sprites.Length - 1 : startIndexSave - 1;
        }
    }
}
