using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class MapGenerator
{
    public string seed;
    public bool useRandomSeed;

    private FieldType fieldType;

    public Field Fields { get; set; }

    public void InitMap(int mapWidth, int mapHeight, FieldType fieldType, int stageLevel)
    {
        Fields = new Field();
        Fields.SetLevel(stageLevel);
        Fields.FillMap(mapWidth, mapHeight, fieldType);
    }
}