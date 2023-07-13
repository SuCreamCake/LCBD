using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManage : MonoBehaviour
{
    public GameObject talkPanel;
    public Text TalkText; //텍스트에 쓸 내용변수
    public bool isTalk; //대화상태여부
    public Text CharText;//캐릭터이름 대화상자
    int clickCount = 0; //클릭한 카운트 갯수;
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
                Debug.Log("대화창 넘기기");
                clickCount++;
                TalkUpdate(); //입력을 통해 계속 대화를 이어가야하기때문에 업데이트에도 넣어줌.
            }
        }
    }

    public void Talk(Collider2D collision)
    {
        scanObject = collision; //깃발과 충돌한 물체이름을 가져옴
        isTalk = true; //대화가 활성화됨
        TalkUpdate(); //충돌시 작동하기때문에 대화 바로 첫번째 실행
    }

    private void TalkUpdate()
    {

        talkPanel.SetActive(true); //함수 실행과 동시에 원래 대화실행
        if (clickCount == 0) //첫번째 대화
        {
            TalkText.text = "where is here?";
            CharText.text = scanObject.name;
        }
        else if (clickCount == 1) //키 입력후 카운트 될때마다 대화가 바뀜
        {
            TalkText.text = "successs????";
            CharText.text = "Suv Charactor";
        }
        else //키 입력의 값이 벗어나면 비활성화 업데이트함수에서도 실행됨.
        {
            talkPanel.SetActive(false);
            isTalk = false;
        }
    }
}
