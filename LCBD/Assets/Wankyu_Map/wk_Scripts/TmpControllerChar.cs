using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpControllerChar : MonoBehaviour
{
    void Update()
    {
        float velocity = 10 * Time.deltaTime; //대충 만든 임시 캐릭터 스크립트
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, velocity, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-velocity, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -velocity, 0);
        }   
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(velocity, 0, 0);
        }

        if ((Input.GetKeyDown(KeyCode.Space)))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 4, 0), ForceMode2D.Impulse);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("StagePortal"))
            {
                if(Input.GetKeyDown(KeyCode.LeftControl))
                {
                    Debug.Log("StagePortal");
                    collision.GetComponent<StagePortal>().TeleportToNext();
                }
            }
            if (collision.CompareTag("FieldPortal"))
            {
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    Debug.Log("FieldPortal");
                    collision.GetComponent<FieldPortal>().Teleport(gameObject);
                }
            }
        }
    }
}