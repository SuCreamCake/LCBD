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
        // 10�ʸ��� TalkNpc �޼��带 ȣ���մϴ�.
        InvokeRepeating("TalkNpc", 0f, 10f);
    }

    public void TalkNpc()
    {
        GameObject go = Instantiate(chatBoxPrefab);
        go.GetComponent<ChatSystem>().Ondialogue(sentences, chatTr);
    }
}
