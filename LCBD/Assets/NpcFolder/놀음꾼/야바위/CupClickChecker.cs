using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CupClickChecker : MonoBehaviour, IPointerClickHandler
{
    public GameObject Cup1;
    public GameObject Cup2;
    public GameObject Cup3;
    public GameObject StartButton;
    bool starting = false;
    public CupManager CupManager;


    // 클릭할 때 호출되는 함수
    public void OnPointerClick(PointerEventData eventData)
    {
        if (starting)
        {
            // 자식 오브젝트들 중에 "Ball" 태그를 가진 오브젝트를 찾는다.
            Transform ballObject = FindBallInChildren();

            if (ballObject != null)
            {
                Debug.Log("공을 찾았습니다. 도박 성공");
            }
            else
            {
                Debug.Log("공이 없습니다. 원금 손실");
            }

            Cup1.GetComponent<CupClickChecker>().End();
            Cup2.GetComponent<CupClickChecker>().End();
            Cup3.GetComponent<CupClickChecker>().End();
            StartButton.GetComponent<CupManager>().GameRunningEnd();
        }

    }

    // 자식 오브젝트들 중에 "Ball" 태그를 가진 오브젝트를 찾는 함수
    private Transform FindBallInChildren()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Ball"))
            {
                return child;
            }
        }

        return null;
    }

    public void Ing()
    {
        starting = true;
    }

    public void End()
    {
        starting = false;
    }

    public void Reset()
    {
        starting = false;
    }
}

