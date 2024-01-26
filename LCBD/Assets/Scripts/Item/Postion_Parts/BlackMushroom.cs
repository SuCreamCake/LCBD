using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BlackMushroom : Potion_Parts_Item
{
    private void Awake()
    {
        Price = 55; //아이템 가격
        item_number = 35; //아이템 번호
        item_Name = "BlackMushroom"; ; //아이템이름
        Rank = Item_Rank.Rare; //아이템 희귀도
        item_type = Item_Type.Potion_Parts; //아이템 타입
        effect_info = Effect_Info.Random; //아이템 정보
        effect_active_type = Effect_Active_Type.Once; //효과 유형
        effect_figures = 0; //얼마나 버프먹는지 정도
        effect_maintain_time = 15; //효과발동 후 효과적용되는 시간
        max_count = 99; //최대 소지갯수
        now_Count = 0; //현재 소지 갯수
    }

    // 무작위로 선택될 속성 리스트
    Effect_Info[] effects = new Effect_Info[] {
            Effect_Info.Attack_Speed,
            Effect_Info.Offense_Power,
            Effect_Info.Health,
            Effect_Info.Speed,
            Effect_Info.Stamina,
            Effect_Info.Defense,
            Effect_Info.Range,
            Effect_Info.Endurance
        };


    public override void DestroyAfterTime()
    {

    }

    public override void Use_Effect() //이론상 구현됨
    {
        Debug.Log("검은버섯");
        GameObject findPlayer = GameObject.FindWithTag("Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                ApplyRandomEffect(player);
                Debug.Log("랜덤할래.");
            }
        }
    }
    private void ApplyRandomEffect(Player player)
    {
       // SimplePlayerMove Splayer = player.GetComponent<SimplePlayerMove>();
        if (gameObject.activeInHierarchy) // GameObject가 활성화 상태인지 확인
        {
            Debug.Log("검은버섯");
            StartCoroutine(TemporaryEffect(player));
        }

    }


    private IEnumerator TemporaryEffect(Player player)
    {
        System.Random random = new System.Random();
        int effectIndex = random.Next(effects.Length); // 무작위 인덱스 선택
        Effect_Info randomEffect = effects[effectIndex];


        // 선택된 랜덤 효과 적용
        switch (randomEffect)
        {
            case Effect_Info.Attack_Speed:
                player.attackSpeed += 1; // 공격속도 증가
                Debug.Log("공격속도 1증가");
                break;
            case Effect_Info.Offense_Power:
                player.attackPower += 1; // 공격력 증가
                Debug.Log("공격력 1증가");
                break;
            case Effect_Info.Health:
                player.health += 1; // 체력 증가
                Debug.Log("체력 1증가");
                break;
            case Effect_Info.Speed:
                player.addSpeed(1); // 이동속도 증가
                Debug.Log("이동속도 1증가");
                break;
            case Effect_Info.Stamina:
                player.endurance += 1; // 스태미나 증가
                Debug.Log("스테미나 1증가");
                break;
            case Effect_Info.Defense:
                player.defense += 1; // 방어력 증가
                Debug.Log("방어력 1증가");
                break;
            case Effect_Info.Range:
                player.crossroads += 1; // 사거리 증가
                Debug.Log("사거리 1증가");
                break;
            case Effect_Info.Endurance:
                player.tenacity += 1; // 강인도 증가
                Debug.Log("강인도 1증가");
                break;
        }

        Debug.Log("적용된 효과: " + randomEffect.ToString());
        yield return new WaitForSeconds(effect_maintain_time); // 지정된 시간 동안 대기

        switch (randomEffect)
        {
            case Effect_Info.Attack_Speed:
                player.attackSpeed += 1; // 공격속도 증가
                Debug.Log("효과제거");
                break;
            case Effect_Info.Offense_Power:
                player.attackPower += 1; // 공격력 증가
                Debug.Log("효과제거");
                break;
            case Effect_Info.Health:
                player.health += 1; // 체력 증가
                Debug.Log("효과제거");
                break;
            case Effect_Info.Speed:
                player.addSpeed(1); // 이동속도 증가
                Debug.Log("효과제거");
                break;
            case Effect_Info.Stamina:
                player.endurance += 1; // 스태미나 증가
                Debug.Log("효과제거");
                break;
            case Effect_Info.Defense:
                player.defense += 1; // 방어력 증가
                Debug.Log("효과제거");
                break;
            case Effect_Info.Range:
                player.crossroads += 1; // 사거리 증가
                Debug.Log("효과제거");
                break;
            case Effect_Info.Endurance:
                player.tenacity += 1; // 강인도 증가
                Debug.Log("효과제거");
                break;
        }
    }
}