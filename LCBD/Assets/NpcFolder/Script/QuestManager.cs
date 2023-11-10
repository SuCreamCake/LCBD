using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public int questIDToRetrieve; // 가져올 퀘스트의 ID 입력

    public QuestData questData;

    private void Awake()
    {
        instance = this;
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
                    }
                    else
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

    public void Questing(int questID)
    {
        if (questData != null)
        {
            if (questData.questDictionary.ContainsKey(questID))
            {
                Quest quest = questData.questDictionary[questID];

                // 퀘스트가 진행 중이어야 하고, 완료 조건에 도달하지 않았을 때만 증가시킵니다.
                if (quest.isQuestInProgress && quest.currentCompletionCount < quest.requiredCompletionCount)
                {
                    quest.currentCompletionCount++;

                    // 퀘스트 진행 상태를 출력합니다.
                    Debug.Log("퀘스트 ID: " + quest.ID + "의 진행도: " + quest.currentCompletionCount);
                }
                else
                {
                    Debug.Log("퀘스트를 진행할 수 없습니다. 또는 이미 완료되었습니다.");
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