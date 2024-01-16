using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlay : MonoBehaviour
{
    //플레이어 입장의 스크립트
    CopyCatAbility copy;
    // Start is called before the first frame update
    void Start()
    {
        copy = GameObject.Find("Square").GetComponent<CopyCatAbility>();
    }

    // Update is called once per frame
    void Update()
    {
        GetKeyKode();
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("monster")){
             Debug.Log("몬스터와 충돌하였습니다.");
            copy.AlertHit(collision);
        }
    }

    void GetKeyKode() {
    // F 키를 누른 경우 즉 카피캣 실행시
    if (Input.GetKeyDown(KeyCode.F)) {
        Debug.Log("F 키가 눌렸습니다.");
        copy.HandleEvent();
    }
}
}
