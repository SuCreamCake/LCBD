using System;
using UnityEngine;

public class FieldPortal : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject targetPos;

    public void SetPortalPos(PortalPoint portalPoint, int mapWidth, int mapHeight)
    {
        Vector3Int pos = new(portalPoint.FieldX * (mapWidth + 1) + portalPoint.MapX,
                          portalPoint.FieldY * (mapHeight + 1) + portalPoint.MapY);

        transform.position = pos;
        transform.Translate(0.5f, 0.5f, 0);
    }

    public void SetTargetPos(PortalPoint portalPoint, int mapWidth, int mapHeight)
    {
        Vector3 pos = new(portalPoint.FieldX * (mapWidth + 1) + portalPoint.MapX,
                          portalPoint.FieldY * (mapHeight + 1) + portalPoint.MapY);

        targetPos.transform.position = pos;
        targetPos.transform.Translate(0.5f, 0.5f, 0);
    }

    public void Teleport(GameObject obj)
    {
        player = obj;
        player.GetComponent<Transform>().position = targetPos.transform.position;
    }
}
