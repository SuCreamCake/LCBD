using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CitizenTalkData : MonoBehaviour
{
    Dictionary<int, string[]> TalkData;

    private void Awake()
    {
        TalkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    public string GetTalk(int id, int talkindex)
    {
        if (talkindex == TalkData[id].Length)
            return null;
        else
            return TalkData[id][talkindex];
    }

    public void GenerateData()
    {
        TalkData.Add(10, new string[] { "123", "456", "789", "101112" });
        TalkData.Add(20, new string[] { "A", "B", "C", "D" });
    }
}



