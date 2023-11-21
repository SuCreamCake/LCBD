using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
    public static Point[] CommonFieldPortal_1_Birth = { new Point(47, 14), new Point(1, 32), new Point(29, 28), new Point(39, 12) };
    public static Point[] CommonFieldPortal_1_BabyBottle_g = { new Point(40, 26), new Point(10, 26), new Point(27, 47), new Point(25, 1) };
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


    private Dictionary<ObjectPoint, ObjectPoint> fieldPortalDict;   //연결된 필드 포탈 dict

    /*static 프로퍼티 및 메소드*/
    public static bool IsTeleporting { get; private set; } = false;     // 플레이어 캐릭터가 텔레포트(포탈을 타는 행위) 중인지
    public static void SetIsTeleporting(bool b) { IsTeleporting = b; }  // IsTeleporting setter 메소드

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
        fieldPortalDict = new Dictionary<ObjectPoint, ObjectPoint>();

        List<Tuple<FieldPoint, FieldPoint>> edges = stageGenerator.FieldsPointsEdges;
        FieldType[,] fieldTypes = stageGenerator.GetFieldType();
        int portalPointX;
        int portalPointY;

        // 위치 설정
        foreach (var edge in edges)
        {
            ObjectPoint keyPortalPoint, valuePortalPoint;

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

                        keyPortalPoint = new ObjectPoint(edge.Item1.X, edge.Item1.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Boss:
                        portalPointX = stageGenerator.MapWidth - 5 - 1;
                        portalPointY = 5;

                        keyPortalPoint = new ObjectPoint(edge.Item1.X, edge.Item1.Y, portalPointX, portalPointY);
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

                                    case CommonFieldSerial_1.BabyBottle_g:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_BabyBottle_g[0].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_BabyBottle_g[0].y;
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

                                    case CommonFieldSerial_1.BabyBottle_g:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_BabyBottle_g[0].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_BabyBottle_g[0].y;
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

                        keyPortalPoint = new ObjectPoint(edge.Item1.X, edge.Item1.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Special:
                        portalPointX = stageGenerator.MapWidth - 5 - 1;
                        portalPointY = 5;

                        keyPortalPoint = new ObjectPoint(edge.Item1.X, edge.Item1.Y, portalPointX, portalPointY);
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

                        valuePortalPoint = new ObjectPoint(edge.Item2.X, edge.Item2.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Boss:
                        portalPointX = 5;
                        portalPointY = 5;

                        valuePortalPoint = new ObjectPoint(edge.Item2.X, edge.Item2.Y, portalPointX, portalPointY);
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

                                    case CommonFieldSerial_1.BabyBottle_g:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_BabyBottle_g[1].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_BabyBottle_g[1].y;
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

                                    case CommonFieldSerial_1.BabyBottle_g:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_BabyBottle_g[1].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_BabyBottle_g[1].y;
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

                        valuePortalPoint = new ObjectPoint(edge.Item2.X, edge.Item2.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Special:
                        portalPointX = 5;
                        portalPointY = 5;

                        valuePortalPoint = new ObjectPoint(edge.Item2.X, edge.Item2.Y, portalPointX, portalPointY);
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

                        keyPortalPoint = new ObjectPoint(edge.Item1.X, edge.Item1.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Boss:
                        portalPointX = 5 + 3;
                        portalPointY = 9;
                        while (portalPointY <= stageGenerator.MapHeight - 8 - 8) { portalPointY += 8; }

                        keyPortalPoint = new ObjectPoint(edge.Item1.X, edge.Item1.Y, portalPointX, portalPointY);
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

                                    case CommonFieldSerial_1.BabyBottle_g:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_BabyBottle_g[2].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_BabyBottle_g[2].y;
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

                                    case CommonFieldSerial_1.BabyBottle_g:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_BabyBottle_g[2].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_BabyBottle_g[2].y;
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


                        keyPortalPoint = new ObjectPoint(edge.Item1.X, edge.Item1.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Special:
                        portalPointX = stageGenerator.MapWidth / 2 + 2;
                        portalPointY = 11;

                        while (portalPointY < stageGenerator.MapHeight - 5 - 5)
                        {
                            portalPointY += 5;
                        }


                        keyPortalPoint = new ObjectPoint(edge.Item1.X, edge.Item1.Y, portalPointX, portalPointY);
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


                        valuePortalPoint = new ObjectPoint(edge.Item2.X, edge.Item2.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Boss:
                        portalPointX = stageGenerator.MapWidth / 2 + 2;
                        portalPointY = 5;


                        valuePortalPoint = new ObjectPoint(edge.Item2.X, edge.Item2.Y, portalPointX, portalPointY);
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

                                    case CommonFieldSerial_1.BabyBottle_g:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_BabyBottle_g[3].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_BabyBottle_g[3].y;
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

                                    case CommonFieldSerial_1.BabyBottle_g:
                                        portalPointX = PortalLocation.CommonFieldPortal_1_BabyBottle_g[3].x;
                                        portalPointY = PortalLocation.CommonFieldPortal_1_BabyBottle_g[3].y;
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

                        valuePortalPoint = new ObjectPoint(edge.Item2.X, edge.Item2.Y, portalPointX, portalPointY);
                        break;

                    case FieldType.Special:
                        portalPointX = stageGenerator.MapWidth / 2 - 2;
                        portalPointY = 5;

                        valuePortalPoint = new ObjectPoint(edge.Item2.X, edge.Item2.Y, portalPointX, portalPointY);
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

        // 필드 포탈 오브젝트 생성
        List<GameObject> parentObjects = new List<GameObject>();  //필드 포탈들을 필드 별로 분리하여 담을 부모 오브젝트들 리스트
        foreach (var field in fieldPortalDict)
        {
            StringBuilder parentName = new();   // 필드 포탈을 담을 부모 오브젝트의 이름
            parentName.Append("Field(").Append((char)(field.Key.FieldX + '0')).Append(", ").Append((char)(field.Key.FieldY + '0')).Append(")");

            GameObject parentObject = null;

            foreach (var obj in parentObjects)  //부모 오브젝트 리스트를 돌면서 이미 존재하는 지 확인
            {
                if (obj.name.Equals(parentName.ToString())) //이름이 이미 존재하면,
                {
                    parentObject = obj; //찾아서 넣어주고
                    break;
                }
            }
            if (parentObject == null)   //없으면,
            {
                parentObject = new GameObject(parentName.ToString());   //새로 만들어줌
                parentObject.transform.parent = fieldPortalParent;      //부모도 필도포탈로 세팅해줌
                parentObjects.Add(parentObject);    //만들었으니까 리스트에 추가해줌
            }

            GameObject fieldPortalPrefab = Instantiate(FieldPortalObject, new(0, 0, 0), new(0, 0, 0, 0), parentObject.transform);   //필드에 맞는 부모에게 필드포탈 생성

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
