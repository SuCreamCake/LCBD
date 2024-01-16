using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanSam : Potion_Parts_Item
{
    private void Awake()
    {
        Price = 95; //아이템 가격
        item_number = 36; //아이템 번호
        item_Name = "SanSam"; ; //아이템이름
        Rank = Item_Rank.Unique; //아이템 희귀도
        item_type = Item_Type.Potion_Parts; //아이템 타입
        effect_type = Effect_Type.unbeatable; //무적기?
        effect_info = Effect_Info.Null; //아이템 정보
        effect_active_type = Effect_Active_Type.Once; //효과 유형
        effect_figures = 0; //얼마나 버프먹는지 정도
        effect_maintain_time = 10; //효과발동 후 효과적용되는 시간
        max_count = 99; //최대 소지갯수
    }


    public override void DestroyAfterTime()
    {

    }

    public override void Use_Effect() //이론상 구현됨
    {
        Debug.Log("산삼 사용");
        GameObject findPlayer = GameObject.Find("Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                //player.HP += effect_figures; // 해줄게 없어
                Debug.Log("산삼사용함.");
            }
        }
    }

    /*public override void Use_Effect()
    {
        Debug.Log("체력 포션사용");
        GameObject findPlayer = GameObject.Find("간단Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.health += effect_figures; // 체력을 10(한칸)회복시킴
                Debug.Log("체력 10회복! 한칸임.");
            }
        }
    }*/
}