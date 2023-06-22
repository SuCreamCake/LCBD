using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSc : MonoBehaviour
{
    private bool canDetect = true; // 오브젝트 감지 가능한 상태인지를 나타내는 변수
    public float detectionInterval = 1f; // 인식 간격 설정 (1초)

    private void OnTriggerStay2D(Collider2D other)
    {
        if (canDetect && other.CompareTag("Player"))
        {
            Debug.Log("오브젝트가 겹쳤습니다!");
            StartCoroutine(StartDetectionInterval());

            // "monster" 태그를 가진 모든 오브젝트를 찾아서 areacheck 메서드를 실행합니다.
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
        canDetect = false; // 오브젝트 감지 중지

        yield return new WaitForSeconds(detectionInterval);

        canDetect = true; // 오브젝트 감지 가능 상태로 변경
    }
}
