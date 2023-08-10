using System;
using UnityEngine;

public class FieldPortal : Portal
{
    [field: SerializeField] public int FieldX { get; private set; }
    [field: SerializeField] public int FieldY { get; private set; }

    [SerializeField]
    private GameObject targetFieldPortal;

    public override void SetTargetPos(int i, int j, int x, int y, int mapWidth, int mapHeight)
    {
        FieldX = i;
        FieldY = j;
        base.SetTargetPos(i, j, x, y, mapWidth, mapHeight);
    }

    public override void Teleport()
    {
        Vector2 pos = targetFieldPortal.transform.position;

        player.GetComponent<Transform>().position = pos;
    }
}
