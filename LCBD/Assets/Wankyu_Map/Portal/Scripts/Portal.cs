using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    protected GameObject player;
    [SerializeField]
    private GameObject targetPos;

    [field: SerializeField] public int MapX { get; private set; }
    [field: SerializeField] public int MapY { get; private set; }

    public virtual void SetTargetPos(int i, int j, int x, int y, int mapWidth, int mapHeight)
    {
        MapX = x;
        MapY = y;

        Vector3 pos = new(i * (mapWidth + 1) + MapX + 0.5f, j * (mapHeight + 1) + MapY + 0.5f);

        targetPos.transform.position = pos;
    }

    public virtual void Teleport()
    {
        Vector2 pos = targetPos.transform.position;

        player.GetComponent<Transform>().position = pos;
    }
}