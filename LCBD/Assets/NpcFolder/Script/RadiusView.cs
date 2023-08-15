using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusView : MonoBehaviour
{
    public float viewRadius;            // �þ� �ݰ�
    public LayerMask targetMask;        // Player�� ������ Ÿ�� ���̾�
    public LayerMask obstacleMask;      // ������ ��ֹ� ���̾�

    private bool isInView;              // �þ� �ȿ� �ִ��� ����

    public GameObject npcMenu; // NpcMenu ������Ʈ�� �����մϴ�.
    private bool isMenuActive = false; // �޴� �¿���

    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0.5f);
    }

    // ���� �ð����� Ÿ�� �˻�
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
                    Debug.Log("�����Դϴ�.");
                    break;
                case "gambler":
                    Debug.Log("�������Դϴ�.");
                    break;
                case "client":
                    Debug.Log("�Ƿ����Դϴ�.");
                    break;
                case "citizen":
                    Debug.Log("�ֹ��Դϴ�.");
                    break;
                case "monster":
                    Debug.Log("�����Դϴ�.");
                    break;
                default:
                    Debug.Log("�±׸� ã�� �� �����ϴ�.");
                    break;
            }
            // RŰ�� �̿��� ��ǳ�� ���� �ڵ� 10�� ���ٷ� ����
            //NpcTextSc NpcTextSc = GetComponent<NpcTextSc>();
            //if (NpcTextSc != null)
            //{
            //    NpcTextSc.TalkNpc();
            //}
            if (npcMenu != null)
            {
                isMenuActive = !isMenuActive; // ���¸� ����մϴ�.
                npcMenu.SetActive(isMenuActive); // NpcMenu ������Ʈ�� Ȱ��ȭ ���¸� �����մϴ�.
            }
        }
    }

    // �ֺ��� �ִ� Ÿ�� �˻�
    private void FindVisibleTargets()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        isInView = false;

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Collider2D target = targetsInViewRadius[i];

            Vector2 dirToTarget = (target.transform.position - transform.position).normalized;

            // �þ� �ȿ� �ִµ� ��ֹ��� ������ �ִٸ� ����
            if (!Physics2D.Raycast(transform.position, dirToTarget, viewRadius, obstacleMask))
            {
                // Player�� �����ϴ� ���� �߰�
                if (target.CompareTag("Player"))
                {
                    Debug.Log("���� �÷��̾�");
                    isInView = true;
                }
            }
            else
            {
                Debug.Log("�繰 ���� ���� �÷��̾�");
            }
        }
    }

    // ����� �׸���
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
