//포탈의 필드의 위치 좌표(i, j)와 맵 안의 좌표(X, Y)를 담는 클래스
using System;
using System.Collections;
using UnityEngine;

public class PortalPoint
{
    private int fieldX; //필드X
    private int fieldY; //필드Y
    private int mapX;   //맵X
    private int mapY;   //맵Y

    public PortalPoint(int i = 0, int j = 0, int x = 0, int y = 0)
    {
        fieldX = i;
        fieldY = j;
        mapX = x;
        mapY = y;
    }

    public int FieldX { get { return fieldX; } private set { fieldX = value; } }
    public int FieldY { get { return fieldY; } private set { fieldY = value; } }
    public int MapX { get { return mapX; } private set { mapX = value; } }
    public int MapY { get { return mapY; } private set { mapY = value; } }

    public override bool Equals(object obj)
    {
        return Equals(obj as PortalPoint);
    }

    public bool Equals(PortalPoint otherField)
    {
        return otherField != null &&
            fieldX == otherField.fieldX &&
            fieldY == otherField.fieldY &&
            mapX == otherField.mapX &&
            mapY == otherField.mapY;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(fieldX, fieldY, mapX, mapY);
    }

    public override string ToString()
    {
        string msg = "(" + fieldX + ", " + fieldY + ")-" + "-(" + mapX + ", " + mapY + ")";
        return msg;
    }
}