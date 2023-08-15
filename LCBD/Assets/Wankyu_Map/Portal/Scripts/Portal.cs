using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour //TODO: 포탈 어떻게 배치할 건지 생각해야 함.
{
    [SerializeField]
    protected GameObject player;
    [SerializeField]
    private GameObject targetPos;

    public void SetTargetPos(PortalPoint portalPoint, int mapWidth, int mapHeight)
    {
        Vector3 pos = new(portalPoint.FieldX * (mapWidth + 1) + portalPoint.MapX, portalPoint.FieldY * (mapHeight + 1) + portalPoint.MapY);
        
        targetPos.transform.position = pos;
        targetPos.transform.Translate(0.5f, 0.5f, 0);
    }

    public void Teleport()
    {
        Vector2 pos = targetPos.transform.position;

        player.GetComponent<Transform>().position = pos;
    }
}