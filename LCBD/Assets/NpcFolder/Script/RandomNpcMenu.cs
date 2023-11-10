using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNpcMenu : MonoBehaviour
{   // 세개의 메뉴 중 하나 랜덤으로 띄우기
    GameObject One, Two, Three;
    private int num; // 초기 값 0이면 랜덤 x
    // Start is called before the first frame update
    void Awake()
    {
        num = Random.Range(0, 3);
    }
    void Start()
    {
        One = transform.GetChild(0).gameObject;
        Two = transform.GetChild(1).gameObject;
        Three = transform.GetChild(2).gameObject;
        One.SetActive(false);
        Two.SetActive(false);
        Three.SetActive(false);

        switch (num)
        {
            case 0:
                One.SetActive(true);
                // num이 0일 때 수행할 동작
                break;
            case 1:
                Two.SetActive(true);
                // num이 1일 때 수행할 동작
                break;
            case 2:
                Three.SetActive(true);
                // num이 2일 때 수행할 동작
                break;
            default:
                Debug.Log("서브메뉴 선택 오류");
                break;
        }
    }

}
