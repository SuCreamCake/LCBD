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

    // 마우스 클릭 이벤트를 처리하는 메소드
    public void OnPointerClick(PointerEventData eventData)
    {
        if (starting)
        {
            // 자식 오브젝트 중에서 "Ball" 태그를 가진 오브젝트를 찾습니다.
            Transform ballObject = FindBallInChildren();

            if (ballObject != null)
            {
                Debug.Log("공이 발견되었습니다. 게임 성공");
                CupManager.PlusMoney(3000);
            }
            else
            {
                Debug.Log("공이 발견되지 않았습니다. 게임 실패");
            }

            Cup1.GetComponent<CupClickChecker>().End();
            Cup2.GetComponent<CupClickChecker>().End();
            Cup3.GetComponent<CupClickChecker>().End();
            StartButton.GetComponent<CupManager>().GameRunningEnd();
        }
    }

    // 자식 오브젝트 중에서 "Ball" 태그를 가진 오브젝트를 찾는 메소드
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
