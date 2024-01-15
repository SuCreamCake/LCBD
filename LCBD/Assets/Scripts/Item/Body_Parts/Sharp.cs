using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharp : Body_Parts_Item
{
    private void Awake() //해당아이템 초기값 설정
    {
        Price = 65;
        item_number = 22;
        item_Name = "Sharp"; //아이템이름
        Rank = Item_Rank.Rare; //아이템 희귀도
        drop_age = Drop_age.Young; //흭득 가능한 년기
        effect_type = Effect_Type.Enhance; //효과 타입
        effect_info = Effect_Info.Offense_Power; //효과 정보
        effect_target = Effect_Target.Self; //효과 적용 대상
        effect_figures = 0.2f; //얼마나 버프먹는지 정도
        effect_active_type = Effect_Active_Type.Infinite; //효과 유형
        effect_maintain_time = 0; //효과발동 후 효과적용되는 시간
        //item_sprite; //아이템 이미지 이미지가 없어요
    }

    private void Update()
    {

    }

    public override void DestraoyAfterTime() //사용후 작업
    { }

    public override void Use_Effect() //사용효과
    {
        GameObject findPlayer = GameObject.Find("간단Player");
        if (findPlayer != null)
        {
            SimplePlayerMove player = findPlayer.GetComponent<SimplePlayerMove>();
            if (player != null)
            {
                player.Attack += 4; // 이동속도 2 향상
                //Debug.Log("이동속도 2 향상.");
            }
        }
    }
}