using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPlus : MonoBehaviour
{
    // QuestData Ŭ������ ������ ������
    public QuestData questData;

    private void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            // QuestData Ŭ������ questDictionary���� ���ϴ� ����Ʈ�� ID�� ����Ͽ� Quest ��ü�� ����
            if (questData.questDictionary.TryGetValue(1, out Quest quest))
            {
                // �ش� ����Ʈ�� currentCompletionCount�� ������Ŵ
                quest.currentCompletionCount++;
                Debug.Log("����Ʈ ����Ƚ�� :" + quest.currentCompletionCount);

                // ���� �ش� ����Ʈ�� ���൵�� ������ ���̹Ƿ� ���ϴ� ó���� ������ �� ����
                // ��: ����Ʈ Ŭ��� Ȯ���ϰ� ���� ����Ʈ�� �̵� ��
            }
        }
    }
}

