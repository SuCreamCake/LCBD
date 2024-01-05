using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private int item_number; //아이템 번호
    private string item_Name; //아이템이름
    private string Rank; //아이템 희귀도
    private string item_type; //아이템 타입
    private float effect_time; //사용 유지 시간
    private string drop_age; //흭득 가능한 년기

    public abstract void Use_Effect(); //사용효과 추상메소드
    public abstract void DestraoyAfterTime(); //사용 후 파괴 추상메소드

    public enum ItemRarity
    {

    }


}
