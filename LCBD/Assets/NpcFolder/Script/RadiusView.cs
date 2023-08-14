using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusView : MonoBehaviour
{
    public float viewRadius;            // 시야 반경
    public LayerMask targetMask;        // Player를 포함한 타겟 레이어
    public LayerMask obstacleMask;      // 가리는 장애물 레이어

    private bool isInView;              // 시야 안에 있는지 여부

    public GameObject npcMenu; // NpcMenu 오브젝트를 연결합니다.
    private bool isMenuActive = false; // 메뉴 온오프

    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0.5f);
    }

    // 일정 시간마다 타겟 검색
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
                    Debug.Log("상인입니다.");
                    break;
                case "gambler":
                    Debug.Log("놀음꾼입니다.");
                    break;
                case "client":
                    Debug.Log("의뢰인입니다.");
                    break;
                case "citizen":
                    Debug.Log("주민입니다.");
                    break;
                case "monster":
                    Debug.Log("몬스터입니다.");
                    break;
                default:
                    Debug.Log("태그를 찾을 수 없습니다.");
                    break;
            }
            // R키를 이용한 말풍선 띄우기 자동 10초 마다로 변경
            //NpcTextSc NpcTextSc = GetComponent<NpcTextSc>();
            //if (NpcTextSc != null)
            //{
            //    NpcTextSc.TalkNpc();
            //}
            if (npcMenu != null)
            {
                isMenuActive = !isMenuActive; // 상태를 토글합니다.
                npcMenu.SetActive(isMenuActive); // NpcMenu 오브젝트의 활성화 상태를 변경합니다.
            }
        }
    }

    // 주변에 있는 타겟 검색
    private void FindVisibleTargets()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        isInView = false;

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Collider2D target = targetsInViewRadius[i];

            Vector2 dirToTarget = (target.transform.position - transform.position).normalized;

            // 시야 안에 있는데 장애물이 가리고 있다면 무시
            if (!Physics2D.Raycast(transform.position, dirToTarget, viewRadius, obstacleMask))
            {
                // Player에 반응하는 로직 추가
                if (target.CompareTag("Player"))
                {
                    Debug.Log("접촉 플레이어");
                    isInView = true;
                }
            }
            else
            {
                Debug.Log("사물 사이 접촉 플레이어");
            }
        }
    }

    // 기즈모 그리기
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
