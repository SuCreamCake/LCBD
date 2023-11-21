using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public static class ObjectLocation  // 오브젝트 위치 좌표 static 클래스
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

    public static Point[] CommonField_1_BabyBottle_GimmickObjects = { new Point(29, 9), new Point(20, 16), new Point(32, 26), new Point(18, 33), new Point(29, 39), new Point(23, 47), new Point(11, 27), new Point(39, 27) };
    public static Point CommonField_1_BabyBottle_FallingFlatform = new Point(25, 6);
    public static Point[] CommonField_1_BabyBottle_Wall = { new Point(13, 26), new Point(13, 27), new Point(13, 28), new Point(13, 29), new Point(13, 30), new Point(37, 26), new Point(37, 27), new Point(37, 28), new Point(37, 29), new Point(37, 30) };
    public static Point CommonField_1_BabyBottle_ClearLever = new Point(25, 47);
    public static Point CommonField_1_BabyBottle_StartPoint = new Point(25, 8);


}

public class ObjectPlace : MonoBehaviour
{
    StageGenerator stageGenerator;
    MapGenerator[,] mapGenerator;

    public void SetObjectPlace()
    {
        stageGenerator = GetComponent<StageGenerator>();
        mapGenerator = stageGenerator.GetMapGenerator();

    }


}
