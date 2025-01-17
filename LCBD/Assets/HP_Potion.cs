using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Potion : Item
{

    private void Awake()
    {
        item_number = 27; //아이템 번호
        item_Name = "HP_Potion";; //아이템이름
        Rank = Item_Rank.Common; //아이템 희귀도
        item_type = Item_Type.Potion_Parts; //아이템 타입
        drop_age = Drop_age.All; //흭득 가능한 년기
        effect_type = Effect_Type.Enhance; //아이템 타입
        effect_info = Effect_Info.Health; //아이템 정보
        effect_target = Effect_Target.Self; //효과 적용 대상
        effect_figures = 10; //얼마나 버프먹는지 정도
        effect_active_type = Effect_Active_Type.Once; //효과 활성 타입 일회성인지 지속성인지
        effect_maintain_time=0; //효과발동 후 효과적용되는 시간
        max_count = 99; //최대 소지갯수
}


    public override void DestroyAfterTime()
    {
        
    }

    public override void Use_Effect()
    {
        Debug.Log("체력 포션사용");
        GameObject findPlayer = GameObject.Find("Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.health += effect_figures; // 체력을 10(한칸)회복시킴
                Debug.Log("체력 10회복! 한칸임.");
            }
        }
    }

}
