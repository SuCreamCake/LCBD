using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaView : MonoBehaviour
{
    public GameObject[] aObjects; // A 오브젝트들을 인스펙터에서 지정할 배열

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // B 오브젝트의 태그가 "Player"인 경우
        {
            foreach (GameObject aObject in aObjects) // 인스펙터에서 지정한 모든 A 오브젝트들에 대해 반복
            {
                if (other.gameObject == aObject) // 충돌한 오브젝트가 A 오브젝트인 경우
                {
                    Debug.Log("A 오브젝트와 B 오브젝트가 충돌했습니다. C 오브젝트에서 로그를 작성합니다.");
                }   
            }
        }
    }
}
