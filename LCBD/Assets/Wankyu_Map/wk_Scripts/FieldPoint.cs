//필드의 X Y 좌표를 담는 클래스
using System;
using System.Collections;
using UnityEngine;

public class FieldPoint
{
    private int x;
    private int y;

    public FieldPoint(int x = 0, int y = 0)
    {
        this.x = x;
        this.y = y;
    }

    public int X { get { return x; } }
    public int Y { get { return y; } }

    public void SetX(int x) { this.x = x; }
    public void SetY(int y) { this.y = y; }

    public override bool Equals(object obj)
    {
        return Equals(obj as FieldPoint);
    }

    public bool Equals(FieldPoint otherField)
    {
        return otherField != null &&
            x == otherField.x &&
            y == otherField.y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }

    public override string ToString()
    {
        string msg = "(" + x + ", " + y + ")";
        return msg;
    }
}