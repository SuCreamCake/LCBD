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
        Debug.Log("이 아이템이름 : " + Itemname + "힐량: " + health);
    }
}
