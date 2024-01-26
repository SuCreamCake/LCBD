using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Parts_Item : Item
{
    public int Weapon_Attack; //무기공격력
    public int Weapon_Range; //공격가능한 사거리
    public int Price; //가격
    public override void DestroyAfterTime()
    { }
    
    public override void Use_Effect() { }

    private void Awake()
    {
        item_type = Item_Type.Hand_Parts; //손 장착형 파츠; //아이템 타입
        max_count=1; //무기 최대 소지갯수는 1고정
        effect_target = Effect_Target.Self; //무기는 자기자신에게 적용
    }
}
