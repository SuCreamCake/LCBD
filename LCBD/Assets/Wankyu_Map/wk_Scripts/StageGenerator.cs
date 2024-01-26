using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField, Range(1, 5)]
    private int _stageLevel;    //스테이지 레벨
    public int StageLevel { get { return _stageLevel; } private set { _stageLevel = value; } }

    public int FieldCount { get; private set; } // 필드의 개수

    public static int CommonFieldCount { get; private set; }   // 일반필드의 개수 (static)
    public static int SpecialFieldCount { get; private set; }  // 특수필드의 개수 (static)
    public static Dictionary<int, int> CommonFieldGeneratedCount { get; private set; }          // 생성된 일반필드 개수 dict (static)
    public static Dictionary<int, float> CommonFieldWeights { get; private set; }               // 일반필드의 가중치 dict (static)
    public static void ChangeWeightInCommonFieldWeights(int commonFieldSerial, float weight)    // 일반필드의 가중치 변경 수정 메소드 (static)
    {
        CommonFieldWeights[commonFieldSerial] = weight;
    }
    public static void InitCommonFieldWeightsAndGeneratedCount(int StageLevel)     // 일반 필드의 유형들의 생성 개수 및 필드 가중치를 (스테이지 레벨과 레벨의 유형에 맞게) 초기화하는 메소드 (static)
    {
        CommonFieldGeneratedCount = new Dictionary<int, int>(); //일반 필드 생성 개수 dict 초기화
        CommonFieldWeights = new Dictionary<int, float>();      //일반 필드 가중치 dict 초기화

        switch (StageLevel)
        {
            case 1:
                foreach (CommonFieldSerial_1 commonItem in Enum.GetValues(typeof(CommonFieldSerial_1)))
                {
                    CommonFieldWeights.Add((int)commonItem, CommonFieldCount);    //가중치 초기화
                    CommonFieldGeneratedCount.Add((int)commonItem, 0);            //생성개수 초기화
                }
                break;
            case 2:
                foreach (CommonFieldSerial_2 commonItem in Enum.GetValues(typeof(CommonFieldSerial_2)))
                {
                    CommonFieldWeights.Add((int)commonItem, CommonFieldCount);    //가중치 초기화
                    CommonFieldGeneratedCount.Add((int)commonItem, 0);            //생성개수 초기화
                }
                break;
            case 3:
                foreach (CommonFieldSerial_3 commonItem in Enum.GetValues(typeof(CommonFieldSerial_3)))
                {
                    CommonFieldWeights.Add((int)commonItem, CommonFieldCount);    //가중치 초기화
                    CommonFieldGeneratedCount.Add((int)commonItem, 0);            //생성개수 초기화
                }
                break;
            case 4:
                foreach (CommonFieldSerial_4 commonItem in Enum.GetValues(typeof(CommonFieldSerial_4)))
                {
                    CommonFieldWeights.Add((int)commonItem, CommonFieldCount);    //가중치 초기화
                    CommonFieldGeneratedCount.Add((int)commonItem, 0);            //생성개수 초기화
                }
                break;
            case 5:
                foreach (CommonFieldSerial_5 commonItem in Enum.GetValues(typeof(CommonFieldSerial_5)))
                {
                    CommonFieldWeights.Add((int)commonItem, CommonFieldCount);    //가중치 초기화
                    CommonFieldGeneratedCount.Add((int)commonItem, 0);            //생성개수 초기화
                }
                break;
        }
        return;
    }
    public static Dictionary<int, int> SpecialFieldGeneratedCount { get; private set; }         // 생성된 특수필드 개수 dict (static)
    public static Dictionary<int, float> SpecialFieldWeights { get; private set; }              // 특수필드의 가중치 dict (static)
    public static void ChangeWeightInSpecialFieldWeights(int specialFieldSerial, float weight)  // 특수필드의 가중치 변경 수정 메소드 (static)
    {
        SpecialFieldWeights[specialFieldSerial] = weight;
    }
    public static void InitSpecialFieldWeightsAndGeneratedCount(int StageLevel)     // 특수 필드의 유형들의 생성 개수 및 필드 가중치를 (스테이지 레벨과 레벨의 유형에 맞게) 초기화하는 메소드 (static)
    {
        SpecialFieldGeneratedCount = new Dictionary<int, int>(); // 특수 필드 생성 개수 dict 초기화
        SpecialFieldWeights = new Dictionary<int, float>();      // 특수 필드 가중치 dict 초기화

        switch (StageLevel)
        {
            case 1:
                foreach (SpecialFieldSerial_1 specialItem in Enum.GetValues(typeof(SpecialFieldSerial_1)))
                {
                    SpecialFieldWeights.Add((int)specialItem, SpecialFieldCount);   //가중치 초기화
                    SpecialFieldGeneratedCount.Add((int)specialItem, 0);            //생성개수 초기화
                }
                break;
            case 2:
                foreach (SpecialFieldSerial_2 specialItem in Enum.GetValues(typeof(SpecialFieldSerial_2)))
                {
                    SpecialFieldWeights.Add((int)specialItem, SpecialFieldCount);   //가중치 초기화
                    SpecialFieldGeneratedCount.Add((int)specialItem, 0);            //생성개수 초기화
                }
                break;
            case 3:
                foreach (SpecialFieldSerial_3 specialItem in Enum.GetValues(typeof(SpecialFieldSerial_3)))
                {
                    SpecialFieldWeights.Add((int)specialItem, SpecialFieldCount);   //가중치 초기화
                    SpecialFieldGeneratedCount.Add((int)specialItem, 0);            //생성개수 초기화
                }
                break;
            case 4:
                foreach (SpecialFieldSerial_4 specialItem in Enum.GetValues(typeof(SpecialFieldSerial_4)))
                {
                    SpecialFieldWeights.Add((int)specialItem, SpecialFieldCount);   //가중치 초기화
                    SpecialFieldGeneratedCount.Add((int)specialItem, 0);            //생성개수 초기화
                }
                break;
            case 5:
                foreach (SpecialFieldSerial_5 specialItem in Enum.GetValues(typeof(SpecialFieldSerial_5)))
                {
                    SpecialFieldWeights.Add((int)specialItem, SpecialFieldCount);   //가중치 초기화
                    SpecialFieldGeneratedCount.Add((int)specialItem, 0);            //생성개수 초기화
                }
                break;
        }
        return;
    }


    public int FieldSquareMatrixRow { get; private set; } //필드의 NxN 배열의 N(행 수)
    private int _mapWidth, _mapHeight;  //한 맵(필드)의 너비, 높이
    public int MapWidth { get { return _mapWidth; } private set { _mapWidth = value; } }
    public int MapHeight { get { return _mapHeight; } private set { _mapHeight = value; } }

    public int StartRow { get; private set; }   //시작필드의 행
    public int StartCol { get; private set; }   //시작필드의 열

    public List<Tuple<FieldPoint, FieldPoint>> FieldsPointsEdges { get; private set; }  //None 필드를 제외한 각 필드 간의 연결되는 두 필드(주변 필드)들을 쌍으로 저장하는 리스트

    //fieldsPointsEdges 를 초기화하고 연결되는 필드(주변 필드)들을 모두 저장하는 메소드
    private void InitFieldsPointsEdges()
    {
        FieldsPointsEdges = new List<Tuple<FieldPoint, FieldPoint>>();

        //가로 연결
        for (int x = 0; x < FieldSquareMatrixRow - 1; x++)
        {
            for (int y = 0; y < FieldSquareMatrixRow; y++)
            {
                if (fieldType[x, y] != FieldType.None && fieldType[x + 1, y] != FieldType.None)
                {
                    FieldPoint field1 = new(x, y);
                    FieldPoint field2 = new(x + 1, y);

                    Tuple<FieldPoint, FieldPoint> linkedField = new(field1, field2);
                    FieldsPointsEdges.Add(linkedField);
                }
            }
        }

        //세로 연결
        for (int x = 0; x < FieldSquareMatrixRow; x++)
        {
            for (int y = 0; y < FieldSquareMatrixRow - 1; y++)
            {
                if (fieldType[x, y] != FieldType.None && fieldType[x, y + 1] != FieldType.None)
                {
                    FieldPoint field1 = new(x, y);
                    FieldPoint field2 = new(x, y + 1);

                    Tuple<FieldPoint, FieldPoint> linkedField = new(field1, field2);
                    FieldsPointsEdges.Add(linkedField);
                }
            }
        }
    }

    public List<FieldPoint> FieldsPointsVertex { get; private set; }    //None 필드를 제외한 필드들을 저장하는 리스트

    //fieldsPointsVertex를 초기화하고 None 필드를 제외한 필드의 좌표를 저장하는 메소드
    private void InitFieldsPointsVertex()
    {
        FieldsPointsVertex = new List<FieldPoint>();
        for (int x = 0; x < FieldSquareMatrixRow; x++)
        {
            for (int y = 0; y < FieldSquareMatrixRow; y++)
            {
                if (fieldType[x, y] != FieldType.None)
                {
                    FieldsPointsVertex.Add(new FieldPoint(x, y));
                }
            }
        }
    }


    FieldType[,] fieldType; //각 맵의 필드 타입
    public FieldType[,] GetFieldType() { return fieldType; }

    MapGenerator[,] mapGenerator;   //각 맵(필드)의 생성기
    public MapGenerator[,] GetMapGenerator() { return mapGenerator; }

    // 스테이지 생성.
    public void GenerateStage()
    {
        MapWidth = 50;
        MapHeight = 50;

        FieldCount = 5 * StageLevel + UnityEngine.Random.Range(3, 7);               // 필드 개수. 스테이지 레벨 * 5 + (3~6난수)
        FieldSquareMatrixRow = Mathf.FloorToInt(Mathf.Sqrt(FieldCount + 20)) + 1;   // 필드 배치가 n*n의 2차원이라서, n 계산.

        Debug.Log("roomCount, stageLevel : " + FieldCount + ", " + StageLevel);
        Debug.Log("FieldSquareMatrixRow : " + FieldSquareMatrixRow);

        GenerateFields();

        Debug.Log("SpecialFieldCount : " + SpecialFieldCount);  //특수 필드로 지정된 필드 개수
        Debug.Log("CommonFieldCount : " + CommonFieldCount);    //일반 필드로 지정된 필드 개수

        InitCommonFieldWeightsAndGeneratedCount(StageLevel);    // 일반 필드의 유형들의 생성 개수 및 필드 가중치 초기화 (스테이지 레벨과 레벨의 유형에 맞게)
        InitSpecialFieldWeightsAndGeneratedCount(StageLevel);   // 특수 필드의 유형들의 생성 개수 및 필드 가중치 초기화 (스테이지 레벨과 레벨의 유형에 맞게)


        DrawFields();
    }

    //각 맵의 필드 타입에 맞게 맵 생성하는 메소드 + 스테이지 레벨에 맞게
    private void DrawFields()
    {
        mapGenerator = new MapGenerator[FieldSquareMatrixRow, FieldSquareMatrixRow];

        for (int x = 0; x < FieldSquareMatrixRow; x++)
        {
            for (int y = 0; y < FieldSquareMatrixRow; y++)
            {
                mapGenerator[x, y] = new MapGenerator();
                mapGenerator[x, y].InitMap(MapWidth, MapHeight, fieldType[x, y], StageLevel);
            }
        }
    }

    //모든 맵을 초기화 하는 메소드
    private void InitFields()
    {
        fieldType = new FieldType[FieldSquareMatrixRow, FieldSquareMatrixRow];

        for (int i = 0; i < fieldType.GetLength(0); i++)
        {
            for (int j = 0; j < fieldType.GetLength(1); j++)
            {
                fieldType[i, j] = FieldType.None;
            }
        }
    }

    //스테이지의 맵을 배치하고 각 맵의 필드 타입을 지정하는 메소드
    private void GenerateFields()
    {
        InitFields();

        StartRow = UnityEngine.Random.Range(0, fieldType.GetLength(0));
        StartCol = UnityEngine.Random.Range(0, fieldType.GetLength(1));

        //랜덤워크알고리즘 비슷한건데, 하나만 하면 생기는 모양새가 포도송이처럼 뭉치는 경우가 많아서 좀 더 다양하고, 좀 더 길쭉한 모양새도 더 쉽게 나올 수 있도록 두 개로 함.
        //랜덤 워크1
        int curRow = StartRow;  //현재 행1
        int curCol = StartCol;  //현재 열1
        //랜덤 워크2
        int curRow2 = StartRow; //현재 행2
        int curCol2 = StartCol; //현재 열2

        //시작 필드 생성 부분
        fieldType[StartRow, StartCol] = FieldType.Start;

        int count = 1;  //생성된 필드의 수

        //일반 필드 생성 부분
        int loopCount = 0;  //while문 loop Count
        while (count < FieldCount)
        {
            int current = UnityEngine.Random.Range(0, 2);   //0 : 현재 행,열 1(curRow, curCol) / 1 : 현재 행,열 2(curRow2, curCol2)
            int direction = UnityEngine.Random.Range(1, 5); // 1. ↑ / 2. ↓ / 3. → / 4. ←
            switch (current)
            {
                case 0:
                    switch (direction)
                    {
                        case 1:
                            if (curRow < FieldSquareMatrixRow - 1)
                                curRow++;
                            break;
                        case 2:
                            if (curRow > 0)
                                curRow--;
                            break;
                        case 3:
                            if (curCol < FieldSquareMatrixRow - 1)
                                curCol++;
                            break;
                        case 4:
                            if (curCol > 0)
                                curCol--;
                            break;
                    }

                    if (fieldType[curRow, curCol] == FieldType.None)
                    {
                        fieldType[curRow, curCol] = FieldType.Common;
                        count++;
                    }
                    break;

                case 1:
                    switch (direction)
                    {
                        case 1:
                            if (curRow2 < FieldSquareMatrixRow - 1)
                                curRow2++;
                            break;
                        case 2:
                            if (curRow2 > 0)
                                curRow2--;
                            break;
                        case 3:
                            if (curCol2 < FieldSquareMatrixRow - 1)
                                curCol2++;
                            break;
                        case 4:
                            if (curCol2 > 0)
                                curCol2--;
                            break;
                    }

                    if (fieldType[curRow2, curCol2] == FieldType.None)
                    {
                        fieldType[curRow2, curCol2] = FieldType.Common;
                        count++;
                    }
                    break;
            }
            
            loopCount++;

            if (loopCount >= 1000)
                break;
        }

        //모든 필드를 생성했으므로, 보스 필드 지정 전에
        //None 이 아닌 모든 필드들의 리스트와
        //None 이 아닌 모든 필드들의 연결을 초기화하여 저장해야 함.
        InitFieldsPointsVertex();
        InitFieldsPointsEdges();

        //하나의 필드를 보스필드로 변경 부분
        //시작 필드로부터 가장 먼 필드를 보스 필드로 변경하도록 함.
        FieldPoint fartheatField = FindLongestPath();
        fieldType[fartheatField.X, fartheatField.Y] = FieldType.Boss;

        //특수 및 일반 필드의 개수 계산 부분
        SpecialFieldCount = 0;
        //일정 확률로 일반필드를 특수필드로 변경 부분
        for (int x = 0; x < FieldSquareMatrixRow; x++)
        {
            for (int y = 0; y < FieldSquareMatrixRow; y++)
            {
                if (fieldType[x, y] == FieldType.Common)
                {
                    if (UnityEngine.Random.Range(0, 10) == 0) // 0~9 중 0 이므로 1/10 => 10%
                    {
                        fieldType[x, y] = FieldType.Special;
                        SpecialFieldCount++;    //변경된 만큼 SpecialFieldCount 추가
                    }
                }
            }
        }
        CommonFieldCount = FieldCount - SpecialFieldCount - 2; //일반 = 전체 - 특수 - 2(=보스+시작)
    }



    //시작 필드 부터 다른 특정 필드 간의 거리를 계산하는 메소드
    // 길 찾기 알고리즘 이용
    private int CalcPathCost(FieldPoint otherPoint)
    {
        FieldPoint startPoint = new(StartRow, StartCol);    //시작 필드

        if (otherPoint.Equals(startPoint))
        {   //("otherField가 startField가 같다면 비용은 0")
            return 0;
        }

        Queue<FieldPoint> frontier = new Queue<FieldPoint>();   //방문할 필드 큐
        List<FieldPoint> visited = new List<FieldPoint>();      //방문한 필드 리스트
        Dictionary<FieldPoint, FieldPoint> nextFields = new Dictionary<FieldPoint, FieldPoint>();    //다음 방문할 필드 Dict [K:지금 필드, V:다음 필드];

        frontier.Enqueue(otherPoint);   //처음에 도착할 필드를 큐에 넣음

        while (frontier.Count > 0)
        {
            FieldPoint curField = frontier.Dequeue();   //제일 앞에 큐에서 꺼낸 필드로 현재 필드

            foreach (FieldPoint neighbor in GetListNeighborFieldPoints(curField))   //현재 필드의 주변(이웃) 필드 만큼
            {
                //처음에 Contains() 가 제대로 작동도 안하고, 확인해보니까 dictionary에 키도 중복으로 추가되어서, 중복 방지 및 isError로 처리하려니까 너무 복잡해지고 이상해서 찾아보니까
                // FieldPoint class 의 GetHashCode()를 overriding 해 주어야 했다. 이제 잘 됨.
                if (!visited.Contains(neighbor) && !frontier.Contains(neighbor))    //주변 필드가 방문한 필드 리스트에 없고, 방문할 필드 큐에도 들어 있지 않을 때,
                {
                    frontier.Enqueue(neighbor);         //이웃 필드를 큐에 넣고
                    nextFields.Add(neighbor, curField); //사전에도 적어줌. [이웃, 현재] => 이헐게 해야 시작 필드부터 도착 필드까지 차례대로 갈 수 있음.
                }

            }
            visited.Add(curField);  //모든 이웃 필드를 처리한 후 방문한 필드에 현재 필드 넣음.
        }

        if (visited.Contains(startPoint) == false)
        {
            Debug.Log("startPoint에 도달 불가"); //이 코드에서는 필드가 떨어질 수 없게 짜 놓았기 때문에 뜨면 안되지만, 혹시 몰라서 작성함.
            return -1;
        }

        //거리 최소 경로 비용 계산
        int cost = 0;   //비용
        FieldPoint curPathField = startPoint;   //현재 필드
        while (!(curPathField.X == otherPoint.X && curPathField.Y == otherPoint.Y)) //nextFieldDict를 통해 시작 필드부터 지정 경로까지 도착할 때까지 돌림
        {
            curPathField = nextFields[curPathField];    //사전 통해서 경로 따라 가면서
            cost++; // 거리 비용은 모두 1이므로 읽어가면서 ++ 해줌
        }

        return cost;
    }

    //주어진 필드의 이웃한 필드들을 리턴하는 메소드
    List<FieldPoint> GetListNeighborFieldPoints(FieldPoint fieldPoint)
    {
        List<FieldPoint> neighborList = new List<FieldPoint>();

        foreach (var tuple in FieldsPointsEdges)
        {
            if (tuple.Item1.X == fieldPoint.X && tuple.Item1.Y == fieldPoint.Y)
            {
                neighborList.Add(tuple.Item2);
            }
            else if (tuple.Item2.X == fieldPoint.X && tuple.Item2.Y == fieldPoint.Y)
            {
                neighborList.Add(tuple.Item1);
            }
        }

        return neighborList;
    }

    //시작 필드로부터 제일 먼 필드를 구하는 메소드 (같으면 제일 처음)
    private FieldPoint FindLongestPath()
    {
        int highestCost = 0;
        int highestIndex = 0;

        int index = 0;
        foreach (var node in FieldsPointsVertex)
        {
            int cost = CalcPathCost(node);
            if (cost > 0 && cost > highestCost)
            {
                highestCost = cost;
                highestIndex = index;
            }

            index++;
        }

        return FieldsPointsVertex.ElementAt(highestIndex);
    }




    //디버그
    void OnDrawGizmos()
    {
        if (mapGenerator != null)
        {
            for (int i = 0; i < mapGenerator.GetLength(0); i++)
            {
                for (int j = 0; j < mapGenerator.GetLength(1); j++)
                {
                    if (mapGenerator[i, j] != null)
                    {
                        for (int x = 0; x < mapGenerator[i, j].Fields.Map.GetLength(0); x++)
                        {
                            for (int y = 0; y < mapGenerator[i, j].Fields.Map.GetLength(1); y++)
                            {
                                switch (mapGenerator[i, j].Fields.Map[x, y])
                                {
                                    case 1:     //벽
                                        Gizmos.color = Color.white;
                                        break;
                                    case 2:     //발판 플랫폼
                                        Gizmos.color = Color.gray;
                                        break;
                                    case 3:     //사다리
                                        Gizmos.color = Color.yellow;
                                        break;
                                    case 0:     //빈공간
                                        Gizmos.color = Color.black;
                                        break;
                                    case 99:    //스테이지 시작 지점
                                        Gizmos.color = Color.green;
                                        break;
                                    case 80:    //필드 포탈
                                        Gizmos.color = new Color(1, 0.5f, 0);
                                        break;
                                    case 95:    //스테이지 포탈
                                        Gizmos.color = Color.red;
                                        break;
                                }

                                Vector3 pos = new Vector3(-(mapGenerator.GetLength(0) * (mapGenerator[i, j].Fields.Map.GetLength(0) + 1)) + (i * (mapGenerator[i, j].Fields.Map.GetLength(0) + 1)) + x, j * (mapGenerator[i, j].Fields.Map.GetLength(1) + 1) + y);
                                Gizmos.DrawCube(pos, Vector3.one);
                            }
                        }
                    }
                }
            }
        }
    }
}