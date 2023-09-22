using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPlus : MonoBehaviour
{
    // QuestData 클래스의 참조를 가져옴
    public QuestData questData;

    private void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            // QuestData 클래스의 questDictionary에서 원하는 퀘스트의 ID를 사용하여 Quest 객체에 접근
            if (questData.questDictionary.TryGetValue(1, out Quest quest))
            {
                // 해당 퀘스트의 currentCompletionCount를 증가시킴
                quest.currentCompletionCount++;
                Debug.Log("퀘스트 진행횟수 :" + quest.currentCompletionCount);

                // 이제 해당 퀘스트의 진행도를 변경한 것이므로 원하는 처리를 수행할 수 있음
                // 예: 퀘스트 클리어를 확인하고 다음 퀘스트로 이동 등
            }
        }
    }
}

