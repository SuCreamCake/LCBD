using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMonster : MonoBehaviour
{
    Rigidbody2D rigid;
    private int nextMove = 0; // �ൿ ��ǥ�� ������ ���� �ϳ� ����

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(ThinkCoroutine());
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //���� üũ
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.4f, rigid.position.y);

        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0)); //������ �󿡼��� ���̸� �׷��ش�
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Default"));
        if (rayHit.collider == null) // �ٴ� ������ ���ؼ� �������� ���! ���ٸ�!!! �ٴ���
        {
            nextMove = nextMove * -1;
            Debug.Log(nextMove);
        }
    }

    IEnumerator ThinkCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // 1�� ���
            nextMove = Random.Range(-2, 2);
        }
    }
}
