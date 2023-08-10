using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PortalManager : MonoBehaviour
{
    StageGenerator stageGenerator;
    MapGenerator[,] mapGenerator;

    [field: SerializeField] public GameObject StagePortalObject { get; private set; }    //스테이지 포탈 지점 오브젝트

    public int StagePortalFieldX { get; private set; }
    public int StagePortalFieldY { get; private set; }
    public int StagePortalMapX { get; private set; }
    public int StagePortalMapY { get; private set; }

    [field: SerializeField]
    public Transform StagePortalParent { get; private set; }

    public void SetPortal()
    {
        stageGenerator = GetComponent<StageGenerator>();
        mapGenerator = stageGenerator.GetMapGenerator();

        SetMapPortal();
        SetFieldPortal();
        SetStagePortal();
    }

    private void SetMapPortal()
    {

    }

    private void SetFieldPortal()
    {
        List<Tuple<FieldPoint, FieldPoint>> edges = stageGenerator.FieldsPointsEdges;
        FieldType[,] fieldTypes = stageGenerator.GetFieldType();
        int portalPointX;
        int portalPointY;

        foreach (var edge in edges)
        {
            Debug.Log(edge.Item1.ToString() + edge.Item2.ToString());

            if (edge.Item1.X < edge.Item2.X)
            {
                if (edge.Item1.Y == edge.Item2.Y)
                {
                    Debug.Log("<<==");

                    portalPointX = stageGenerator.MapWidth - 2;
                    portalPointY = stageGenerator.MapHeight / 2 + 1;

                    while (mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Map[portalPointX, portalPointY] != 0)
                    {
                        portalPointX--;
                    }

                    mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Map[portalPointX, portalPointY] = 80;
                    mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Map[portalPointX, portalPointY - 1] = 1;


                    portalPointX = 1;
                    portalPointY = stageGenerator.MapHeight / 2 + 1;
                    while (mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Map[portalPointX, portalPointY] != 0)
                    {
                        portalPointX++;
                    }

                    mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Map[portalPointX, portalPointY] = 80;
                    mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Map[portalPointX, portalPointY - 1] = 1;
                }
            }
            else if(edge.Item1.X == edge.Item2.X)
            {
                if (edge.Item1.Y < edge.Item2.Y)
                {
                    Debug.Log("==<<");

                    portalPointX = stageGenerator.MapWidth / 2;
                    portalPointY = stageGenerator.MapHeight - 2;

                    while (mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Map[portalPointX, portalPointY] != 0)
                    {
                        portalPointY--;
                    }

                    mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Map[portalPointX, portalPointY] = 80;
                    mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Map[portalPointX, portalPointY - 1] = 1;


                    portalPointX = stageGenerator.MapWidth / 2;
                    portalPointY = 1;
                    while (mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Map[portalPointX, portalPointY] != 0)
                    {
                        portalPointY++;
                    }

                    mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Map[portalPointX, portalPointY] = 80;
                    mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Map[portalPointX, portalPointY - 1] = 1;
                }
            }

            switch (fieldTypes[edge.Item1.X, edge.Item1.Y])
            {
                case FieldType.None:
                    break;
                case FieldType.Start:
                    break;
                case FieldType.Boss:
                    break;
                case FieldType.Common:
                    break;
                case FieldType.Special:
                    break;
                default:
                    break;
            }
            switch (fieldTypes[edge.Item2.X, edge.Item2.Y])
            {
                case FieldType.None:
                    break;
                case FieldType.Start:
                    break;
                case FieldType.Boss:
                    break;
                case FieldType.Common:
                    break;
                case FieldType.Special:
                    break;
                default:
                    break;
            }
        }

    }

    private void SetStagePortal()
    {
        stageGenerator = GetComponent<StageGenerator>();

        mapGenerator = stageGenerator.GetMapGenerator();

        if (mapGenerator != null)
        {
            for (int i = 0; i < mapGenerator.GetLength(0); i++)
            {
                for (int j = 0; j < mapGenerator.GetLength(1); j++)
                {
                    int[,] map = mapGenerator[i, j].Fields.Map;

                    for (int x = 0; x < map.GetLength(0); x++)
                    {
                        for (int y = 0; y < map.GetLength(1); y++)
                        {
                            if (map[x, y] == 95)   //스테이지 포탈 위치
                            {
                                StagePortalFieldX = i;
                                StagePortalFieldY = j;
                                StagePortalMapX = x;
                                StagePortalMapY = y;

                                Vector3Int pos = new(i * (map.GetLength(0) + 1) + x, j * (map.GetLength(1) + 1) + y, 0);

                                StagePortalObject.transform.position = pos;
                                StagePortalObject.transform.Translate(0.5f, 0.5f, 0);

                                Instantiate(StagePortalObject, StagePortalObject.transform.position, StagePortalObject.transform.rotation, StagePortalParent);
                            }
                        }
                    }
                }
            }
        }
    }
}
