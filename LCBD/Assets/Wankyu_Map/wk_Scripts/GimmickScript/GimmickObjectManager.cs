using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickObjectManager : MonoBehaviour
{
    [SerializeField]
    private Gimmick_BabyBottle_Object gimmick_BabyBottle_Object;

    [SerializeField]
    StageManager stageManager;

    public static bool IsRisingPlatform { get; private set; } = false;     // 플랫폼이 상승 중인지

    // Start is called before the first frame update
    void Start()
    {
        stageManager = GetComponent<StageManager>();

        gimmick_BabyBottle_Object = GetComponent<Gimmick_BabyBottle_Object>();  // 기믹맵 젖병의 기믹오브젝트.
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("gimmick"))
        {
            if (Input.GetKey(KeySetting.keys[KeyInput.TouchNPC]) && gimmick_BabyBottle_Object.getIsRisingFlatform())
            {
                gimmick_BabyBottle_Object.RiseFlatform(stageManager.GetPlayer().attackPower);
            }
        }
    }
}
