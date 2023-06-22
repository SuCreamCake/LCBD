using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;            // 시야 반경
    [Range(0, 360)]
    public float viewAngle;             // 시야 각도
    public float startingAngle;         // 시작 각도 (기본값은 0으로 위 방향)

    public LayerMask targetMask;        // Player를 포함한 타겟 레이어
    public LayerMask obstacleMask;      // 가리는 장애물 레이어


    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 1f);
    }

    // 일정 시간마다 타겟 검색
    private System.Collections.IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    // 주변에 있는 타겟 검색
    private void FindVisibleTargets()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Collider2D target = targetsInViewRadius[i];

            Vector2 dirToTarget = (target.transform.position - transform.position).normalized;

            float correctedStartingAngle;

            // 시작 각도와 시야 범위에 포함되는지 확인
            float angleToTarget = Vector2.SignedAngle(Vector2.up, dirToTarget);

            if (GetComponent<SpriteRenderer>() != null && GetComponent<SpriteRenderer>().flipX)
            {
                correctedStartingAngle = startingAngle + 90f;
            } else
            {
                correctedStartingAngle = startingAngle - 90f;
            }

            
            float angleBetweenTargetAndStartingAngle = Mathf.DeltaAngle(correctedStartingAngle, angleToTarget);

            

            if (Mathf.Abs(angleBetweenTargetAndStartingAngle) < viewAngle / 2)
            {

                // 시야 안에 있는데 장애물이 가리고 있다면 무시
                if (!Physics2D.Raycast(transform.position, dirToTarget, viewRadius, obstacleMask))
                {
                    // Player에 반응하는 로직 추가
                    if (target.CompareTag("Player"))
                    {
                        Debug.Log("Player detected!");
                    }
                }
                else
                {
                    Debug.Log("Obstacle in the way!");
                }

            }
        }
    }

    // 기즈모 그리기
    private void OnDrawGizmos()
    {
        // 원 범위표시
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector2 viewAngleA = DirFromAngle(-viewAngle / 2 + startingAngle, false);
        Vector2 viewAngleB = DirFromAngle(viewAngle / 2 + startingAngle, false);

        // x 값이 체크되어 있다면 시점 방향을 180도 돌립니다.
        if (GetComponent<SpriteRenderer>() != null && GetComponent<SpriteRenderer>().flipX)
        {
            viewAngleA = DirFromAngle(viewAngle / 2 + startingAngle + 180f, false);
            viewAngleB = DirFromAngle(-viewAngle / 2 + startingAngle + 180f, false);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)viewAngleB * viewRadius);

    }

    // 각도로부터 방향 벡터 반환
    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }

        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
    }

}