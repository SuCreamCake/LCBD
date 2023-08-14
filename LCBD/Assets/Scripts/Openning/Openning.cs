using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openning : MonoBehaviour
{
    public TalkManage talkmanager; //톡매니저
    public BGscroll scroll1,scroll2,scroll3,scroll4; //배경스크롤 스피드설정
    public GameObject Hole;
    public GameObject talkNext;
    SpriteRenderer render; //플레이어 렌더링
    int speed=7; //플레이어 떨어지는 속도
    Animator ani; //애니메이션
    Vector3 startPos;
    bool state; //상태




    private void Awake()
    {
        render = GetComponent<SpriteRenderer>(); //플레이어 렌더링
        ani = GetComponent<Animator>(); //애니메이션
       // scrollspeed.isMove = false; //배경이 움직이는 속도
        startPos = transform.position; //플레이어의 시작위치를 저장해둠.
        state = false;
    }

    void Update()
    {
        OpenningAction();
        if (state)
            playerDown();
    }

    private void playerDown() //플레이어가 떨어지는 메소드
    {
        talkmanager.isTalk = false;
        Vector3 curPos = transform.position; //현재 위치를 가져온다
        Vector3 downPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + downPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Potal"))
        {
            Debug.Log("홀에 들어감.");
            transform.position = startPos;
            state = false;
            speed = 0; //대화를 안넘기면 state가 true상태를 유지하게되서 일단 내려가는 속력을0으로 멈춘것처럼보이게
            Hole.SetActive(false);
            scroll1.isMove = true;
            scroll2.isMove = true;
            scroll3.isMove = true;
            scroll4.isMove = true;
            ++talkmanager.clickCount; 
            //여기서 바꿔줘야 텍스트가 바뀜...저거 ++한다해도 talkmanager에 update에 영향이안감....
            talkmanager.TalkText.text = "끝도 없이 떨어지는 주인공..\n" +
                "과거의 기억들이 스쳐 지나간다.";
            talkmanager.isTalk = true;
        }
        if (collision.CompareTag("TestTag"))
        {
            //여기서 바꿔줘야 텍스트가 바뀜...저거 ++한다해도 talkmanager에 update에 영향이안감....
            talkmanager.TalkText.text = "이때, 주인공은 나무 밑에 있는 구멍에 빠지게 된다.";
            ++talkmanager.clickCount;
            talkNext.SetActive(false);
        }
    }

    private void OpenningAction() //대화창에 따라 달라지는 애니메이션
    {
        if (talkmanager.clickCount == 0)
        {
            ani.SetBool("isNightMare", true); //우울한 표정으로 대체해야함.
        }
        else if (talkmanager.clickCount == 1)
        {
            ani.SetBool("isNightMare", false); //우울한 표정으로 대체해야함.
            ani.SetBool("isBoxing", true); //쪽팔린 표정+쉐도우 복싱하며 흔들리는 모션으로 대체해야함.
        }
        else if (talkmanager.clickCount == 2)
        {
            render.flipY = true; //얼굴이 아래로 가도록 바꾸고
            state = true;
            ani.SetBool("isBoxing", false); //놀란표정/공포 애니메이션으로 대체해야함
        }
    }
}
