using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion_Parts_Item : Item
{
    public override void DestraoyAfterTime()
    { }

    public override void Use_Effect() { }

    private void Awake()
    {
        item_type = Item_Type.Potion_Parts; //손 장착형 파츠; //아이템 타입
        max_count = 99; //포션류 아이템은 최대 소지갯수 99개
        effect_target = Effect_Target.Self; //포션류는 무조건 자기자신적용
        effect_active_type = Effect_Active_Type.Once; //소모성 타입
        effect_type = Effect_Type.Enhance; //웬만하면 증감용 산삼은 무적으로 오버라이딩
        drop_age = Drop_age.All; //전연령에서 얻을 수 있음
    }
}
