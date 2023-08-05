using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField, Range(1, 5)]
    private int _stageLevel;    //스테이지 레벨
    public int StageLevel { get { return _stageLevel; } private set { _stageLevel = value; } }

    public int FieldCount { get; private set; } //필드의 개수

    public int FieldSquareMatrixRow { get; private set; } //필드의 NxN 배열의 N(행 수)

    [SerializeField, Range(20, 100)]
    private int _mapWidth, _mapHeight;  //한 맵(필드)의 너비, 높이
    public int MapWidth { get { return _mapWidth; } private set { _mapWidth = value; } }
    public int MapHeight { get { return _mapHeight; } private set { _mapHeight = value; } }

    public void SetMapWidth(int mapWidth)
    {
        if (mapWidth > 20)
            MapWidth = mapWidth;
    }
    public void SetMapHeight(int mapHeight)
    {
        if (mapHeight > 20)
            MapHeight = mapHeight;
    }

    public int StartRow { get; private set; }   //시작필드의 행
    public int StartCol { get; private set; }   //시작필드의 열

    private List<Tuple<FieldPoint, FieldPoint>> fieldsPointsEdges;  //None 필드를 제외한 각 필드 간의 연결되는 두 필드(주변 필드)들을 쌍으로 저장하는 리스트

    //fieldsPointsEdges 를 초기화하고 연결되는 필드(주변 필드)들을 모두 저장하는 메소드
    private void InitFieldsPointsEdges()
    {
        fieldsPointsEdges = new List<Tuple<FieldPoint, FieldPoint>>();

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
                    fieldsPointsEdges.Add(linkedField);
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
                    fieldsPointsEdges.Add(linkedField);
                }
            }
        }
    }

    private List<FieldPoint> fieldsPointsVertex;    //None 필드를 제외한 필드들을 저장하는 리스트

    //fieldsPointsVertex를 초기화하고 None 필드를 제외한 필드의 좌표를 저장하는 메소드
    private void InitFieldsPointsVertex()
    {
        fieldsPointsVertex = new List<FieldPoint>();
        for (int x = 0; x < FieldSquareMatrixRow; x++)
        {
            for (int y = 0; y < FieldSquareMatrixRow; y++)
            {
                if (fieldType[x, y] != FieldType.None)
                {
                    fieldsPointsVertex.Add(new FieldPoint(x, y));
                }
            }
        }
    }


    FieldType[,] fieldType; //각 맵의 필드 타입

    MapGenerator[,] mapGenerator;   //각 맵(필드)의 생성기

    public MapGenerator[,] GetMapGenerator() { return mapGenerator; }

    private void Start()
    {
        FieldCount = 5 * StageLevel + UnityEngine.Random.Range(3, 6);
        FieldSquareMatrixRow = Mathf.FloorToInt(Mathf.Sqrt(FieldCount + 20)) + 1;

        Debug.Log("roomCount, stageLevel : " + FieldCount + ", " + StageLevel);
        Debug.Log("FieldSquareMatrixRow : " + FieldSquareMatrixRow);

        GenerateFields();
        DrawFields();
    }

    //임시 디버그
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus)) // '-' 키
        {
            FieldCount = 5 * StageLevel + UnityEngine.Random.Range(3, 6);
            FieldSquareMatrixRow = Mathf.FloorToInt(Mathf.Sqrt(FieldCount + 20)) + 1;

            Debug.Log("roomCount, stageLevel : " + FieldCount + ", " + StageLevel);
            Debug.Log("FieldSquareMatrixRow : " + FieldSquareMatrixRow);

            GenerateFields();
            DrawFields();
        }
    }

    //각 맵의 필드 타입에 맞게 맵 생성하는 메소드
    public void DrawFields()
    {
        mapGenerator = new MapGenerator[FieldSquareMatrixRow, FieldSquareMatrixRow];

        for (int x = 0; x < FieldSquareMatrixRow; x++)
        {
            for (int y = 0; y < FieldSquareMatrixRow; y++)
            {
                //임시 
                //fieldType[x,y] = (FieldType)Enum.GetValues(typeof(FieldType)).GetValue(new System.Random().Next(0, Enum.GetValues(typeof(FieldType)).Length));

                mapGenerator[x, y] = new MapGenerator();
                mapGenerator[x, y].InitMap(MapWidth, MapHeight, fieldType[x, y]);
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
    public void GenerateFields()
    {
        InitFields();

        StartRow = UnityEngine.Random.Range(0, fieldType.GetLength(0));
        StartCol = UnityEngine.Random.Range(0, fieldType.GetLength(1));

        int curRow = StartRow;
        int curCol = StartCol;

        //시작 필드 생성 부분
        fieldType[StartRow, StartCol] = FieldType.Start;

        int count = 1;  //생성된 필드의 수

        //일반 필드 생성 부분
        int loopCount = 0;  //while문 loop Count
        while (count < FieldCount)
        {
            int direction = UnityEngine.Random.Range(1, 5); // 1. ↑ / 2. ↓ / 3. → / 4. ←
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

        //일정 확률로 일반필드를 특수필드로 변경 부분
        for (int x = 0; x < FieldSquareMatrixRow; x++)
        {
            for (int y = 0; y < FieldSquareMatrixRow; y++)
            {
                if (fieldType[x, y] == FieldType.Common)
                {
                    if (UnityEngine.Random.Range(0, 5) == 0) // 0~4 중 0 이므로 1/5 => 20%
                        fieldType[x, y] = FieldType.Special;
                }
            }
        }

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

        foreach (var tuple in fieldsPointsEdges)
        {
            if (tuple.Item1.X ==  fieldPoint.X && tuple.Item1.Y == fieldPoint.Y)
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
        foreach (var node in fieldsPointsVertex)
        {
            int cost = CalcPathCost(node);
            if (cost > 0 && cost > highestCost)
            {
                highestCost = cost;
                highestIndex = index;
            }

            index++;
        }

        Debug.Log("시작과 제일 먼 필드는 (cost:" + highestCost + ") - " + fieldsPointsVertex.ElementAt(highestIndex).ToString());
        return fieldsPointsVertex.ElementAt(highestIndex);
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
                                Gizmos.color = (mapGenerator[i, j].Fields.Map[x, y] == 1) ? Color.white : Color.black;

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