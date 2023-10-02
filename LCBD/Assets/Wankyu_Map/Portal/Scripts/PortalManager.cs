﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class PortalLocation  // 포탈 위치 좌표 static 클래스
{
    public class Point
    {
        public int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    // [0]=좌(맵의 오른쪽 포탈) / [1]=우(맵의 왼쪽 포탈) / [2]=하(맵의 위쪽 포탈) / [3]=상(맵의 아래쪽 포탈)
    public static Point[] CommonFieldPortal_1_Crying = { new Point(47, 21), new Point(1, 19), new Point(15, 42), new Point(13, 5) };
    public static Point[] CommonFieldPortal_1_Chromosome = { new Point(34, 29), new Point(17, 17), new Point(38, 47), new Point(31, 2) };
    public static Point[] CommonFieldPortal_1_Birth = { new Point(47, 14), new Point(32, 1), new Point(29, 28), new Point(39, 12) };
}

public class PortalManager : MonoBehaviour
{
    StageGenerator stageGenerator;
    MapGenerator[,] mapGenerator;

    [field: SerializeField] public GameObject StagePortalObject { get; private set; }    //스테이지 포탈 지점 오브젝트
    [field: SerializeField] public GameObject FieldPortalObject { get; private set; }    //필드 포탈 지점 오브젝트

    public int StagePortalFieldX { get; private set; }
    public int StagePortalFieldY { get; private set; }
    public int StagePortalMapX { get; private set; }
    public int StagePortalMapY { get; private set; }

    [SerializeField] private Transform stagePortalParent;
    [SerializeField] private Transform fieldPortalParent;


    private Dictionary<PortalPoint, PortalPoint> fieldPortalDict;   //연결된 필드 포탈 dict

    /*static 프로퍼티 및 메소드*/
    public static bool IsTeleporting { get; private set; } = false;     // 플레이어 캐릭터가 텔레포트(포탈을 타는 행위) 중인지
    public static void setIsTeleporting(bool b) { IsTeleporting = b; }  // IsTeleporting setter 메소드

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
        // TODO
    }

    private void SetFieldPortal()
    {
        fieldPortalDict = new Dictionary<PortalPoint, PortalPoint>();

        List<Tuple<FieldPoint, FieldPoint>> edges = stageGenerator.FieldsPointsEdges;
        FieldType[,] fieldTypes = stageGenerator.GetFieldType();
        int portalPointX;
        int portalPointY;

        // 위치 설정
        foreach (var edge in edges)
        {
            PortalPoint keyPortalPoint, valuePortalPoint;

            if (edge.Item1.X < edge.Item2.X && edge.Item1.Y == edge.Item2.Y)   //가로 연결 (Item1(좌) - Item2(우))
            {
                switch (fieldTypes[edge.Item1.X, edge.Item1.Y])     //Item1(좌)
                {
                    case FieldType.None:
                        keyPortalPoint = null;
                        break;

                    case FieldType.Start:
                        portalPointX = stageGenerator.MapWidth / 4 * 3;
                        portalPointY = stageGenerator.MapHeight / 2;

                        //mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Map[portalPointX, portalPointY] = 80;
                        keyPortalPoint = new PortalPoint(edge.Item1.X, edge.Item1.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Boss:
                        portalPointX = stageGenerator.MapWidth - 5 - 1;
                        portalPointY = 5;

                        mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Map[portalPointX, portalPointY] = 80;
                        keyPortalPoint = new PortalPoint(edge.Item1.X, edge.Item1.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Common:
                        portalPointX = 0;
                        portalPointY = 0;

                        switch (stageGenerator.StageLevel)
                        {
                            case 1:
                                switch ((CommonFieldSerial_1)mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Serial)
                                {
                                    case CommonFieldSerial_1.Crying:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Crying[0].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Crying[0].y;
                                        break;

                                    case CommonFieldSerial_1.Chromosome:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Chromosome[0].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Chromosome[0].y;
                                        break;

                                    case CommonFieldSerial_1.Birth:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Birth[0].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Birth[0].y;
                                        break;
                                }
                                break;
                            case 2:
                                // TODO (현재 1스테 포탈 위치)
                                switch ((CommonFieldSerial_1)mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Serial)
                                {
                                    case CommonFieldSerial_1.Crying:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Crying[0].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Crying[0].y;
                                        break;

                                    case CommonFieldSerial_1.Chromosome:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Chromosome[0].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Chromosome[0].y;
                                        break;

                                    case CommonFieldSerial_1.Birth:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Birth[0].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Birth[0].y;
                                        break;
                                }
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            case 5:
                                break;
                        }

                        mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Map[portalPointX, portalPointY] = 80;
                        keyPortalPoint = new PortalPoint(edge.Item1.X, edge.Item1.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Special:
                        portalPointX = stageGenerator.MapWidth - 5 - 1;
                        portalPointY = 5;

                        mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Map[portalPointX, portalPointY] = 80;
                        keyPortalPoint = new PortalPoint(edge.Item1.X, edge.Item1.Y, portalPointX, portalPointY);
                        break;

                    default:
                        keyPortalPoint = null;
                        break;
                }
                switch (fieldTypes[edge.Item2.X, edge.Item2.Y])     //Item2(우)
                {
                    case FieldType.None:
                        valuePortalPoint = null;
                        break;

                    case FieldType.Start:
                        portalPointX = stageGenerator.MapWidth / 4;
                        portalPointY = stageGenerator.MapHeight / 2;

                        mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Map[portalPointX, portalPointY] = 80;
                        valuePortalPoint = new PortalPoint(edge.Item2.X, edge.Item2.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Boss:
                        portalPointX = 5;
                        portalPointY = 5;

                        mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Map[portalPointX, portalPointY] = 80;
                        valuePortalPoint = new PortalPoint(edge.Item2.X, edge.Item2.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Common:
                        portalPointX = 0;
                        portalPointY = 0;

                        switch (stageGenerator.StageLevel)
                        {
                            case 1:
                                switch ((CommonFieldSerial_1)mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Serial)
                                {
                                    case CommonFieldSerial_1.Crying:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Crying[1].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Crying[1].y;
                                        break;

                                    case CommonFieldSerial_1.Chromosome:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Chromosome[1].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Chromosome[1].y;
                                        break;

                                    case CommonFieldSerial_1.Birth:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Birth[1].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Birth[1].y;
                                        break;
                                }
                                break;
                            case 2:
                                // TODO (현재 1스테 포탈 위치)
                                switch ((CommonFieldSerial_1)mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Serial)
                                {
                                    case CommonFieldSerial_1.Crying:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Crying[1].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Crying[1].y;
                                        break;

                                    case CommonFieldSerial_1.Chromosome:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Chromosome[1].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Chromosome[1].y;
                                        break;

                                    case CommonFieldSerial_1.Birth:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Birth[1].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Birth[1].y;
                                        break;
                                }
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            case 5:
                                break;
                        }

                        mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Map[portalPointX, portalPointY] = 80;
                        valuePortalPoint = new PortalPoint(edge.Item2.X, edge.Item2.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Special:
                        portalPointX = 5;
                        portalPointY = 5;

                        mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Map[portalPointX, portalPointY] = 80;
                        valuePortalPoint = new PortalPoint(edge.Item2.X, edge.Item2.Y, portalPointX, portalPointY);
                        break;

                    default:
                        valuePortalPoint = null;
                        break;
                }

                if (keyPortalPoint != null && valuePortalPoint != null)
                {
                    fieldPortalDict.Add(keyPortalPoint, valuePortalPoint);
                    fieldPortalDict.Add(valuePortalPoint, keyPortalPoint);
                }
            }
            else if (edge.Item1.X == edge.Item2.X && edge.Item1.Y < edge.Item2.Y)   //세로 연결 (Item1(하) - Item2(상))
            {
                switch (fieldTypes[edge.Item1.X, edge.Item1.Y])     //Item1(하)
                {
                    case FieldType.None:
                        keyPortalPoint = null;
                        break;

                    case FieldType.Start:
                        portalPointX = stageGenerator.MapWidth / 2;
                        portalPointY = stageGenerator.MapHeight / 4 * 3 - 1;

                        mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Map[portalPointX, portalPointY] = 80;
                        keyPortalPoint = new PortalPoint(edge.Item1.X, edge.Item1.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Boss:
                        portalPointX = 5 + 3;
                        portalPointY = 9;
                        while (portalPointY <= stageGenerator.MapHeight - 8 - 8) { portalPointY += 8; }

                        mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Map[portalPointX, portalPointY] = 80;
                        keyPortalPoint = new PortalPoint(edge.Item1.X, edge.Item1.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Common:
                        portalPointX = 0;
                        portalPointY = 0;

                        switch (stageGenerator.StageLevel)
                        {
                            case 1:
                                switch ((CommonFieldSerial_1)mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Serial)
                                {
                                    case CommonFieldSerial_1.Crying:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Crying[2].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Crying[2].y;
                                        break;

                                    case CommonFieldSerial_1.Chromosome:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Chromosome[2].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Chromosome[2].y;
                                        break;

                                    case CommonFieldSerial_1.Birth:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Birth[2].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Birth[2].y;
                                        break;
                                }
                                break;
                            case 2:
                                // TODO (현재 1스테 포탈 위치)
                                switch ((CommonFieldSerial_1)mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Serial)
                                {
                                    case CommonFieldSerial_1.Crying:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Crying[2].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Crying[2].y;
                                        break;

                                    case CommonFieldSerial_1.Chromosome:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Chromosome[2].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Chromosome[2].y;
                                        break;

                                    case CommonFieldSerial_1.Birth:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Birth[2].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Birth[2].y;
                                        break;
                                }
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            case 5:
                                break;
                        }

                        mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Map[portalPointX, portalPointY] = 80;
                        keyPortalPoint = new PortalPoint(edge.Item1.X, edge.Item1.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Special:
                        portalPointX = stageGenerator.MapWidth / 2 + 2;
                        portalPointY = 11;

                        while (portalPointY < stageGenerator.MapHeight - 5 - 5)
                        {
                            portalPointY += 5;
                        }

                        mapGenerator[edge.Item1.X, edge.Item1.Y].Fields.Map[portalPointX, portalPointY] = 80;
                        keyPortalPoint = new PortalPoint(edge.Item1.X, edge.Item1.Y, portalPointX, portalPointY);
                        break;

                    default:
                        keyPortalPoint = null;
                        break;
                }
                switch (fieldTypes[edge.Item2.X, edge.Item2.Y])     //Item2(상)
                {
                    case FieldType.None:
                        valuePortalPoint = null;
                        break;

                    case FieldType.Start:
                        portalPointX = stageGenerator.MapWidth / 2;
                        portalPointY = stageGenerator.MapHeight / 4;

                        mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Map[portalPointX, portalPointY] = 80;
                        valuePortalPoint = new PortalPoint(edge.Item2.X, edge.Item2.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Boss:
                        portalPointX = stageGenerator.MapWidth / 2 + 2;
                        portalPointY = 5;

                        mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Map[portalPointX, portalPointY] = 80;
                        valuePortalPoint = new PortalPoint(edge.Item2.X, edge.Item2.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Common:
                        portalPointX = 0;
                        portalPointY = 0;

                        switch (stageGenerator.StageLevel)
                        {
                            case 1:
                                switch ((CommonFieldSerial_1)mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Serial)
                                {
                                    case CommonFieldSerial_1.Crying:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Crying[3].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Crying[3].y;
                                        break;

                                    case CommonFieldSerial_1.Chromosome:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Chromosome[3].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Chromosome[3].y;
                                        break;

                                    case CommonFieldSerial_1.Birth:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Birth[3].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Birth[3].y;
                                        break;
                                }
                                break;
                            case 2:
                                // TODO (현재 1스테 포탈 위치)
                                switch ((CommonFieldSerial_1)mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Serial)
                                {
                                    case CommonFieldSerial_1.Crying:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Crying[3].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Crying[3].y;
                                        break;

                                    case CommonFieldSerial_1.Chromosome:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Chromosome[3].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Chromosome[3].y;
                                        break;

                                    case CommonFieldSerial_1.Birth:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_Birth[3].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_Birth[3].y;
                                        break;
                                }
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            case 5:
                                break;
                        }

                        mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Map[portalPointX, portalPointY] = 80;
                        valuePortalPoint = new PortalPoint(edge.Item2.X, edge.Item2.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Special:
                        portalPointX = stageGenerator.MapWidth / 2 - 2;
                        portalPointY = 5;

                        mapGenerator[edge.Item2.X, edge.Item2.Y].Fields.Map[portalPointX, portalPointY] = 80;
                        valuePortalPoint = new PortalPoint(edge.Item2.X, edge.Item2.Y, portalPointX, portalPointY);
                        break;

                    default:
                        valuePortalPoint = null;
                        break;
                }

                if (keyPortalPoint != null && valuePortalPoint != null)
                {
                    fieldPortalDict.Add(keyPortalPoint, valuePortalPoint);
                    fieldPortalDict.Add(valuePortalPoint, keyPortalPoint);
                }
            }
        }

        // 포탈 생성
        foreach (var field in fieldPortalDict)
        {
            //GameObject obj = null;
            //GameObject fieldIndex = Instantiate(obj);
            //fieldIndex.name = "a";
            GameObject fieldPortalPrefab = Instantiate(FieldPortalObject, new(0, 0, 0), new(0, 0, 0, 0), fieldPortalParent);

            fieldPortalPrefab.GetComponent<FieldPortal>().SetPortalPos(field.Key, stageGenerator.MapWidth, stageGenerator.MapHeight);
            fieldPortalPrefab.GetComponent<FieldPortal>().SetTargetPos(field.Value, stageGenerator.MapWidth, stageGenerator.MapHeight);
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

                                GameObject stagePortal = Instantiate(StagePortalObject, StagePortalObject.transform.position, StagePortalObject.transform.rotation, stagePortalParent);
                                stagePortal.transform.position = pos;
                                stagePortal.transform.Translate(0.5f, 0.5f, 0);
                            }
                        }
                    }
                }
            }
        }
    }
}
