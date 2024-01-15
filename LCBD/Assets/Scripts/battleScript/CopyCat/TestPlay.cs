using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlay : MonoBehaviour
{
    CopyCatAbility copy;
    // Start is called before the first frame update
    void Start()
    {
        copy = GameObject.Find("TestCopyCat").GetComponent<CopyCatAbility>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("monster")){
             Debug.Log("몬스터와 충돌하였습니다.");
            copy.AlertHit(collision);
        }
    }
}
