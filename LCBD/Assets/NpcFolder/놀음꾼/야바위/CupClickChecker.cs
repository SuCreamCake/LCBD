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

    // Ŭ���� �� ȣ��Ǵ� �Լ�
    public void OnPointerClick(PointerEventData eventData)
    {
        if (starting)
        {
            // �ڽ� ������Ʈ�� �߿� "Ball" �±׸� ���� ������Ʈ�� ã�´�.
            Transform ballObject = FindBallInChildren();

            if (ballObject != null)
            {
                Debug.Log("���� ã�ҽ��ϴ�.");
            }
            else
            {
                Debug.Log("���� �����ϴ�.");
            }

            Cup1.GetComponent<CupClickChecker>().End();
            Cup2.GetComponent<CupClickChecker>().End();
            Cup3.GetComponent<CupClickChecker>().End();
            StartButton.GetComponent<CupManager>().GameRunningEnd();
        }

    }

    // �ڽ� ������Ʈ�� �߿� "Ball" �±׸� ���� ������Ʈ�� ã�� �Լ�
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

    // The Ing() method that you want to call.
    public void Ing()
    {
        starting = true;
    }

    public void End()
    {
        starting = false;
    }
}

