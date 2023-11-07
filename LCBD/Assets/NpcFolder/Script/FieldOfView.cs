using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;            // 시야 반경
    [Range(0, 360)]
    public float viewAngle;             // 시야 각도
    public float startingAngle;         // 시작 각도 (기본적으로 0에서 시작)

    public LayerMask targetMask;        // Player를 감지할 레이어 마스크
    public LayerMask obstacleMask;      // 장애물을 감지할 레이어 마스크

    public float smallViewRadius;        // 작은 시야 반경

    MonsterManager MonsterManager;

    bool touch = false;              // 터치 여부

    private void Start()
    {
        MonsterManager = GetComponent<MonsterManager>();
        StartCoroutine("FindTargetsWithDelay", 0.2f);
        StartCoroutine("SmallFindTargetsWithDelay", 0.2f);
    }

    // 주기적으로 타겟 찾기
    private IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    // 작은 시야 반경으로 주기적으로 타겟 찾기
    private IEnumerator SmallFindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            SmallFindVisibleTargets();
        }
    }

    // 시야 내의 타겟 찾기
    private void FindVisibleTargets()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        bool playerDetected = false; // 플레이어를 감지했는지 여부를 나타내는 변수

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Collider2D target = targetsInViewRadius[i];

            Vector2 dirToTarget = (target.transform.position - transform.position).normalized;

            float correctedStartingAngle;

            // 시작 각도와 타겟과의 각도를 계산
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
                // 시야 내에 장애물이 없으면 타겟을 감지
                if (!Physics2D.Raycast(transform.position, dirToTarget, viewRadius, obstacleMask))
                {
                    // Player 태그를 가진 타겟을 감지하면 플레이어를 감지했다고 표시
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

        // 플레이어를 감지하거나 터치했으면 MonsterManager에 알림
        if (playerDetected || touch)
        {
            MonsterManager.AppearPlayer();
        }
        else
        {
            MonsterManager.DisAppearPlayer();
        }
    }

    // 시각적 표시를 위한 Gizmos 그리기
    private void OnDrawGizmos()
    {
        // 작은 시야 반경 표시
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, smallViewRadius);

        // 시야 반경 표시
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector2 viewAngleA = DirFromAngle(-viewAngle / 2 + startingAngle, false);
        Vector2 viewAngleB = DirFromAngle(viewAngle / 2 + startingAngle, false);

        // X 좌우로 뒤집어야 하는 경우 180도를 더해줌
        if (GetComponent<SpriteRenderer>() != null && GetComponent<SpriteRenderer>().flipX)
        {
            viewAngleA = DirFromAngle(viewAngle / 2 + startingAngle + 180f, false);
            viewAngleB = DirFromAngle(-viewAngle / 2 + startingAngle + 180f, false);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)viewAngleB * viewRadius);
    }

    // 각도를 벡터로 변환하는 함수
    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }

        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
    }

    // 작은 시야 반경 내의 타겟 찾기
    private void SmallFindVisibleTargets()
    {
        Collider2D[] SmalltargetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, smallViewRadius, targetMask);

        touch = false;

        for (int i = 0; i < SmalltargetsInViewRadius.Length; i++)
        {
            Collider2D target = SmalltargetsInViewRadius[i];

            Vector2 dirToTarget = (target.transform.position - transform.position).normalized;

            if (Physics2D.Raycast(transform.position, dirToTarget, smallViewRadius))
            {
                // Player 태그를 가진 타겟을 감지하면 터치했다고 표시
                if (target.CompareTag("Player"))
                {
                    Debug.Log("플레이어 터치함");
                    touch = true;
                }
            }
        }
    }
}
