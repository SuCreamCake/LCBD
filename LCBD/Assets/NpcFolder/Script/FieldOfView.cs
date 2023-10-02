using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;            // �þ� �ݰ�
    [Range(0, 360)]
    public float viewAngle;             // �þ� ����
    public float startingAngle;         // ���� ���� (�⺻���� 0���� �� ����)

    public LayerMask targetMask;        // Player�� ������ Ÿ�� ���̾�
    public LayerMask obstacleMask;      // ������ ��ֹ� ���̾�

    public float smallViewRadius;        // ���� �ݰ�

    MonsterManager MonsterManager;

    bool touch = false;              //���� ����


    private void Start()
    {
        MonsterManager = GetComponent<MonsterManager>();
        StartCoroutine("FindTargetsWithDelay", 0.2f);
        StartCoroutine("SmallFindTargetsWithDelay", 0.2f);
    }

    // ���� �ð����� Ÿ�� �˻�
    private System.Collections.IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
            
        }
    }
    // ���� �ð����� Ÿ�� �˻�
    private IEnumerator SmallFindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            SmallFindVisibleTargets();
        }
    }

    //�ֺ��˻�
    private void FindVisibleTargets()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        bool playerDetected = false; // �÷��̾ �������� ���θ� �����ϱ� ���� ����

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Collider2D target = targetsInViewRadius[i];

            Vector2 dirToTarget = (target.transform.position - transform.position).normalized;

            float correctedStartingAngle;

            // ���� ������ �þ� ������ ���ԵǴ��� Ȯ��
            float angleToTarget = Vector2.SignedAngle(Vector2.up, dirToTarget);

            if (GetComponent<SpriteRenderer>() != null && GetComponent<SpriteRenderer>().flipX)
            {
                correctedStartingAngle = startingAngle + 90f;
            }
            else
            {
                correctedStartingAngle = startingAngle - 90f;
            }

            float angleBetweenTargetAndStartingAngle = Mathf.DeltaAngle(correctedStartingAngle, angleToTarget);

            if (Mathf.Abs(angleBetweenTargetAndStartingAngle) < viewAngle / 2)
            {
                // �þ� �ȿ� �ִµ� ��ֹ��� ������ �ִٸ� ����
                if (!Physics2D.Raycast(transform.position, dirToTarget, viewRadius, obstacleMask))
                {
                    // Player�� �����ϴ� ���� �߰�
                    if (target.CompareTag("Player"))
                    {
                        playerDetected = true;
                        Debug.Log("Player detected");
                    }
                }
                else
                {
                    Debug.Log("Obstacle in the way!");
                }
            }
        }

        // �÷��̾ �������� ���� �������� �ʾ��� �� ���� �ٸ� ���� ����
        if (playerDetected || touch)
        {
            MonsterManager.AppearPlayer();
        } else {
            MonsterManager.DisAppearPlayer();
        }
    }


    // ����� �׸���
    private void OnDrawGizmos()
    {
        //���� �� ǥ��
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, smallViewRadius);


        // �� ����ǥ��
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector2 viewAngleA = DirFromAngle(-viewAngle / 2 + startingAngle, false);
        Vector2 viewAngleB = DirFromAngle(viewAngle / 2 + startingAngle, false);

        // x ���� üũ�Ǿ� �ִٸ� ���� ������ 180�� �����ϴ�.
        if (GetComponent<SpriteRenderer>() != null && GetComponent<SpriteRenderer>().flipX)
        {
            viewAngleA = DirFromAngle(viewAngle / 2 + startingAngle + 180f, false);
            viewAngleB = DirFromAngle(-viewAngle / 2 + startingAngle + 180f, false);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)viewAngleB * viewRadius);

    }

    // �����κ��� ���� ���� ��ȯ
    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }

        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
    }


    // ���˿� �ִ� Ÿ�� �˻�
    private void SmallFindVisibleTargets()
    {
        Collider2D[] SmalltargetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, smallViewRadius, targetMask);

        touch = false; ;

        for (int i = 0; i < SmalltargetsInViewRadius.Length; i++)
        {
            Collider2D target = SmalltargetsInViewRadius[i];

            Vector2 dirToTarget = (target.transform.position - transform.position).normalized;

            if (Physics2D.Raycast(transform.position, dirToTarget, smallViewRadius))
            {
                // Player�� �����ϴ� ���� �߰�
                if (target.CompareTag("Player"))
                {
                    Debug.Log("�����÷��̾�");
                    touch = true;
                }
            }
        }
    }

}