using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CitizenManager : MonoBehaviour
{
    public Text dialogueText;
    public Button nextButton;
    public GameObject TalkUIObjects;

    public CitizenTalkData citizenTalkData;
    public int currentNpcID = 10;
    public int currentTalkIndex;

    private void Start()
    {
        currentTalkIndex = 0;

        // 버튼 클릭 이벤트 추가
        nextButton.onClick.AddListener(Exit);

        // 초기 대화 표시
        DisplayDialogue();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextButtonClick();
        }
    }

    void Exit()
    {
        if (TalkUIObjects != null)
            TalkUIObjects.SetActive(false);
        currentTalkIndex = 0;
    }

    void NextButtonClick()
    {
        // 대화 인덱스 증가
        currentTalkIndex++;

        // 다음 대화 가져오기
        string dialogue = citizenTalkData.GetTalk(currentNpcID, currentTalkIndex);

        // 더 많은 대화가 있는지 확인
        if (dialogue != null)
        {
            // 다음 대화 표시
            DisplayDialogue();
        }
        else
        {
            if (TalkUIObjects != null)
                TalkUIObjects.SetActive(false);
            currentTalkIndex = 0;
        }
    }

    void DisplayDialogue()
    {
        // 현재 대화 가져오기
        string dialogue = citizenTalkData.GetTalk(currentNpcID, currentTalkIndex);

        // UI 텍스트 업데이트
        dialogueText.text = dialogue;
    }

}
