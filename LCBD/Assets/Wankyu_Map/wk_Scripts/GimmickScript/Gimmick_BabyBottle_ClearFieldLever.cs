using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick_BabyBottle_ClearFieldLever : MonoBehaviour, IControlGimmickObject
{
    private bool isClear;

    public Sprite left_state;     // 왼쪽 이미지 (off).
    public Sprite right_state;    // 오른쪽 이미지 (on).

    [SerializeField] private GameObject walls;     // 벽 오브젝트들.

    SoundsPlayer SFXPlayer; //

    private void Awake()
    {
        isClear = false;
        walls = transform.parent.GetChild(1).gameObject;
        walls.SetActive(true);

        GetComponent<SpriteRenderer>().sprite = left_state;
        SFXPlayer = GameObject.Find("SFXPlayer").GetComponent<SoundsPlayer>();
    }

    public void ControlGimmickObject()
    {
        if (isClear == false)
        {
            // 사운드 재생. 레버 조작 소리.
            SFXPlayer.Gimmick01Sound(3);
            ClearSignal();
            // 사운드 재생. 클리어 소리.
            SFXPlayer.Gimmick01Sound(1);
        }
    }

    private void ClearSignal()
    {
        isClear = true;

        StageGenerator stageGenerator;
        stageGenerator = FindObjectOfType<StageGenerator>();

        MapGenerator[,] mapGenerator = stageGenerator.GetMapGenerator();

        int x = (int)(transform.position.x / (50 + 1));     // 수정 필요
        int y = (int)(transform.position.y / (50 + 1));

        mapGenerator[x, y].Fields.SetIsClear(true);

        walls.SetActive(false);

        GetComponent<SpriteRenderer>().sprite = right_state;
    }
}
