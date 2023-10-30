using System;
using System.Collections;
using UnityEngine;

public class FieldPortal : MonoBehaviour
{
    //[SerializeField] private GameObject player;
    [SerializeField] private GameObject targetPos;

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

    private IEnumerator FieldTeleportCoroutine(GameObject obj)  //필드 포탈 텔레포트하는 코루틴
    {
        PortalManager.SetIsTeleporting(true);
        obj.transform.position = targetPos.transform.position;

        yield return new WaitForEndOfFrame();
        PortalManager.SetIsTeleporting(false);

        yield break;
    }
}
