using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField, Range(1, 5)]
    private int _stageLevel;
    public int StageLevel { get { return _stageLevel; } private set { _stageLevel = value; } }

    public int FieldCount { get; private set; }

    private int _fieldSquareMatrixRow;
    public int FieldSquareMatrixRow
    {
        get { return _fieldSquareMatrixRow; }
        private set { if (6 <= value && value <= 8) _fieldSquareMatrixRow = value; }
    }

    [SerializeField, Range(20, 100)]
    private int _mapWidth, _mapHeight;
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

    FieldType[,] fieldType;

    MapGenerator[,] mapGenerator;
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

    public void DrawFields()
    {
        Debug.Log("DrawFields()");
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

    public void GenerateFields()
    {
        InitFields();
        Debug.Log("GenerateFields()");

        int startRow = UnityEngine.Random.Range(0, fieldType.GetLength(0));
        int startCol = UnityEngine.Random.Range(0, fieldType.GetLength(1));

        Debug.Log("startRow, startCol : " + startRow + ", " + startCol);

        int row = startRow;
        int col = startCol;

        //시작 필드
        fieldType[startRow, startCol] = FieldType.Start;

        int count = 1;  //생성된 필드의 수

        //일반 필드 생성
        int loopCount = 0;  //while문 loop Count
        while (count < FieldCount)
        {
            int direction = UnityEngine.Random.Range(1, 5); // 1. ↑ / 2. ↓ / 3. → / 4. ←
            switch (direction)
            {
                case 1:
                    if (row < FieldSquareMatrixRow - 1)
                        row++;
                    break;
                case 2:
                    if (row > 0)
                        row--;
                    break;
                case 3:
                    if (col < FieldSquareMatrixRow - 1)
                        col++;
                    break;
                case 4:
                    if (col > 0)
                        col--;
                    break;
            }
            if (fieldType[row, col] == FieldType.None)
            {
                Debug.Log("generated/ row, col : " + row + ", " + col);
                fieldType[row, col] = FieldType.Common;
                count++;
            }

            loopCount++;

            Debug.Log("row, col : " + row + ", " + col);

            if (loopCount >= 1000)
                break;
        }

        //하나의 필드를 보스필드로 변경
        //시작 필드로부터 가장 먼 필드를 보스 필드로 변경
        FieldPoint fartheatField = FindLongestPath();
        fieldType[fartheatField.X, fartheatField.Y] = FieldType.Boss;

        //일정 확률로 일반필드를 특수필드로 변경
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


    //None 필드를 제외한 필드의 좌표를 리스트로 리턴하는 메소드
    private List<FieldPoint> GetFieldsPoints()
    {
        List<FieldPoint> fieldsVertexs = new List<FieldPoint>();
        for (int x = 0; x < FieldSquareMatrixRow; x++)
        {
            for (int y = 0; y < FieldSquareMatrixRow; y++)
            {
                if (fieldType[x, y] != FieldType.None)
                {
                    fieldsVertexs.Add(new FieldPoint(x, y));
                }
            }
        }

        return fieldsVertexs;
    }

    //None 필드를 제외한 각 필드 간의 연결되는 두 필드를 쌍으로 리스트에 담고 리턴하는 메소드
    private List<Tuple<FieldPoint, FieldPoint>> ConnectFields()
    {
        return GetFieldsPointsEdges();
    }

    private List<Tuple<FieldPoint, FieldPoint>> GetFieldsPointsEdges()
    {
        List<Tuple<FieldPoint, FieldPoint>> linkedFields = new List<Tuple<FieldPoint, FieldPoint>>();

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
                    linkedFields.Add(linkedField);
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
                    linkedFields.Add(linkedField);
                }
            }
        }

        return linkedFields;
    }

    //시작 필드 부터 다른 특정 필드 간의 거리를 계산하는 메소드
    // TODO 길찰기 알고리즘 필요
    private int CalcPathCost(FieldPoint otherPoint)
    {
        int cost = 0;
        FieldPoint startPoint = new FieldPoint();

        for (int x = 0; x < FieldSquareMatrixRow; x++)
        {
            for (int y = 0; y < FieldSquareMatrixRow; y++)
            {
                if (fieldType[x, y] == FieldType.Start)
                {
                    startPoint.SetX(x);
                    startPoint.SetY(y);
                }
            }
        }

        List<FieldPoint> fieldsVertexs = new List<FieldPoint>();
        foreach (var item in GetFieldsPoints())
        {
            fieldsVertexs.Add(item);
        }

        List<Tuple<FieldPoint, FieldPoint>> fieldsEdges = new List<Tuple<FieldPoint, FieldPoint>>();
        foreach (var item in GetFieldsPointsEdges())
        {
            fieldsEdges.Add(item);
        }

        return cost;
    }



    //시작 필드로부터 제일 먼 필드를 구하는 메소드
    private FieldPoint FindLongestPath()
    {
        int highestCost = 0;
        int highestIndex = 0;

        int index = 0;
        foreach (var node in GetFieldsPoints())
        {
            int cost = CalcPathCost(node);
            if (cost > highestCost)
            {
                highestCost = cost;
                highestIndex = index;
            }

            index++;
        }

        return GetFieldsPoints().ElementAt(highestIndex);
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