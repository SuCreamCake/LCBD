using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int item_number; //아이템 번호
    public string item_Name; //아이템이름
    public Item_Rank Rank; //아이템 희귀도
    public Item_Type item_type; //아이템 타입
    public Drop_age drop_age; //흭득 가능한 년기
    public Effect_Type effect_type; //효과 타입
    public Effect_Info effect_info; //효과 정보
    public Effect_Target effect_target; //효과 적용 대상
    public float effect_figures; //얼마나 버프먹는지 정도
    public Effect_Active_Type effect_active_type; //효과 유형
    public float effect_maintain_time; //효과발동 후 효과적용되는 시간
    public Sprite item_sprite; //아이템 이미지
    public int max_count; //최대 소지갯수


    public abstract void Use_Effect(); //사용효과 추상메소드
    public abstract void DestraoyAfterTime(); //사용 후 파괴 추상메소드

    public enum Item_Type //아이템 타입
    {
        Hand_Parts, //손 장착형 파츠
        Body_Parts, //몸 장착형 파츠
        Potion_Parts, //소모형 파츠
        List, //리스트형
        Goods //재화형
    }

    public enum Item_Rank //아이템 희귀도
    {
        Rare, //희귀등급
        Common,  //일반등급
        Unique //유일등급
    }

    public enum Drop_age //얻을 수 있는 성장기
    {
        Baby,  //유아기
        Child, //아동기
        Young, //청년기
        Adult, //성인기
        Old, //노년기
        All //전체에서 얻기가능

    }
    public enum Effect_Type //효과 타입
    {
        Enhance, //버프류
        unbeatable, //무적류
        ending_key, //엔딩키
        Open_Stage, //오픈스테이지
        Purchace, //구매류
        Null
    }

    public enum Effect_Info //효과정보 번역후 다시사용
    {
        Attack_Speed, //공격속도
        Offense_Power, //공격력
        Infinite, //지속효과
        Health, //체력회복
        Stamina, //지구력
        Defense, //방어력
        Range, //사거리
        Endurance, //강인도
        Random, //랜덤
        Money, //돈
        Null
    }

    public enum Effect_Target //효과대상 누구에게 적용
    {
        Self, //자기자신
        Object, //오브젝트
        NPC, //NPC
        Null //적용대상없음
    }

    public enum Effect_Active_Type //효과유형 지속,쿨타임 등
    {
        Infinite, //지속효과
        Once, //한번
        Null //사용안함
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ItemInventory.instance.AddItem(item_Name, item_sprite, this))
            {
                //Destroy(gameObject); // 아이템을 씬에서 제거
                gameObject.SetActive(false);
            }
            else
            {
                // 인벤토리가 가득 차 있다면, 메시지를 표시하거나 다른 로직을 수행
            }
        }
    }
}
