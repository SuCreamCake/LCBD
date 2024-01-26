using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class FieldPortal : MonoBehaviour
{
    //[SerializeField] private GameObject player;
    [SerializeField] private GameObject targetPos;

    private StageGenerator stageGenerator;
    private MapGenerator[,] mapGenerator;
    Transform parent;

    private void Awake()
    {
        stageGenerator = FindObjectOfType<StageGenerator>();
        mapGenerator = stageGenerator.GetMapGenerator();

        parent = FindObjectOfType<GimmickObjectPlaceManager>().GimmickObjectsParent;
    }

    public void SetPortalPos(ObjectPoint portalPoint, int mapWidth, int mapHeight)
    {
        Vector3Int pos = new(portalPoint.FieldX * (mapWidth + 1) + portalPoint.MapX,
                          portalPoint.FieldY * (mapHeight + 1) + portalPoint.MapY);

        transform.position = pos;
        transform.Translate(0.5f, 0.5f, 0);
    }

    public void SetTargetPos(ObjectPoint portalPoint, int mapWidth, int mapHeight)
    {
        Vector3 pos = new(portalPoint.FieldX * (mapWidth + 1) + portalPoint.MapX,
                          portalPoint.FieldY * (mapHeight + 1) + portalPoint.MapY);

        targetPos.transform.position = pos;
        targetPos.transform.Translate(0.5f, 0.5f, 0);
    }

    public void Teleport(GameObject obj)
    {
        Debug.Log("FieldTeleport");

        if (PortalManager.IsTeleporting == false)
        {
            StartCoroutine(FieldTeleportCoroutine(obj));
        }
    }

    private IEnumerator FieldTeleportCoroutine(GameObject obj)  //필드 포탈 텔레포트하는 코루틴.
    {
        PortalManager.SetIsTeleporting(true);

        int x = (int)(targetPos.transform.position.x / (50 + 1));
        int y = (int)(targetPos.transform.position.y / (50 + 1));

        if (mapGenerator[x, y].Fields.IsClear)
        {
            // 이동할 필드가 클리어된 필드면 원래 연결된 지점으로 이동.
            obj.transform.position = targetPos.transform.position;
        }
        else
        {
            // 클리어된 필드가 아니면
            StringBuilder targetParentName = new();   //몬스터 스포너들을 담을 부모 오브젝트의 이름.
            targetParentName.Append("Field(").Append((char)(x + '0')).Append(", ").Append((char)(y + '0')).Append(")");
            
            if (parent.Find(targetParentName.ToString()).childCount > 0)   // 이동할 맵에 기믹 오브젝트가 있으면.
            {
                Transform startPoint = parent.Find(targetParentName.ToString()).GetChild(0).Find("mapStartPoint"); // 기믹의 시작 위치를 가져옴.
                if (startPoint != null)
                {
                    // 기믹의 시작위치가 있으면,
                    obj.transform.position = startPoint.position;           // 지정된 시작 지점으로 이동.
                }
                else
                {
                    // 기믹 오브젝트는 있지만, 기믹의 시작위치가 없다.
                    obj.transform.position = targetPos.transform.position;  // 원래 연결된 지점으로 이동.
                }
            }
            else
            {
                // 기믹 오브젝트가 없다.
                obj.transform.position = targetPos.transform.position;  // 원래 연결된 지점으로 이동.
            }
        }

        yield return new WaitForEndOfFrame();
        PortalManager.SetIsTeleporting(false);

        yield break;
    }
}
