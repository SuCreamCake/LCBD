using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questIDToRetrieve; // ������ ����Ʈ�� ID �Է�

    private QuestData questData;

    private void Start()
    {
        questData = FindObjectOfType<QuestData>(); // QuestData ��ũ��Ʈ �ν��Ͻ��� ã��
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

                // ���� ����Ʈ �Ϸ� ���� Ȯ��
                if (!retrievedQuest.isPrerequisiteComplete)
                {
                    Debug.Log("���� ����Ʈ�� �Ϸ���� �ʾҽ��ϴ�.");
                    return;
                }

                if (!retrievedQuest.isQuestInProgress)
                {
                    Debug.Log("����Ʈ ID: " + retrievedQuest.ID);
                    Debug.Log("����Ʈ �̸�: " + retrievedQuest.name);
                    Debug.Log("����Ʈ ����: " + retrievedQuest.description);
                    Debug.Log("����Ʈ �����ȭ:");
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
                        Debug.Log("����Ʈ �Ϸ��ȭ:");
                        foreach (string dialogue in retrievedQuest.completionDialogues)
                        {
                            Debug.Log("- " + dialogue);
                        }
                        // ���� ����Ʈ ó��
                        if (retrievedQuest.nextQuestID != 0)
                        {
                            Quest nextQuest = questData.questDictionary[retrievedQuest.nextQuestID];
                            nextQuest.isPrerequisiteComplete = true; // ���� ����Ʈ�� ���� ����Ʈ �Ϸ� ���θ� true�� ����
                            Debug.Log("���� ����Ʈ ó��: ID - " + nextQuest.ID + "  " + nextQuest.isPrerequisiteComplete);
                        }
                    } else
                    {
                        Debug.Log("����Ʈ ������ �Ϻ����� �ʽ��ϴ�.");
                    }
                }
            }
            else
            {
                Debug.Log("�ش� ID�� ����Ʈ�� �����ϴ�.");
            }
        }
        else
        {
            Debug.Log("QuestData ��ũ��Ʈ�� ã�� �� �����ϴ�.");
        }
    }
}
