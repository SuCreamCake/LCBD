using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : Hand_Parts_Item
{
    private void Awake() //해당아이템 초기값 설정
    {
        Price = 45;
        item_number = 2;
        item_Name = "Pillow"; //아이템이름
        Rank = Item_Rank.Common; //아이템 희귀도
        Weapon_Attack = 7;
        Weapon_Range = 1;
        drop_age = Drop_age.Baby; //득 가능한 년기
        effect_type = Effect_Type.Null; //효과 타입
        effect_info = Effect_Info.Null; //효과 정보
        effect_target = Effect_Target.Null; //효과 적용 대상
        effect_figures = 0; //얼마나 버프먹는지 정도
        effect_active_type = Effect_Active_Type.Null; //효과 유형
        effect_maintain_time = 0; //효과발동 후 효과적용되는 시간
                                  //item_sprite; //아이템 이미지 이미지가 없어요

        now_Count = 0; //현재 소지 갯수
    }

    public override void DestroyAfterTime() //사용후 작업
    { }

    public override void Use_Effect() //사용효과
    { }
}