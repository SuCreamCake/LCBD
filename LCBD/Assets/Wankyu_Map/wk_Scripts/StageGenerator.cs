using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageGenerator : MonoBehaviour
{
    [SerializeField, Range(6, 10)]
    private int _fieldSquareMatrixRow;
    public int FieldSquareMatrixRow
    {
        get { return _fieldSquareMatrixRow; }
        private set { if (6 <= value && value <= 10) _fieldSquareMatrixRow = value; }
    }

    [SerializeField, Range(20, 100)]
    private int _mapWidth, _mapHeight;
    public int MapWidth { get { return _mapWidth; } private set { _mapWidth = value; } }
    public int MapHeight { get { return _mapHeight; } private set { _mapHeight = value; } }

    public void SetMapWidth(int mapWidth)
    {
        if(mapWidth > 20)
            MapWidth = mapWidth;
    }
    public void SetMapHeight(int mapHeight)
    {
        if(mapHeight > 20)
            MapHeight = mapHeight;
    }

    FieldType[,] fieldType;

    MapGenerator[,] mapGenerator;
    public MapGenerator[,] GetMapGenerator() { return mapGenerator; }
    


    private void Start()
    {
        DrawFields();
    }

    //임시 디버그
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus)) // '-' 키
        {
            DrawFields();
        }
    }

    public void DrawFields()
    {
        mapGenerator = new MapGenerator[FieldSquareMatrixRow, FieldSquareMatrixRow];
        fieldType = new FieldType[FieldSquareMatrixRow, FieldSquareMatrixRow];

        for (int x = 0; x < FieldSquareMatrixRow; x++)
        {
            for (int y = 0; y < FieldSquareMatrixRow; y++)
            {
                //임시 
                fieldType[x,y] = (FieldType)Enum.GetValues(typeof(FieldType)).GetValue(new System.Random().Next(0, Enum.GetValues(typeof(FieldType)).Length));

                mapGenerator[x, y] = new MapGenerator();
                mapGenerator[x, y].InitMap(MapWidth, MapHeight, fieldType[x, y]);
            }
        }
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