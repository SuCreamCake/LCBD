using System.Collections;
using UnityEngine;

public class BuffDebuffManager : MonoBehaviour
{
    [field : SerializeField] public static float WeightOfLife_duration { get; private set; } = 0f; // '디버프: 삶의 무게' 지속 시간.
    public static void SetWeightOfLife_duration(float value) { WeightOfLife_duration = value; }


    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }


    void Update()
    {
        // 버프 '체력 재생'
        if (Player_Buff_Debuff.HealthRegeneration)
        {
            if (player.health < player.maxHealth)   // 체력이 최대 체력 미만이면,
            {
                player.health += 2f * Time.deltaTime;   // 초당 체력 2 회복.


                if (player.health >= player.maxHealth)  //현재 체력이 최대 체력 이상이면,
                    player.health = player.maxHealth;   // 최대체력으로 캐스팅.
            }
        }


        // 디버프 '삶의 무게'
        if (Player_Buff_Debuff.WeightOfLife)
        {
            // '디버프: 삶의 무게' 지속시간이 0보다 크면, 지속시간이 줄어들도록.
            if (WeightOfLife_duration > 0)
            {
                WeightOfLife_duration -= Time.deltaTime;
            }
            else if (WeightOfLife_duration <= 0)
            {
                WeightOfLife_duration = 0f;
                Player_Buff_Debuff.SetWeightOfLife(false);  // 디버프 해제.
            }
        }
    }


}
