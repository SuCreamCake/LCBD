using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpControllerChar : MonoBehaviour
{
    void Update()
    {
        float velocity = 50 * Time.deltaTime;
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
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("StagePortal"))
            {
                Debug.Log("StagePortal");
                collision.GetComponent<StagePortal>().TeleportToNext();
            }
        }
    }
}