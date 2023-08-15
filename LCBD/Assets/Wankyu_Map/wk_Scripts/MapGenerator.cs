using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class MapGenerator
{
    public string seed;
    public bool useRandomSeed;

    private FieldType fieldType;

    public Field Fields { get; set; }

    public void InitMap(int mapWidth, int mapHeight, FieldType fieldType)
    {
        Fields = new Field();

        Fields.FillMap(mapWidth, mapHeight, fieldType);
    }

    //public void GenerateMap(int row, int col)
    //{
    //    RandomFillMap();

    //    for (int x = 0; x < row; x++)
    //    {
    //        for (int y = 0; y < col; y++)
    //        {
    //            DrawMapTile(x, y);
    //        }
    //    }
    //}


    //public void RandomFillMap()
    //{
    //    if (useRandomSeed)
    //    {
    //        seed = DateTime.Now.ToString();
    //    }

    //    System.Random psuedoRandom = new System.Random(seed.GetHashCode());

    //    for (int i = 0; i < fieldSquareMatrixRow; i++)
    //    {
    //        for (int j = 0; j < fieldSquareMatrixRow; j++)
    //        {
    //            for (int x = 0; x < width; x++)
    //            {
    //                for (int y = 0; y < height; y++)
    //                {
    //                    Fields[i, j].Map[x, y] = (psuedoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
    //                }//= (int)Mathf.PerlinNoise(x, y);
    //            }
    //        }
    //    }
    //}



    //public void DrawMapTile(int i, int j)
    //{
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            if (Fields[i,j].Map[x, y] == 1)
    //            {
    //                tilemap.SetTile(new Vector3Int(i * width + x, j * height + y, 0), tileBaseBrick);
    //            }
    //            else
    //            {
    //                tilemap.SetTile(new Vector3Int(i * width + x, j * height + y, 0), null);
    //            }
    //        }
    //    }
    //}

}