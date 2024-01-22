using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchovy : Potion_Parts_Item
{
    
    private void Awake()
    {
        Price = 30; //아이템 가격
        item_number = 30; //아이템 번호
        item_Name = "Anchovy"; ; //아이템이름
        Rank = Item_Rank.Common; //아이템 희귀도
        item_type = Item_Type.Potion_Parts; //아이템 타입
        effect_info = Effect_Info.Defense; //아이템 정보
        effect_active_type = Effect_Active_Type.Once; //효과 유형
        effect_figures = 2.0f; //얼마나 버프먹는지 정도
        effect_maintain_time = 15; //효과발동 후 효과적용되는 시간
        max_count = 99; //최대 소지갯수
        now_Count = 0; //현재갯수
    }


    public override void DestroyAfterTime()
    {

    }

    public override void Use_Effect() //이론상 구현됨
    {
        if (gameObject.activeInHierarchy) // GameObject가 활성화 상태인지 확인
        {
            Debug.Log("멸치");
            StartCoroutine(TemporaryEffect());
        }
    }

    private IEnumerator TemporaryEffect()
    {
        GameObject findPlayer = GameObject.FindWithTag("Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.defense += (int)effect_figures; // 스테미나 증가
                Debug.Log("스테미나 2.0회복.");

                yield return new WaitForSeconds(effect_maintain_time); // 15초 대기

                player.defense -= (int)effect_figures; // 스테미나 증가분 되돌리기
                Debug.Log("스테미나 회복 효과 종료.");
            }
        }
    }
}
