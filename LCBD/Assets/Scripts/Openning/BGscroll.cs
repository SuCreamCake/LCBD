using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGscroll : MonoBehaviour
{

    public float speed;
    public int startIndex; //시작인덱스
    public int endIndex; //끝 인덱스
    public Transform[] sprites;//배경들 배열
    public TalkManage talkManager;
    public bool isMove;
    float viewHeight;

    void Awake() {
        isMove = false;
        viewHeight = Camera.main.orthographicSize*2; //카메라 높이 사이즈
    }

    void Update()
    {
        if(isMove)
            scrolling(); //스크롤링 실행
    }

    private void scrolling() //플레이어가 떨어지는 것처럼 보이도록 하는 메소드
    {
        Vector3 curPos = transform.position; //현재 위치를 가져온다
        //떨어지는것처럼 보이기위해 배경을 위로 이동시킴
        Vector3 nextPos = Vector3.up * speed * Time.deltaTime;
        //배경의 위치를 계속 위로 이동시킴
        transform.position = curPos + nextPos;


        //첫번째 sprite가 카메라위치를 벗어날경우 발생
        if (sprites[startIndex].position.y > viewHeight*1.3)
        {
            //마지막 sprite의 위치를 가져온다.
            Vector3 backSpritePos = sprites[endIndex].localPosition;
            //첫번재 sprite의 월드위치를 마지막 sprite의 뒤에 카메라크기만큼 아래에 붙인다.
            sprites[startIndex].transform.localPosition = backSpritePos + Vector3.down * viewHeight;


            //이동이 완료되면 endIndex와 StartIndex 갱신
            //첫번째 인덱스를 저장
            int startIndexSave = startIndex;
            //마지막 인덱스를 첫번째 인덱스로 넣는다
            endIndex = startIndex;
            //배열 안벗어나게
            startIndex = (startIndexSave - 1 == -1) ? sprites.Length - 1 : startIndexSave - 1;
        }
    }
}
