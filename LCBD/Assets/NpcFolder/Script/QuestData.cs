using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public int ID; //퀘스트ID
    public string name; //퀘스트 이름
    public string description; //퀘스트 상세 정보
    public string[] dialogues; //퀘스트 선행 대사
    public int nextQuestID; // 다음 퀘스트 번호
    public bool isPrerequisiteComplete; // 선행 퀘스트 완료 여부
    public string[] completionDialogues; // 퀘스트 완료 대사
    public int requiredCompletionCount; // 퀘스트 클리어 조건달성에 필요한 횟수
    public int currentCompletionCount; // 현재 퀘스트 진행도
    public bool isQuestInProgress; // 현재 퀘스트 진행 여부

    public Quest(int id, string questName, string questDescription, string[] questDialogues, int nextID, bool prerequisite,
                 string[] completeDialogues, int completionCount)
    {
        ID = id;
        name = questName;
        description = questDescription;
        dialogues = questDialogues;
        nextQuestID = nextID;
        isPrerequisiteComplete = prerequisite;
        completionDialogues = completeDialogues;
        requiredCompletionCount = completionCount;
        currentCompletionCount = 0; // 퀘스트 진행도 초기화
        isQuestInProgress = false; // 퀘스트 진행 여부 초기화
    }
}

public class QuestData : MonoBehaviour
{
    public Dictionary<int, Quest> questDictionary = new Dictionary<int, Quest>();

    private void Awake()
    {
        // 퀘스트 정보를 생성하고 딕셔너리에 추가
        Quest quest1 = new Quest(1, "첫 번째 퀘스트", "첫 번째 퀘스트 세부내용입니다.", new string[] { "안녕하세요!", "L키를 세번 눌러주세요." },
                                 10, true, new string[] { "1퀘스트 완료 대사1", "1퀘스트 완료 대사2..." }, 3);

        Quest quest2 = new Quest(2, "두 번째 퀘스트", "두 번째 퀘스트 세부내용입니다.", new string[] { "이 퀘스트는 어려워요." },
                                 0, true, new string[] { "두 번째 퀘스트 완료 대사1", "두 번째 퀘스트 완료 대사2" }, 10);

        Quest quest10 = new Quest(10, "열 번째 퀘스트", "열 번째 퀘스트 세부내용입니다.", new string[] { "이 퀘스트는 어려워요.", "ㅋㅋ" },
                                  0, false, new string[] { "열 번째 퀘스트 완료 대사1", "열 번째 퀘스트 완료 대사2" }, 15);

        questDictionary.Add(quest1.ID, quest1);
        questDictionary.Add(quest2.ID, quest2);
        questDictionary.Add(quest10.ID, quest10);
    }
}
