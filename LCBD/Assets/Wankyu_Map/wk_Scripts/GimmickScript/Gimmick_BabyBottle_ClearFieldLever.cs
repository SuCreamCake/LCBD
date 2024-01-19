using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick_BabyBottle_ClearFieldLever : MonoBehaviour, IControlGimmickObject
{
    private bool isClear;

    public Sprite left_state;     // 왼쪽 이미지 (off).
    public Sprite right_state;    // 오른쪽 이미지 (on).

    [SerializeField] private GameObject walls;     // 벽 오브젝트들.
    
    private void Awake()
    {
        isClear = false;
        walls = transform.parent.GetChild(1).gameObject;
        walls.SetActive(true);

        GetComponent<SpriteRenderer>().sprite = left_state;
    }

    public void ControlGimmickObject()
    {
        if (isClear == false)
        {
            ClearSignal();
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
