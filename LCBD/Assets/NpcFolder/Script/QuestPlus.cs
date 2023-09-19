using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPlus : MonoBehaviour
{
    public int QuestId;

    private void Start()
    {
        QuestId = 1;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            QuestManager.instance.Questing(QuestId);
        }
    }
}

