using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TalkManage : MonoBehaviour
{
    public GameObject talkPanel; //대화창 UI판넬
    public GameObject CharPanel; //캐릭터이름 UI판넬
    public Text TalkText; //텍스트에 쓸 내용변수
    public bool isTalk; //대화상태여부
    public Text CharText;//캐릭터이름 대화상자
    public int clickCount = 0; //클릭한 카운트 갯수;
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
                Debug.Log("대화창 넘기기");
                clickCount++;
                TalkUpdate(); //입력을 통해 계속 대화를 이어가야하기때문에 업데이트에도 넣어줌.
            }
        }
    }

    private void TalkUpdate()
    {

        talkPanel.SetActive(true); //함수 실행과 동시에 원래 대화실행
        if (clickCount == 0) //첫번째 대화
        {
            TalkText.text = "오늘도 실수를 했네....언제쯤 좋은 선택을 할 수 있을까\n";
            CharText.text = "주인공";
        }
        else if (clickCount == 1) //키 입력후 카운트 될때마다 대화가 바뀜
        {
            TalkText.text = "왜 그런 선택을 했지?\n" +
                "(쉐도우 복싱을 하며..몸이 크게 흔들린다..)";
        }
        else if (clickCount == 2)
        {
            TalkText.text = "으아아아아악!!!!!!!!!\n" + "(나무 위에서 떨어진다.)";
        }
        else if (clickCount == 3)
        {
            CharPanel.SetActive(false);
            TalkText.text = "이때, 주인공은 나무 밑에 있는 구멍에 빠지게 된다.";
        }
        else if (clickCount == 4)
        {
            TalkText.text = "끝도 없이 떨어지는 주인공..\n" +
                "과거의 기억들이 스쳐 지나간다.";
        }
        else if (clickCount == 5)
        {
            CharPanel.SetActive(true);
            TalkText.text = "나무에 올라간 선택이 잘못 된걸까?\n" +
                "오늘은 올라가지 말걸...." +
                "또 잘못 선택했네...";
        }
        else if (clickCount == 6)
        {
            TalkText.text = "(눈을 감으며)이렇게 죽는구나...";
        }
        else if (clickCount == 7)
        {
            CharPanel.SetActive(false);
            blackPanel.SetActive(true);
            TalkText.text = "쿵...!";
        }
        else if(clickCount ==8)
        {
            SceneManager.LoadScene("RandomMap");
        }
        else //키 입력의 값이 벗어나면 비활성화 업데이트함수에서도 실행됨.
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
