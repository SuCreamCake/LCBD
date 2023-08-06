using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FieldType
{
    None, Start, Boss, Common, Special
}

[Serializable]
public class Field
{
    public bool IsClear { get; private set; }
    public void SetIsClear(bool isClear) { IsClear = isClear; }

    public FieldType FIeldType { get; private set; }

    public int[,] Map { get; private set; }


    private void InitMap(int width, int height)
    {
        IsClear = false;
        Map = new int[width, height];
    }

    public void FillMap(int width, int height, FieldType fIeldType)
    {
        InitMap(width, height);

        switch (fIeldType)
        {
            case FieldType.None: //필드 없음
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (x == y || width - x == y)
                        {
                            Map[x, y] = 1;
                        }
                        else
                        {
                            Map[x, y] = 0;
                        }
                    }
                }
                break;

            case FieldType.Start:
                //시작 필드
                FillStartMap(width, height);
                break;
            case FieldType.Boss:
                //보스 필드
                FillBossMap(width, height);
                break;
            case FieldType.Common:
                //일반 필드
                FillCommonMap(width, height);
                break;
            case FieldType.Special:
                //특수 필드
                FillSpecialMap(width, height);
                break;
        }

        DrawBorder(width, height);

    }

    private void FillCommonMap(int width, int height)
    {
        FillRandomMap(width, height);
    }

    private void FillRandomMap(int width, int height)
    {
        //string seed = DateTime.Now.Ticks.ToString();
        //System.Random psuedoRandom = new System.Random(seed.GetHashCode());
        //Debug.Log("seed: " + seed);

        float max = DateTime.Now.Ticks;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Map[x, y] = UnityEngine.Random.Range(0, max) < (max / 10 * 4) ? 1 : 0;
            }
        }
        
        DrawBorder(width, height);

        int smoothLevel;
        smoothLevel = width > height ? width >> 4 : height >> 4;
        smoothLevel = smoothLevel <= 1 ? 1 : smoothLevel;

        for (int i = 0; i < smoothLevel; i++)
        {
            SmoothMap(width, height);
        }
    }

    private void SmoothMap(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(width, height, x, y);

                if (neighbourWallTiles > 4)
                {
                    Map[x, y] = 1;
                }
                else if (neighbourWallTiles < 4)
                {
                    Map[x, y] = 0;
                }
            }
        }
    }

    private int GetSurroundingWallCount(int width, int height, int gridX, int gridY)
    {
        int wallCount = 0;

        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += Map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }

    private void FillStartMap(int width, int height)
    {
        Map = DefaultMap.GetStartField(width, height);
    }

    private void FillBossMap(int width, int height)
    {
        Map = DefaultMap.GetBossField(width, height);
    }

    private void FillSpecialMap(int width, int height)
    {
        Map = DefaultMap.GetSpecialField(width, height);
    }

    private void DrawBorder(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            Map[x, 0] = 1;
            Map[x, height - 1] = 1;
        }
        for (int y = 0; y < height; y++)
        {
            Map[0, y] = 1;
            Map[width - 1, y] = 1;
        }
    }
}

// 시작,보스,특수 같은 지정된 맵 가져오는 메소드 갖는 클래스 (모두 임시로 대충 작성해 봄)
class DefaultMap
{
    //StartField
    public static int[,] GetStartField(int width, int height)
    {
        int[,] startMap = new int[width, height];

        int floorHeight = height >> 1;

        int floorStart = width >> 2;
        int floorWidth = width >> 1;

        //발판
        for (int x = 0; x < floorWidth; x++)
        {
            startMap[floorStart + x, floorHeight] = 1;
        }

        return startMap;
    }

    //BossField
    public static int[,] GetBossField(int width, int height)
    {
        int[,] bossMap = new int[width, height];

        int firstFloor = height / 3;
        int secondFloor = height / 3 * 2;

        int floorWidth = width / 5;

        int firstFloorStart = width / 5;
        int firstFloorStart2 = width / 5 * 3;

        int secondFloorStart = width / 5 * 2;

        //발판
        for (int x = 0; x < floorWidth; x++)
        {
            bossMap[firstFloorStart + x, firstFloor] = 1;
            bossMap[firstFloorStart2 + x, firstFloor] = 1;
        }
        for (int x = secondFloorStart; x < (secondFloorStart + floorWidth); x++)
        {
            bossMap[x, secondFloor] = 1;
        }

        return bossMap;
    }

    //SpecialField
    public static int[,] GetSpecialField(int width, int height)
    {
        int[,] specialMap = new int[width, height];

        int floorWidth = width / 3;
        int floorHeight = height / 3;

        //발판
        for (int x = 0; x < floorWidth; x++)
        {
            specialMap[floorWidth + x, floorHeight] = 1;
        }
        for (int y = 0; y < floorHeight; y++)
        {
            specialMap[floorWidth, floorHeight + y] = 1;
        }

        return specialMap;
    }
    
    
    //
}