using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSc : MonoBehaviour
{
    private bool canDetect = true; // ������Ʈ ���� ������ ���������� ��Ÿ���� ����
    public float detectionInterval = 1f; // �ν� ���� ���� (1��)

    private void OnTriggerStay2D(Collider2D other)
    {
        if (canDetect && other.CompareTag("Player"))
        {
            Debug.Log("������Ʈ�� ���ƽ��ϴ�!");
            StartCoroutine(StartDetectionInterval());

            // "monster" �±׸� ���� ��� ������Ʈ�� ã�Ƽ� areacheck �޼��带 �����մϴ�.
            GameObject[] monsterObjects = GameObject.FindGameObjectsWithTag("monster");
            foreach (GameObject monsterObject in monsterObjects)
            {
                MonsterSc monsterSc = monsterObject.GetComponent<MonsterSc>();
                if (monsterSc != null)
                {
                    monsterSc.areacheck();
                }
            }
        }
    }

    IEnumerator StartDetectionInterval()
    {
        canDetect = false; // ������Ʈ ���� ����

        yield return new WaitForSeconds(detectionInterval);

        canDetect = true; // ������Ʈ ���� ���� ���·� ����
    }
}
