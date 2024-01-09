using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body_Parts_Item : Item
{
    public override void DestraoyAfterTime()
    { }

    public override void Use_Effect() { }

    private void Awake()
    {
        item_type = Item_Type.Body_Parts; //손 장착형 파츠; //아이템 타입
        max_count = 1; //방어구 최대 소지갯수는 1고정
        effect_target = Effect_Target.Self; //방어구는 무조건 자기자신적용
    }
}
