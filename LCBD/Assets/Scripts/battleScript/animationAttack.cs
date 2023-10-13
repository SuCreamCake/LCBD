using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationAttack : MonoBehaviour
{
    Animator anim;
    public GameObject followPlayer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void SetAnimMealAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("isAttack");
            Debug.Log("�ִϸ��̼� ���� ����!!");
        }

    }

    public void DebugLog()
    {
        Debug.Log("�ִϸ��̼� ���� ����� �α׸� ȣ����..");
    }
    public void Follow()
    {
        this.transform.position = followPlayer.transform.position;
    }

    public void Update()
    {
        Follow();
    }

}
