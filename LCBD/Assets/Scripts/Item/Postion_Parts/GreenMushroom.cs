using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMushroom : Potion_Parts_Item
{
    private void Awake()
    {
        Price = 35; //아이템 가격
        item_number = 33; //아이템 번호
        item_Name = "GreenMushroom"; ; //아이템이름
        Rank = Item_Rank.Common; //아이템 희귀도
        item_type = Item_Type.Potion_Parts; //아이템 타입
        effect_info = Effect_Info.Speed; //아이템 정보
        effect_active_type = Effect_Active_Type.Once; //효과 유형
        effect_figures = 0.3f; //얼마나 버프먹는지 정도
        effect_maintain_time = 15; //효과발동 후 효과적용되는 시간
        max_count = 99; //최대 소지갯수
        now_Count = 0; //현재 소지 갯수
    }


    public override void DestroyAfterTime()
    {

    }

    public override void Use_Effect() //이론상 구현됨
    {
        if (gameObject.activeInHierarchy) // GameObject가 활성화 상태인지 확인
        {
            Debug.Log("초록버섯");
            StartCoroutine(TemporaryEffect());
        }
    }

    private IEnumerator TemporaryEffect()
    {
        GameObject findPlayer = GameObject.Find("간단Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.addSpeed((int)effect_figures); // 이속 0.3 증가
                Debug.Log("이동속도 0.3 증가.");
            

                yield return new WaitForSeconds(effect_maintain_time); // 15초 대기

                player.subSpeed((int)effect_figures); // 스테미나 증가분 되돌리기
                Debug.Log("이동속도 증가 효과 종료.");
            }
        }

    }


}
