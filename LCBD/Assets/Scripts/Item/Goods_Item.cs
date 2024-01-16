using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goods_Item : Item
{
    public override void DestroyAfterTime()
    { }

    public override void Use_Effect() { }

    private void Awake()
    {
        item_type = Item_Type.Goods; //리스트 아이템 타입
        drop_age = Drop_age.All; //전연령 흭득 가능한 년기
        effect_type = Effect_Type.Purchace; //일기장 말고는 다 Null 타입
        effect_info = Effect_Info.Money; //효과 정보
        effect_target = Effect_Target.NPC; //효과 적용 대상
        effect_figures = 0; //얼마나 버프먹는지 정도
        effect_active_type = Effect_Active_Type.Once; //효과 유형
        effect_maintain_time = 0; //효과발동 후 효과적용되는 시간
    }
}
