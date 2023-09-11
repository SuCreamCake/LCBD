using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Data", menuName = "ScripTableObjects/ItmeData",order =1)]
public class Item : ScriptableObject
{
    public string Itemname;
    public Sprite sprite;
    public int health;

    public void print()
    {
        Debug.Log("�� �������̸� : " + Itemname + "����: " + health);
    }
}
