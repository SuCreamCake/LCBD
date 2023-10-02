﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FieldType
{
    None, Start, Boss, Common, Special
}

/*일반 필드 유형 시리얼*/    //TODO 따로 나누는게 맞는 거 같음 너무 길어짐
public enum CommonFieldSerial_1     /*1스테이지*/
{
    Crying = 0,             /*울음소리*/
    Chromosome,             /*염색체*/
    Birth,                  /*출생*/
    //BabyBottle_g,         /*젖병(기믹)*/
    //Mom_g,                /*엄마(기믹)*/
    //StandingOnTwoFeet_g,  /*두발서기(기믹)*/
}

public enum CommonFieldSerial_2     /*2스테이지*/
{
    Tmp1 = 0,
    Tmp2,
}
public enum CommonFieldSerial_3     /*3스테이지*/
{
    TTmp1 = 0,
    TTmp2,
    TTmp3,
    TTmp4
}
public enum CommonFieldSerial_4     /*4스테이지*/
{
    TTTmp1 = 0,
    TTTmp2,
    TTTmp3
}
public enum CommonFieldSerial_5     /*5스테이지*/
{
    TTTTmp1 = 0,
    TTTTmp2,
    TTTTmp3,
    TTTTmp4,
    TTTTmp5
}

/*특수 필드 유형 시리얼*/
public enum SpecialFieldSerial
{
    AAA = 0,
    BBB,
    CCC
}


public class Field
{
    public int StageLevel { get; private set; }  //현재 스테이지 레벨
    public void SetLevel(int level) { StageLevel = level; }
    public bool IsVisited { get; private set; }     // 이 필드를 방문했는지 판별하는 boolean / 방문한 필드 = true
    public void SetIsVisited(bool isVisited) { IsVisited = isVisited; }
    public bool IsClear { get; private set; }       // 이 필드를 클리어했는지 판별하는 boolean / 클리어한 필드 = true
    public void SetIsClear(bool isClear) { IsClear = isClear; }

    public FieldType FieldFIeldType { get; private set; }    //각 필드의 필드 타입
    public void SetFieldType(FieldType fieldType) { FieldFIeldType = fieldType; }

    public int Serial { get; private set; }         //필드 유형
    public void SetSerial(int serial) { Serial = serial; }

    public int[,] Map { get; private set; }         //각 필드의 타일 배치
    // 0. 빈 공간, 1. 벽 블록, 2. 플랫폼 블록, 3. 사다리
    // 99.스테이지 시작 지점(시작 필드) 95. 스테이지 포탈 위치(보스 필드) 80. 필드 포탈 위치 40. 보상 위치


    public Dictionary<int[,], int[,]> portal;       //각 필드의 포탈 정보 //TODO 아직 안씀.


    private void InitMap(int width, int height)
    {
        IsClear = false;
        IsVisited = false;

        Map = new int[width, height];
    }

