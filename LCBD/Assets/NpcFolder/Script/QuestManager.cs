using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public int questIDToRetrieve; // ������ ����Ʈ�� ID �Է�

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

    public void Questing(int questID)
    {
        if (questData != null)
        {
            if (questData.questDictionary.ContainsKey(questID))
            {
                Quest quest = questData.questDictionary[questID];

                // ����Ʈ�� ���� ���̾�� �ϰ�, �Ϸ� ���ǿ� �������� �ʾ��� ���� ������ŵ�ϴ�.
                if (quest.isQuestInProgress && quest.currentCompletionCount < quest.requiredCompletionCount)
                {
                    quest.currentCompletionCount++;

                    // ����Ʈ ���� ���¸� ����մϴ�.
                    Debug.Log("����Ʈ ID: " + quest.ID + "�� ���൵: " + quest.currentCompletionCount);
                }
                else
                {
                    Debug.Log("����Ʈ�� ������ �� �����ϴ�. �Ǵ� �̹� �Ϸ�Ǿ����ϴ�.");
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
