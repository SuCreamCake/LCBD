using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick_Mom_Lever : MonoBehaviour
{
    public Sprite left;
    public Sprite right;

    public void Start()
    {
        transform.GetChild(0).gameObject.SetActive(true);  //자식 0번 활성화
        GetComponent<SpriteRenderer>().sprite = left;
    }

    public void PullLever()
    {
        if (GetComponent<SpriteRenderer>().sprite == left)
        {
            transform.GetChild(0).gameObject.SetActive(false);  //자식 0번 비활성화
            GetComponent<SpriteRenderer>().sprite = right;
        }
    }
}
