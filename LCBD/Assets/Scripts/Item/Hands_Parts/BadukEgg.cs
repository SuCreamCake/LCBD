using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadukEgg : Hand_Parts_Item
{
    private void Awake()
    {
        Price = 85;
        item_number = 16;
        item_Name = "Sand"; //아이템이름
        Rank = Item_Rank.Common; //아이템 희귀도
        drop_age = Drop_age.Old; //흭득 가능한 년기
        Weapon_Attack = 12;
        Weapon_Range = 0;
        effect_type = Effect_Type.Enhance; //효과 타입
        effect_info = Effect_Info.Attack_Speed; //효과 정보
        effect_target = Effect_Target.Self; //효과 적용 대상
        effect_figures = 3; //얼마나 버프먹는지 정도
        effect_active_type = Effect_Active_Type.Infinite; //효과 유형
        effect_maintain_time = 0; //효과발동 후 효과적용되는 시간
                                  //item_sprite; //아이템 이미지 이미지가 없어요
    }
    private void Update()
    {
        GameObject findPlayer = GameObject.Find("간단Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.attackSpeed += effect_figures; // 공격속도 3 향상
                //Debug.Log("공격속도 3 향상.");
            }
        }
    }

    public override void DestraoyAfterTime() //사용후 작업
    { }

    public override void Use_Effect() //사용효과
    { }
}