    public void FillMap(int width, int height, FieldType fIeldType)
    {
        SetFieldType(fIeldType);    //이 필드의 타입 지정

        InitMap(width, height);

        switch (fIeldType)
        {
            case FieldType.None: //필드 없음
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Map[x, y] = 1;
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

    // 일반 필드의 가중치있는 랜덤으로 유형을 하나를 선택하여 선택된 유형을 정수형으로 리턴 (또한 가중치도 조정)
    private int GetSelectedSerialByCommonFieldWeight()
    {
        //(만약 모든 가중치가 0이면 제일 처음 유형이 자동으로 선택된다.)

        // 디버그
        //Debug.Log("[선택 전 현재 일반 필드의 가중치]");
        //for (int i = 0; i < StageGenerator.CommonFieldWeights.Count; i++)
        //{
        //    Debug.Log("  ㄴ(" + (CommonFieldSerial)i + " : " + StageGenerator.CommonFieldWeights[(CommonFieldSerial)i] + ")");
        //}


        //CommonSerial_(1~5) 열거형에서 가중치 랜덤으로 하나를 선택
        //현재 일반 필드의 유형 가중치를 가져와서 초기화
        float[] weights = new float[StageGenerator.CommonFieldWeights.Count];
        Debug.Log("weights.length : " + weights.Length);
        Debug.Log("StageGenerator.CommonFieldWeights.Count : " + StageGenerator.CommonFieldWeights.Count);
        float maxWeight = 0;


        int index = 0;
        foreach (var item in StageGenerator.CommonFieldWeights)
        {
            Debug.Log("item : " + item);
            weights[index] = item.Value;
            maxWeight += item.Value;

            index++;
        }

        // 가중치 중에서 선택
        float selectedPoint = UnityEngine.Random.Range(0f, maxWeight);
        Debug.Log("maxWeight : " + maxWeight);
        Debug.Log("selectedPoint : " + selectedPoint);

        int selectedIndex = 0;
        //선택된 것의 인덱스로 찾기
        for (int i = 0; i < StageGenerator.CommonFieldWeights.Count; i++)
        {
            selectedIndex = i;
            selectedPoint -= weights[i];

            if (selectedPoint <= 0f)
                break;
        }
        Debug.Log("selectedIndex : " + selectedIndex);
        SetSerial(selectedIndex);   // 이 필드의 시리얼을 선택된 유형으로 초기화해줌.

        int maximumCount;   //유형 최대 생성 가능 개수 => 생성된(생성할) 일반 필드의 개수 / 해당 스테이지의 일반 유형의 개수
        switch (StageLevel)
        {
            case 1:
                maximumCount = StageGenerator.CommonFieldCount / Enum.GetNames(typeof(CommonFieldSerial_1)).Length - 1;   //유형 최대 생성 가능 개수 (0 부터 이므로 -1 해줌)
                break;
            case 2:
                maximumCount = StageGenerator.CommonFieldCount / Enum.GetNames(typeof(CommonFieldSerial_2)).Length - 1;   //유형 최대 생성 가능 개수 (0 부터 이므로 -1 해줌)
                break;
            case 3:
                maximumCount = StageGenerator.CommonFieldCount / Enum.GetNames(typeof(CommonFieldSerial_3)).Length - 1;   //유형 최대 생성 가능 개수 (0 부터 이므로 -1 해줌)
                break;
            case 4:
                maximumCount = StageGenerator.CommonFieldCount / Enum.GetNames(typeof(CommonFieldSerial_4)).Length - 1;   //유형 최대 생성 가능 개수 (0 부터 이므로 -1 해줌)
                break;
            case 5:
                maximumCount = StageGenerator.CommonFieldCount / Enum.GetNames(typeof(CommonFieldSerial_5)).Length - 1;   //유형 최대 생성 가능 개수 (0 부터 이므로 -1 해줌)
                break;
            default:
                maximumCount = 0;
                break;
        }

        //Debug.Log("maximumCount : " + maximumCount);
        ////현재 유형 생성 개수
        //for (int i = 0; i < StageGenerator.CommonFieldGeneratedCount.Count; i++)
        //{
        //    Debug.Log("  ㄴ(" + (CommonFieldSerial)i + " : " + StageGenerator.CommonFieldGeneratedCount[(CommonFieldSerial)i] + ")");
        //}

        // 가중치 조정 (선택된 유형의 가중치 = x0.5 / 선택되지 않은 유형의 가중치 = x1.5)
        for (int i = 0; i < StageGenerator.CommonFieldWeights.Count; i++)
        {
            if (i == selectedIndex)
            {
                StageGenerator.ChangeWeightInCommonFieldWeights(i, weights[i] * 0.5f);
            }
            else
            {
                StageGenerator.ChangeWeightInCommonFieldWeights(i, weights[i] * 1.5f);
            }
        }
        //현재 선택된 유형의 필드 생성 개수가 최대 생성 가능 개수 이상이면 가중치를 0으로 조정
        if (StageGenerator.CommonFieldGeneratedCount[selectedIndex] >= maximumCount)
        {
            StageGenerator.ChangeWeightInCommonFieldWeights(selectedIndex, 0);
        }

        //// 디버그
        //Debug.Log("[가중치 조정 후 현재 일반 필드의 가중치]");
        //for (int i = 0; i < StageGenerator.CommonFieldWeights.Count; i++)
        //{
        //    Debug.Log("  ㄴ(" + (CommonFieldSerial)i + " : " + StageGenerator.CommonFieldWeights[(CommonFieldSerial)i] + ")");
        //}

        return selectedIndex;
    }

    private void FillCommonMap(int width, int height)
    {
        // selectedIndex = 선택된 시리얼
        int selectedIndex = GetSelectedSerialByCommonFieldWeight(); // 가중치 에 따라 일반 필드 유형 선택

        // 일반 필드 생성 개수 dict 의 선택된 시리얼의 유형의 생성 개수 +1
        StageGenerator.CommonFieldGeneratedCount[selectedIndex]++;

        // 정해진 시리얼에 맞는 유형으로 맵을 가져와서 초기화해준다.
        Map = DefaultMap.GetCommonField(width, height, selectedIndex, StageLevel);

        // 지금 안쓰임
        // FillRandomMap(width, height);
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
        for (int y = roomPointY + roomHeight / 2 + 1 + 1; y < roomPointY + roomHeight - 2; y++)
        {
            startMap[width / 2 + 2, y] = 3;
        }
        for (int y = roomPointY + 1; y <= roomPointY + roomHeight / 2; y++)
        {
            startMap[width / 2 - 2, y] = 3;
        }

        startMap[roomPointX + roomWidth / 2, roomPointY + roomHeight / 2 + 1] = 99; //스테이지 시작 포인트(99)
        startMap[roomPointX + roomWidth / 2 + 4, roomPointY + roomHeight / 2 + 1] = 95; //스테이지 potal(95) (빠른 디버그를 위한 임시 배치)



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
            //bossMap[5 + 6, y - 3] = 3;

            bossMap[width - 1 - 6 - 5, y - 0] = 3;
            bossMap[width - 1 - 6 - 5, y - 1] = 3;
            bossMap[width - 1 - 6 - 5, y - 2] = 3;
            //bossMap[width - 1 - 6 - 5, y - 3] = 3;

            if (y + 4 <= height - 8)
            {
                bossMap[6 + 6, y + 4 - 0] = 3;
                bossMap[6 + 6, y + 4 - 1] = 3;
                bossMap[6 + 6, y + 4 - 2] = 3;
                //bossMap[6 + 6, y + 4 - 3] = 3;

                bossMap[width - 1 - 6 - 6, y + 4 - 0] = 3;
                bossMap[width - 1 - 6 - 6, y + 4 - 1] = 3;
                bossMap[width - 1 - 6 - 6, y + 4 - 2] = 3;
                //bossMap[width - 1 - 6 - 6, y + 4 - 3] = 3;
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
                //specialMap[5 + 6, y - 4] = 3;

            }
            else
            {
                specialMap[width - 1 - 6 - 5, y - 0] = 3;
                specialMap[width - 1 - 6 - 5, y - 1] = 3;
                specialMap[width - 1 - 6 - 5, y - 2] = 3;
                specialMap[width - 1 - 6 - 5, y - 3] = 3;
                //specialMap[width - 1 - 6 - 5, y - 4] = 3;
            }
        }



        return specialMap;
    }

    //CommonField
    public static int[,] GetCommonField(int width, int height, int commonSerial, int stageLevel)
    {
        int[,] commonlMap = new int[width, height];

        switch (stageLevel)
        {
            case 1:
                commonlMap = GetCommonField_Stage1(width, height, commonSerial);
                break;
            case 2:
                commonlMap = GetCommonField_Stage1(width, height, commonSerial);    // 임시로 1스테이지 맵 가져옴
                break;
            case 3:
                commonlMap = GetCommonField_Stage1(width, height, commonSerial);    // 임시로 1스테이지 맵 가져옴
                break;
            case 4:
                commonlMap = GetCommonField_Stage1(width, height, commonSerial);    // 임시로 1스테이지 맵 가져옴
                break;
            case 5:
                commonlMap = GetCommonField_Stage1(width, height, commonSerial);    // 임시로 1스테이지 맵 가져옴
                break;
        }

        return commonlMap;
    }

    // CommonField _ Stage1
    private static int[,] GetCommonField_Stage1(int width, int height, int commonSerial)
    {
        int[,] commonlMap_Stage1 = new int[width, height];

        switch ((CommonFieldSerial_1)commonSerial)
        {
            case CommonFieldSerial_1.Crying:   //울음소리
                int[,] commonMap_Crying =   //이게 맞나... ?
                    { {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 80, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 2, 50, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 0, 0, 0, 0, 2, 40, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 2, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 2, 0, 0, 0, 0, 0, 2, 49, 0, 2, 0, 0, 2, 111, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 3, 3, 3, 0, 0, 2, 0, 0, 2, 0, 0, 2, 0, 0, 2, 49, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 2, 0, 0, 2, 0, 0, 2, 0, 0, 2, 111, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 2, 0, 0, 2, 0, 3, 3, 0, 0, 2, 0, 2, 0, 0, 0, 2, 111, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 2, 40, 0, 0, 2, 49, 0, 0, 0, 0, 2, 0, 0, 0, 0, 2, 0, 0, 0, 2, 111, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 2, 0, 0, 0, 2, 111, 111, 0, 0, 1, 1, 0, 3, 3, 3, 3, 0, 0, 0, 2, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 3, 3, 3, 0, 0, 0, 2, 111, 0, 0, 0, 2, 49, 0, 0, 2, 111, 111, 0, 0, 1, 1, 0, 0, 0, 0, 2, 0, 0, 0, 2, 49, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 0, 0, 2, 0, 0, 0, 2, 49, 0, 0, 0, 2, 0, 0, 0, 2, 111, 111, 0, 0, 1, 1, -1, -1, -1, 1, 1, 0, 3, 3, 3, 0, 0, 0, 0, 0, 2, 0, 0, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 80, 0, 2, 50, 0, 0, 2, 111, 0, 0, 0, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1, -1, -1, 1, 1, 0, 0, 0, 2, 0, 0, 0, 0, 0, 2, 0, 0, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, -52, -52, -52, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 1, 1, 1, -41, -1, -1, -1, -1, -3, -1, -1, -1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 80, 0, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 0, 0, 2, 0, 3, 3, 3, 3, 3, 0, 0, 2, 0, 1, 1, 1, -4, -4, -4, -1, -1, -3, -1, -1, -1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 3, 3, 0, 1, 1, 1, -1, -1, -3, -1, -1, -1, -1, -1, -1, 1, 1, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 40, 0, 0, 0, 0, 0, 0, 0, 1, 1, -50, -1, -3, -1, -4, -4, -4, -1, -1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 111, 111, 2, 50, 0, 0, 0, 0, 1, 1, -1, -1, -1, -3, -3, -3, -3, -1, -1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 111, 111, 2, 49, 0, 0, 0, 0, 1, 1, -1, -3, -1, -3, -41, -1, -3, -1, -1, 1, 1, 201, 201, 201, 201, 201, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 111, 111, 2, 0, 0, 0, 0, 0, 1, 1, -1, -3, -112, -3, -1, -1, -3, -50, -1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 2, 0, 0, 1, 1, -52, -3, -112, -3, -202, -3, -3, -1, -1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 2, 0, 0, 0, 2, 0, -1, -1, -1, -1, -3, -1, -3, -112, -3, -112, -1, -1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 2, 49, 0, 0, 2, 0, -4, -4, -4, -4, -4, -1, -3, -112, -4, -112, -1, -1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 2, 0, 0, 0, 2, 0, -1, -1, -1, -1, -1, -1, -3, -112, -112, -112, -1, -1, 1, 1, 81, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 50, 0, 0, 0, 0, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 3, 3, 3, 3, 3, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 2, 81, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 49, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 50, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 80, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1} };

                if (UnityEngine.Random.Range(0, 100) < 50) // 50 대신 플레이어의 행운도 -> 행운도가 높으면 추가 기믹맵 생성O // TODO
                {
                    for (int x = 0; x < commonMap_Crying.GetLength(0); x++)
                    {
                        for (int y = 0; y < commonMap_Crying.GetLength(1); y++)
                        {
                            if (commonMap_Crying[x, y] < 0)
                                commonMap_Crying[x, y] = Math.Abs(commonMap_Crying[x, y] + 1);
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < commonMap_Crying.GetLength(0); x++)
                    {
                        for (int y = 0; y < commonMap_Crying.GetLength(1); y++)
                        {
                            if (commonMap_Crying[x, y] < 0)
                                commonMap_Crying[x, y] = 1;
                        }
                    }
                }

                commonlMap_Stage1 = commonMap_Crying;

                break;
            case CommonFieldSerial_1.Chromosome:   //염색체
                int[,] commonMap_Chromosome =
                    { {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1, -1, -1, -1, -1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -41, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -112, -1, -1, -1, -1, -1, -1, -1, -1, -51, -1, -3, -1, -1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -112, -1, -1, -1, -1, -1, -1, -1, -3, -1, -4, -4, -1, -1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -112, -1, -1, -1, -1, -3, -1, -4, -4, -1, -1, -3, -82, -1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1, -1, -3, -1, -1, -1, -51, -1, -1, -3, -1, -1, -3, -50, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1, -4, -4, -1, -1, -1, -1, -1, -1, -3, -1, -1, -3, -1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 80, 0, 0, 2, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, -1, -1, -3, -1, -4, -4, -1, -1, -1, -3, -1, -1, -1, -1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 2, 40, 0, 0, 1, 1, 1, 1, 1, 1, 1, -50, -1, -3, -1, -1, -3, -1, -4, -4, -4, -1, -1, -51, -1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 2, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, -1, -1, -1, -1, -1, -3, -50, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 2, 0, 0, 1, 1, 1, 0, 0, 0, 2, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, -1, -1, -1, -1, -1, -3, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 49, 0, 0, 0, 2, 0, 3, 3, 3, 3, 0, 0, 0, 2, 0, 3, 3, 3, 3, 3, 3, 3, 0, 0, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 50, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 2, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 49, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 49, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 3, 3, 3, 3, 3, 0, 0, 0, 0, 2, 0, 3, 3, 3, 0, 3, 3, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 2, 0, 3, 3, 3, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 49, 0, 1, 1, 1, 1, 40, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 2, 40, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 0, 2, 0, 3, 3, 3, 0, 0, 0, 0, 2, 50, 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 0, 0, 2, 0, 0, 2, 0, 0, 0, 2, 0, 3, 3, 3, 3, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 0, 0, 2, 0, 3, 3, 0, 0, 0, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 40, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 1},
                    {1, 1, 80, 0, 2, 0, 0, 2, 0, 0, 0, 2, 49, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 40, 0, 0, 0, 0, 1},
                    {1, 1, 0, 0, 2, 0, 0, 2, 40, 0, 0, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 2, 0, 0, 0, 0, 0, 1},
                    {1, 1, 0, 0, 2, 0, 0, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 2, 0, 0, 0, 0, 0, 1},
                    {1, 1, 0, 3, 3, 0, 0, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 80, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 2, 0, 3, 3, 0, 0, 1},
                    {1, 1, 0, 0, 0, 0, 0, 2, 81, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 2, 0, 0, 2, 0, 0, 1},
                    {1, 1, 0, 0, 0, 0, 0, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 0, 0, 2, 0, 0, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 2, 0, 0, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 49, 0, 0, 0, 2, 80, 0, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 2, 0, 0, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1} };

                if (UnityEngine.Random.Range(0, 100) < 50) // 50 대신 플레이어의 행운도 -> 행운도가 높으면 추가 기믹맵 생성O // TODO
                {
                    for (int x = 0; x < commonMap_Chromosome.GetLength(0); x++)
                    {
                        for (int y = 0; y < commonMap_Chromosome.GetLength(1); y++)
                        {
                            if (commonMap_Chromosome[x, y] < 0)
                                commonMap_Chromosome[x, y] = Math.Abs(commonMap_Chromosome[x, y] + 1);
                        }
                    }
                    Debug.Log("commonMap_Chromosome's 추가 기믹맵 생성O");
                }
                else
                {
                    for (int x = 0; x < commonMap_Chromosome.GetLength(0); x++)
                    {
                        for (int y = 0; y < commonMap_Chromosome.GetLength(1); y++)
                        {
                            if (commonMap_Chromosome[x, y] < 0)
                                commonMap_Chromosome[x, y] = 1;
                        }
                    }
                    Debug.Log("commonMap_Chromosome's 추가 기믹맵 생성X");
                }

                commonlMap_Stage1 = commonMap_Chromosome;

                break;
            case CommonFieldSerial_1.Birth:    //출생
                int[,] commonMap_Birth =
                    { {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 80, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 50, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 2, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 3, 3, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 2, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 0, 0, 2, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 40, 0, 0, 0, 0, 0, 2, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 2, 0, 0, 0, 2, 40, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 2, 49, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 2, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3, 0, 0, 0, 2, 201, 201, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 0, 0, 0, 0, 0, 2, 0, 0, 2, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 2, 0, 0, 2, 49, 0, 2, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 50, 0, 0, 0, 0, 0, 2, 49, 0, 2, 0, 0, 2, 40, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 2, 0, 3, 3, 0, 0, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 2, 0, 0, 2, 201, 201, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 50, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 2, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 201, 201, 201, 2, 49, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 201, 201, 201, 2, 0, 0, 1, 1, 1, 1, 1, 0, 0, 2, 201, 201, 201, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 2, 0, 0, 1, 1, 1, 0, 2, 0, 0, 2, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 2, 0, 3, 0, 0, 2, 0, 0, 0, 2, 0, 0, 2, 80, 0, -1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 3, 0, 0, 0, 0, 2, 49, 0, 0, 2, 0, 3, 3, 0, 0, -1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 2, 0, 0, 0, 0, 2, 0, 2, 0, 3, 0, 0, 2, 0, -4, -4, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 2, 0, 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 2, 0, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 0, 0, 0, 0, 0, 0, 2, 0, 3, 0, 0, 0, 0, 0, 0, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 2, 0, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 2, 0, 3, 0, 0, 0, 0, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 2, 0, 3, 3, 0, 2, 0, 2, 0, 0, 0, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 2, 0, 3, 0, 0, 2, 0, 3, 0, 2, 0, 0, 0, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 2, 0, 2, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 80, 0, 0, 2, 0, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 2, 0, 0, 0, 0, 2, 40, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 2, 40, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 3, 3, 3, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -3, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 80, 0, 0, 0, 0, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -3, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -4, -4, -4, -4, -4, -4, -4, -4, -4, -4, -4, -4, -4, -4, -4, -4, -4, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1} };

                if (UnityEngine.Random.Range(0, 100) < 50) // 50 대신 플레이어의 행운도 -> 행운도가 높으면 추가 기믹맵 생성O // TODO
                {
                    for (int x = 0; x < commonMap_Birth.GetLength(0); x++)
                    {
                        for (int y = 0; y < commonMap_Birth.GetLength(1); y++)
                        {
                            if (commonMap_Birth[x, y] < 0)
                                commonMap_Birth[x, y] = Math.Abs(commonMap_Birth[x, y] + 1);
                        }
                    }
                    Debug.Log("commonMap_Birth's 추가 기믹맵 생성O");
                }
                else
                {
                    for (int x = 0; x < commonMap_Birth.GetLength(0); x++)
                    {
                        for (int y = 0; y < commonMap_Birth.GetLength(1); y++)
                        {
                            if (commonMap_Birth[x, y] < 0)
                                commonMap_Birth[x, y] = 1;
                        }
                    }
                    Debug.Log("commonMap_Birth's 추가 기믹맵 생성X");
                }
                commonlMap_Stage1 = commonMap_Birth;

                break;
        }

        return commonlMap_Stage1;
    }

    // CommonField _ Stage2
    private static int[,] GetCommonField_Stage2(int width, int height, int commonSerial)
    {
        int[,] commonlMap_Stage2 = new int[width, height];

        return commonlMap_Stage2;
    }
}