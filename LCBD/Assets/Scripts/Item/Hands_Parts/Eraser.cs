using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : Hand_Parts_Item
{
    private void Awake()
    {
        Price = 55;
        item_number = 7;
        item_Name = "Eraser"; //아이템이름
        Rank = Item_Rank.Common; //아이템 희귀도
        drop_age = Drop_age.Young; //흭득 가능한 년기
        Weapon_Attack = 8; //공격력
        Weapon_Range = 0; //공격범위
        effect_type = Effect_Type.Enhance; //효과 타입
        effect_info = Effect_Info.Attack_Speed; //효과 정보
        effect_target = Effect_Target.Self; //효과 적용 대상
        effect_figures = 3; //얼마나 버프먹는지 정도
        effect_active_type = Effect_Active_Type.Infinite; //효과 유형
        effect_maintain_time = 0; //효과발동 후 효과적용되는 시간
                                  //item_sprite; //아이템 이미지 이미지가 없어요

        now_Count = 0; //현재 소지 갯수
    }
    private void Update()
    {}
    public override void DestroyAfterTime() //사용후 작업
    { }

    public override void Use_Effect() //사용효과
    {
        GameObject findPlayer = GameObject.FindWithTag("Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.attackSpeed += (int)effect_figures; // 공격속도 3 향상
            }
        }
    }
}
