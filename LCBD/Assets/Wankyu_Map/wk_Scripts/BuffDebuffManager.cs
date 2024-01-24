using System.Collections;
using UnityEngine;

public class BuffDebuffManager : MonoBehaviour
{
    [field : SerializeField] public static float WeightOfLife_duration { get; private set; } = 0f; // '디버프: 삶의 무게' 지속 시간.
    public static void SetWeightOfLife_duration(float value) { WeightOfLife_duration = value; }

    void Update()
    {
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
