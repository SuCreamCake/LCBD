using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbalMedicine : Potion_Parts_Item //한약
{
    private void Awake()
    {
        Price = 40; //아이템 가격
        item_number = 29; //아이템 번호
        item_Name = "HerbalMedicine"; ; //아이템이름 한약영어임
        Rank = Item_Rank.Rare; //아이템 희귀도
        item_type = Item_Type.Potion_Parts; //아이템 타입
        effect_info = Effect_Info.Stamina; //아이템 정보
        effect_active_type = Effect_Active_Type.Once; //효과 유형
        effect_figures = 3.0f; //얼마나 버프먹는지 정도
        effect_maintain_time = 15; //효과발동 후 효과적용되는 시간
        max_count = 99; //최대 소지갯수
        now_Count = 0; //현재 소지 갯수
    }


    public override void DestroyAfterTime()
    {

    }

    public override void Use_Effect() //이론상 구현됨
    {
        Debug.Log("한약");
        GameObject findPlayer = GameObject.FindWithTag("Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.endurance += effect_figures; // 스테미나(지구력) 증가
                //Debug.Log("스테미나 3.0회복.");
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
