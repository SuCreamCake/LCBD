using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow_Personality_Control : Item
{
    private void Awake()
    {
        item_number = 52; //아이템 번호
        item_Name = "Grow_Personality_Control"; //아이템이름
        Rank = Item_Rank.Unique; //아이템 희귀도
        item_type = Item_Type.Status; //아이템 타입
        drop_age = Drop_age.All; //흭득 가능한 년기
        effect_type = Effect_Type.Null; //효과 타입
        effect_info = Effect_Info.Null; //효과 정보
        effect_target = Effect_Target.Null; //효과 적용 대상
        effect_figures = 0; //얼마나 버프먹는지 정도
        effect_active_type = Effect_Active_Type.Null; //효과 유형
        effect_maintain_time = 0; //효과발동 후 효과적용되는 시간
                                  //item_sprite; //아이템 이미지
        max_count = 1; //최대 소지갯수
    }
    public override void DestraoyAfterTime()
    {
        throw new System.NotImplementedException();
    }

    public override void Use_Effect()
    {
        throw new System.NotImplementedException();
    }
}

