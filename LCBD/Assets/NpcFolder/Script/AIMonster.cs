using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMonster : MonoBehaviour
{
    Rigidbody2D rigid;
    private int nextMove = 0; // 행동 지표를 결정할 변수 하나 생성

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(ThinkCoroutine());
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //지형 체크
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.4f, rigid.position.y);

        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0)); //에디터 상에서만 레이를 그려준다
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Default"));
        if (rayHit.collider == null) // 바닥 감지를 위해서 레이저를 쏜다! 없다면!!! 바닥이
        {
            nextMove = nextMove * -1;
            Debug.Log(nextMove);
        }
    }

    IEnumerator ThinkCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // 1초 대기
            nextMove = Random.Range(-2, 2);
        }
    }
}
