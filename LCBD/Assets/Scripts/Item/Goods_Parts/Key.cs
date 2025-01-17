using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Goods_Item
{
    private void Awake() //해당아이템 초기값 설정
    {
        item_number = 43;
        item_Name = "Key"; //아이템이름
        Rank = Item_Rank.Common; //아이템 희귀도
        effect_type = Effect_Type.Open_Stage; //효과 타입
        effect_info = Effect_Info.Null; //효과 정보
        effect_target = Effect_Target.Object; //효과 적용 대상
        effect_figures = 0; //얼마나 버프먹는지 정도
        effect_maintain_time = 0; //효과발동 후 효과적용되는 시간
        //item_sprite; //아이템 이미지 이미지가 없어요

        now_Count = 0; //현재 소지 갯수
    }

    public override void DestroyAfterTime() //사용후 작업
    { }

    public override void Use_Effect() //사용효과
    { }
}
