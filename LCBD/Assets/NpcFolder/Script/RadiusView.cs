using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusView : MonoBehaviour
{
    public float viewRadius;            // 시야 반경
    public LayerMask targetMask;        // Player와 상호작용할 레이어 마스크
    public LayerMask obstacleMask;      // 장애물 레이어 마스크

    private bool isInView;              // 시야에 있는지 여부

    public GameObject npcMenu; // NpcMenu 게임 오브젝트에 대한 참조
    private bool isMenuActive = false; // 메뉴 활성화 여부

    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0.5f);
    }

    // 일정 시간 간격으로 대상을 찾는 코루틴
    private IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    private void Update()
    {
        if (isInView && Input.GetKeyDown(KeyCode.R))
        {
            string objectTag = gameObject.tag;
            switch (objectTag)
            {
                case "shop":
                    Debug.Log("상점을 열었습니다.");
                    break;
                case "gambler":
                    Debug.Log("도박을 시작합니다.");
                    break;
                case "client":
                    Debug.Log("상담을 시작합니다.");
                    break;
                case "citizen":
                    Debug.Log("인사합니다.");
                    break;
                case "monster":
                    Debug.Log("몬스터를 공격합니다.");
                    break;
                default:
                    Debug.Log("해당 태그를 찾을 수 없습니다.");
                    break;
            }
            // R키를 누르면 대화 메뉴를 엽니다.
            if (npcMenu != null)
            {
                isMenuActive = !isMenuActive;
                npcMenu.SetActive(isMenuActive);
            }
        }
    }

    // 시야 내의 대상을 찾는 메소드
    private void FindVisibleTargets()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        isInView = false;

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Collider2D target = targetsInViewRadius[i];

            Vector2 dirToTarget = (target.transform.position - transform.position).normalized;

            // 시야 안에 장애물이 없으면
            if (!Physics2D.Raycast(transform.position, dirToTarget, viewRadius, obstacleMask))
            {
                // Player를 찾았을 때
                if (target.CompareTag("Player"))
                {
                    Debug.Log("플레이어를 찾았습니다.");
                    isInView = true;
                }
            }
            else
            {
                Debug.Log("시야 내에 장애물이 있습니다.");
            }
        }
    }

    // 시야 반경을 그리는 메소드
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
