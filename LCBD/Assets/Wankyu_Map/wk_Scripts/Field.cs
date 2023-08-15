using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FieldType
{
    None, Start, Boss, Common, Special
}

public class Field
{
    public bool IsClear { get; private set; }
    public void SetIsClear(bool isClear) { IsClear = isClear; }

    public FieldType FIeldType { get; private set; }    //각 필드의 필드 타입

    public int[,] Map { get; private set; }         //각 필드의 타일 배치
    // 0. 빈 공간, 1. 벽 블록, 2. 플랫폼 블록, 3. 사다리
    // 99.스테이지 시작 지점(시작 필드) 95. 스테이지 포탈 위치(보스 필드) 80. 필드 포탈 위치 40. 보상 위치


    public Dictionary<int[,], int[,]> portal;       //각 필드의 포탈 정보 //TODO 아직 안씀.


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
        Map = DefaultMap.GetCommonField(width, height);
        //FillRandomMap(width, height);
    }

    #region // 원래 일반 필드 그릴 때 쓰려고 만든 완전 랜덤으로 맵 그리는 메소드들인데 지금 안쓰임
    #region // 맵에 벽을 생성할 때 랜덤으로 벽을 만들고, 테두리 그리고, 스무딩해서 맵을 만들어주는 메소드
    //private void FillRandomMap(int width, int height)
    //{
    //    //string seed = DateTime.Now.Ticks.ToString();
    //    //System.Random psuedoRandom = new System.Random(seed.GetHashCode());
    //    //Debug.Log("seed: " + seed);

    //    //float max = DateTime.Now.Ticks;
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            if (x == width/2 || y == height/2)
    //            {
    //                Map[x,y] = 0;
    //            }
    //            else
    //            {
    //                Map[x, y] = UnityEngine.Random.Range(0, 100) < 42 ? 1 : 0;  //42%확률 로 벽 만듬
    //            }
    //        }
    //    }

    //    DrawBorder(width, height);

    //    int smoothLevel;
    //    smoothLevel = width > height ? width >> 2 : height >> 2;
    //    smoothLevel = smoothLevel <= 1 ? 1 : smoothLevel;

    //    for (int i = 0; i < smoothLevel; i++)
    //    {
    //        SmoothMap(width, height);
    //    }
    //}
    #endregion

    #region // 스무딩해주는 메소드 / GetSurroundingWallCount()를 통해 주변 벽의 개수에 맞게 자신의 상태를 바꾸는 메소드
    //private void SmoothMap(int width, int height)
    //{
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            int neighbourWallTiles = GetSurroundingWallCount(width, height, x, y);

    //            if (neighbourWallTiles > 4)
    //            {
    //                Map[x, y] = 1;
    //            }
    //            else if (neighbourWallTiles < 4)
    //            {
    //                Map[x, y] = 0;
    //            }
    //        }
    //    }
    //}
    #endregion

    #region // 주변의 벽의 개수 세는 메소드
    //private int GetSurroundingWallCount(int width, int height, int gridX, int gridY)
    //{
    //    int wallCount = 0;

    //    for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
    //    {
    //        for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
    //        {
    //            if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
    //            {
    //                if (neighbourX != gridX || neighbourY != gridY)
    //                {
    //                    wallCount += Map[neighbourX, neighbourY];
    //                }
    //            }
    //            else
    //            {
    //                wallCount++;
    //            }
    //        }
    //    }

    //    return wallCount;
    //}
    #endregion
    #endregion

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

        //벽
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                startMap[x, y] = 1;
            }
        }

        int roomPointX = width / 4;
        int roomPointY = height / 4;

        int roomWidth = width / 2;
        int roomHeight = height / 2;

        //빈 공간
        for (int x = 0; x < roomWidth; x++)
        {
            for (int y = 0; y < roomHeight; y++)
            {
                startMap[roomPointX + x, roomPointY + y] = 0;
            }
        }

        //발판
        for (int x = 0; x < roomWidth / 2; x++)
        {
            startMap[roomPointX + roomWidth / 4 + x, roomPointY + roomHeight - 3] = 2;
        }
        for (int x = 0; x < roomWidth; x++)
        {
            startMap[roomPointX + x, roomPointY + roomHeight / 2] = 2;
        }

        //사다리
        for (int y = roomPointY + roomHeight / 2 + 1; y < roomPointY + roomHeight - 2; y++)
        {
            startMap[width / 2 + 2, y] = 3;
        }
        for (int y = roomPointY; y <= roomPointY + roomHeight / 2; y++)
        {
            startMap[width / 2 - 2, y] = 3;
        }

        startMap[roomPointX + roomWidth / 2, roomPointY + roomHeight / 2 + 1] = 99; //스테이지 시작 포인트(99)

        return startMap;
    }

    //BossField
    public static int[,] GetBossField(int width, int height)
    {
        int[,] bossMap = new int[width, height];

        //벽
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < height; y++)
            {
                bossMap[x, y] = 1;
                bossMap[width - 1 - x, y] = 1;
            }
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                bossMap[x, y] = 1;
                bossMap[x, height - 1 - y] = 1;
            }
        }

        //발판
        for (int y = 8; y < height - 8; y += 8)
        {
            bossMap[5 + 8, y] = 2;
            bossMap[5 + 7, y] = 2;
            bossMap[5 + 6, y] = 2;
            bossMap[5 + 5, y] = 2;
            bossMap[5 + 4, y] = 2;
            bossMap[5 + 3, y] = 2;
            bossMap[5 + 2, y] = 2;

            bossMap[width - 1 - 8 - 5, y] = 2;
            bossMap[width - 1 - 7 - 5, y] = 2;
            bossMap[width - 1 - 6 - 5, y] = 2;
            bossMap[width - 1 - 5 - 5, y] = 2;
            bossMap[width - 1 - 4 - 5, y] = 2;
            bossMap[width - 1 - 3 - 5, y] = 2;
            bossMap[width - 1 - 2 - 5, y] = 2;

            if (y + 4 <= height - 8)
            {
                bossMap[5 + 11, y + 4] = 2;
                bossMap[5 + 10, y + 4] = 2;
                bossMap[5 + 9, y + 4] = 2;
                bossMap[5 + 8, y + 4] = 2;
                bossMap[5 + 7, y + 4] = 2;
                bossMap[5 + 6, y + 4] = 2;
                bossMap[5 + 5, y + 4] = 2;

                bossMap[width - 1 - 11 - 5, y + 4] = 2;
                bossMap[width - 1 - 10 - 5, y + 4] = 2;
                bossMap[width - 1 - 9 - 5, y + 4] = 2;
                bossMap[width - 1 - 8 - 5, y + 4] = 2;
                bossMap[width - 1 - 7 - 5, y + 4] = 2;
                bossMap[width - 1 - 6 - 5, y + 4] = 2;
                bossMap[width - 1 - 5 - 5, y + 4] = 2;
            }
        }
        if ((width - 1 - 11 - 5 - 2) - (5 + 11 + 3) > 4)
        {
            for (int x = 5 + 11 + 3; x < width - 1 - 11 - 5 - 2; x++)
            {
                for (int y = 8 + 4; y < height - 8; y += 8)
                {
                    if (y + 4 <= height - 8)
                        bossMap[x, y + 4] = 2;
                }
            }
        }
        //

        //사다리
        for (int y = 8; y < height - 8; y += 8)
        {
            bossMap[5 + 6, y - 0] = 3;
            bossMap[5 + 6, y - 1] = 3;
            bossMap[5 + 6, y - 2] = 3;
            bossMap[5 + 6, y - 3] = 3;

            bossMap[width - 1 - 6 - 5, y - 0] = 3;
            bossMap[width - 1 - 6 - 5, y - 1] = 3;
            bossMap[width - 1 - 6 - 5, y - 2] = 3;
            bossMap[width - 1 - 6 - 5, y - 3] = 3;

            if (y + 4 <= height - 8)
            {
                bossMap[6 + 6, y + 4 - 0] = 3;
                bossMap[6 + 6, y + 4 - 1] = 3;
                bossMap[6 + 6, y + 4 - 2] = 3;
                bossMap[6 + 6, y + 4 - 3] = 3;

                bossMap[width - 1 - 6 - 6, y + 4 - 0] = 3;
                bossMap[width - 1 - 6 - 6, y + 4 - 1] = 3;
                bossMap[width - 1 - 6 - 6, y + 4 - 2] = 3;
                bossMap[width - 1 - 6 - 6, y + 4 - 3] = 3;
            }
        }

        if ((width - 1 - 11 - 5 - 2) - (5 + 11 + 3) > 4) //스테이지 포탈 위치(95)
        {
            bossMap[width / 2 - 2, 16 + 1] = 95;
        }
        else
        {
            if (bossMap[width / 2 - 2, 5] == 0)
            {
                bossMap[width / 2 - 2, 5] = 95;
            }
        }

        return bossMap;
    }

    //SpecialField
    public static int[,] GetSpecialField(int width, int height)
    {
        int[,] specialMap = new int[width, height];

        //벽
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < height; y++)
            {
                specialMap[x, y] = 1;
                specialMap[width - 1 - x, y] = 1;
            }
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                specialMap[x, y] = 1;
                specialMap[x, height - 1 - y] = 1;
            }
        }

        //발판
        for (int x = 10; x < width - 10; x++)
        {
            for (int y = 10; y < height - 5; y += 5)
            {
                specialMap[x, y] = 2;
            }
        }

        //사다리
        for (int y = 10; y < height - 5; y += 5)
        {
            if (y % 10 == 0)
            {
                specialMap[5 + 6, y - 0] = 3;
                specialMap[5 + 6, y - 1] = 3;
                specialMap[5 + 6, y - 2] = 3;
                specialMap[5 + 6, y - 3] = 3;
                specialMap[5 + 6, y - 4] = 3;

            }
            else
            {
                specialMap[width - 1 - 6 - 5, y - 0] = 3;
                specialMap[width - 1 - 6 - 5, y - 1] = 3;
                specialMap[width - 1 - 6 - 5, y - 2] = 3;
                specialMap[width - 1 - 6 - 5, y - 3] = 3;
                specialMap[width - 1 - 6 - 5, y - 4] = 3;
            }
        }



        return specialMap;
    }
    
    //CommonField
    public static int[,] GetCommonField(int width, int height)
    {
        int[,] commonlMap = new int[width, height];

        //벽
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                commonlMap[x, y] = 1;
            }
        }

        //int rand = UnityEngine.Random.Range(0, 3);
        //switch (rand)
        //{
        //    case 0:   //↗
        //        break;
        //    case 1:   //↖
        //        break;
        //    case 2:   //→
        //        break;
        //}

        int roomX = width / 2 + UnityEngine.Random.Range(-5, 5 + 1);
        int roomY = height / 2 + UnityEngine.Random.Range(-5, 5 + 1);

        //빈공간
        for (int x = 5; x < roomX; x++)
        {
            for (int y = 5; y < roomY; y++)
            {
                commonlMap[x, y] = 0;
            }
        }
        for (int x = roomX; x < width - 5; x++)
        {
            for (int y = roomY; y < height - 5; y++)
            {
                commonlMap[x, y] = 0;
            }
        }

        int midRoomX = roomX + UnityEngine.Random.Range(-roomX / 2, 0);
        int midRoomY = roomY + UnityEngine.Random.Range(-roomY / 2, 0);
        int midRoomWidth = UnityEngine.Random.Range(roomX - midRoomX + 2, 2 * (roomX - midRoomX));
        int midRoomHeight = UnityEngine.Random.Range(roomY - midRoomY + 2, 2 * (roomY - midRoomY));

        //빈공간 (가운데부분)
        for (int x = 0; x < midRoomWidth; x++)
        {
            for (int y = 0; y < midRoomHeight; y++)
            {
                commonlMap[midRoomX + x, midRoomY + y] = 0;
            }
        }

        //사다리
        for (int y = 5; y < midRoomY; y++)
        {
            commonlMap[roomX - 1, y] = 3;
        }
        for (int y = midRoomY; y < roomY; y++)
        {
            commonlMap[midRoomX + midRoomWidth - 1, y] = 3;
        }

        return commonlMap;
    }
}