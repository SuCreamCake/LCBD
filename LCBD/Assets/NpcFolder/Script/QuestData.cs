using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public int ID; //����ƮID
    public string name; //����Ʈ �̸�
    public string description; //����Ʈ �� ����
    public string[] dialogues; //����Ʈ ���� ���
    public int nextQuestID; // ���� ����Ʈ ��ȣ
    public bool isPrerequisiteComplete; // ���� ����Ʈ �Ϸ� ����
    public string[] completionDialogues; // ����Ʈ �Ϸ� ���
    public int requiredCompletionCount; // ����Ʈ Ŭ���� ���Ǵ޼��� �ʿ��� Ƚ��
    public int currentCompletionCount; // ���� ����Ʈ ���൵
    public bool isQuestInProgress; // ���� ����Ʈ ���� ����

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
        currentCompletionCount = 0; // ����Ʈ ���൵ �ʱ�ȭ
        isQuestInProgress = false; // ����Ʈ ���� ���� �ʱ�ȭ
    }
}

public class QuestData : MonoBehaviour
{
    public Dictionary<int, Quest> questDictionary = new Dictionary<int, Quest>();

    private void Awake()
    {
        // ����Ʈ ������ �����ϰ� ��ųʸ��� �߰�
        Quest quest1 = new Quest(1, "ù ��° ����Ʈ", "ù ��° ����Ʈ ���γ����Դϴ�.", new string[] { "�ȳ��ϼ���!", "LŰ�� ���� �����ּ���." },
                                 10, true, new string[] { "1����Ʈ �Ϸ� ���1", "1����Ʈ �Ϸ� ���2..." }, 3);

        Quest quest2 = new Quest(2, "�� ��° ����Ʈ", "�� ��° ����Ʈ ���γ����Դϴ�.", new string[] { "�� ����Ʈ�� �������." },
                                 0, true, new string[] { "�� ��° ����Ʈ �Ϸ� ���1", "�� ��° ����Ʈ �Ϸ� ���2" }, 10);

        Quest quest10 = new Quest(10, "�� ��° ����Ʈ", "�� ��° ����Ʈ ���γ����Դϴ�.", new string[] { "�� ����Ʈ�� �������.", "����" },
                                  0, false, new string[] { "�� ��° ����Ʈ �Ϸ� ���1", "�� ��° ����Ʈ �Ϸ� ���2" }, 15);

        questDictionary.Add(quest1.ID, quest1);
        questDictionary.Add(quest2.ID, quest2);
        questDictionary.Add(quest10.ID, quest10);
    }
}
