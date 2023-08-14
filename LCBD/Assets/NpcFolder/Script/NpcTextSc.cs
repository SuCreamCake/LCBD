using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTextSc : MonoBehaviour
{
    public string[] sentences;
    public Transform chatTr;
    public GameObject chatBoxPrefab;

    void Start()
    {
        // 10초마다 TalkNpc 메서드를 호출합니다.
        InvokeRepeating("TalkNpc", 0f, 10f);
    }

    public void TalkNpc()
    {
        GameObject go = Instantiate(chatBoxPrefab);
        go.GetComponent<ChatSystem>().Ondialogue(sentences, chatTr);
    }
}
