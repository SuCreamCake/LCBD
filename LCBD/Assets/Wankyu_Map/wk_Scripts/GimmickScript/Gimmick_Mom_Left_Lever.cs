using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick_Mom_Left_Lever : MonoBehaviour, IControlGimmickObject
{
    public Sprite left_state;     // 왼쪽 이미지 (off).
    public Sprite right_state;    // 오른쪽 이미지 (on).

    private GameObject[] leftDials;     // 왼쪽 다이얼 기믹 오브젝트들.

    public void Awake()
    {
        leftDials = new GameObject[4];
        leftDials[0] = transform.parent.GetChild(0).GetChild(0).GetChild(0).gameObject;
        leftDials[1] = transform.parent.GetChild(0).GetChild(1).GetChild(0).gameObject;
        leftDials[2] = transform.parent.GetChild(0).GetChild(2).GetChild(0).gameObject;
        leftDials[3] = transform.parent.GetChild(0).GetChild(3).GetChild(0).gameObject;


        GetComponent<SpriteRenderer>().sprite = left_state;
        foreach (GameObject p in leftDials)
        {
            p.SetActive(false);
        }
    }

    public void ControlGimmickObject()
    {
        PullLever();
    }

    public void PullLever()
    {
        if (GetComponent<SpriteRenderer>().sprite == left_state)
        {
            foreach (GameObject p in leftDials)
            {
                p.SetActive(false);
            }
            GetComponent<SpriteRenderer>().sprite = right_state;
        }
        else
        {
            foreach (GameObject p in leftDials)
            {
                p.SetActive(true);
            }
            GetComponent<SpriteRenderer>().sprite = left_state;
        }
    }
}
