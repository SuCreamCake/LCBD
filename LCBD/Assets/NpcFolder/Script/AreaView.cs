using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaView : MonoBehaviour
{
    public GameObject[] aObjects; // A ������Ʈ���� �ν����Ϳ��� ������ �迭

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // B ������Ʈ�� �±װ� "Player"�� ���
        {
            foreach (GameObject aObject in aObjects) // �ν����Ϳ��� ������ ��� A ������Ʈ�鿡 ���� �ݺ�
            {
                if (other.gameObject == aObject) // �浹�� ������Ʈ�� A ������Ʈ�� ���
                {
                    Debug.Log("A ������Ʈ�� B ������Ʈ�� �浹�߽��ϴ�. C ������Ʈ���� �α׸� �ۼ��մϴ�.");
                }   
            }
        }
    }
}
