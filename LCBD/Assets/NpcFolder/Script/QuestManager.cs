using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questIDToRetrieve; // 가져올 퀘스트의 ID 입력

    private QuestData questData;

    private void Start()
    {
        questData = FindObjectOfType<QuestData>(); // QuestData 스크립트 인스턴스를 찾음
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RetrieveQuestInfo();
        }
    }

    void RetrieveQuestInfo()
    {
        if (questData != null)
        {
            if (questData.questDictionary.ContainsKey(questIDToRetrieve))
            {
                Quest retrievedQuest = questData.questDictionary[questIDToRetrieve];

                // 선행 퀘스트 완료 여부 확인
                if (!retrievedQuest.isPrerequisiteComplete)
                {
                    Debug.Log("선행 퀘스트가 완료되지 않았습니다.");
                    return;
                }

                if (!retrievedQuest.isQuestInProgress)
                {
                    Debug.Log("퀘스트 ID: " + retrievedQuest.ID);
                    Debug.Log("퀘스트 이름: " + retrievedQuest.name);
                    Debug.Log("퀘스트 내용: " + retrievedQuest.description);
                    Debug.Log("퀘스트 선행대화:");
                    foreach (string dialogue in retrievedQuest.dialogues)
                    {
                        Debug.Log("- " + dialogue);
                    }
                    retrievedQuest.isQuestInProgress = true;
                }
                else
                {
                    if (retrievedQuest.requiredCompletionCount <= retrievedQuest.currentCompletionCount)
                    {
                        Debug.Log("퀘스트 완료대화:");
                        foreach (string dialogue in retrievedQuest.completionDialogues)
                        {
                            Debug.Log("- " + dialogue);
                        }
                        // 다음 퀘스트 처리
                        if (retrievedQuest.nextQuestID != 0)
                        {
                            Quest nextQuest = questData.questDictionary[retrievedQuest.nextQuestID];
                            nextQuest.isPrerequisiteComplete = true; // 다음 퀘스트의 선행 퀘스트 완료 여부를 true로 설정
                            Debug.Log("다음 퀘스트 처리: ID - " + nextQuest.ID + "  " + nextQuest.isPrerequisiteComplete);
                        }
                    } else
                    {
                        Debug.Log("퀘스트 수행이 완벽하지 않습니다.");
                    }
                }
            }
            else
            {
                Debug.Log("해당 ID의 퀘스트가 없습니다.");
            }
        }
        else
        {
            Debug.Log("QuestData 스크립트를 찾을 수 없습니다.");
        }
    }
}
