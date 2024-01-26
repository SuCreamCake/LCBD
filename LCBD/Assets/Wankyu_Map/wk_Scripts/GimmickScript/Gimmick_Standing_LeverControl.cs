using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick_Standing_LeverControl : MonoBehaviour
{
    public int LeverControlCount { get; private set; }
    public void LeverControlCountUp() { LeverControlCount++; Debug.Log("LeverControlCount :" + LeverControlCount); }


    [SerializeField] private GameObject walls;

    [SerializeField] private Transform bombsParent;
    [SerializeField] private GameObject[] bombs;

    private bool isClear;

    private void Awake()
    {
        isClear = false;

        LeverControlCount = 0;

        walls = transform.parent.GetChild(3).gameObject;
        walls.SetActive(true);

        bombsParent = transform.parent.GetChild(4);
        if (bombsParent != null)
        {
            bombs = new GameObject[bombsParent.childCount];
            for (int i = 0; i < bombs.Length; i++)
            {
                bombs[i] = bombsParent.GetChild(i).gameObject;
            }
        }


    }

    private void Update()
    {
        if (!isClear)
        {
            if (LeverControlCount >= 4)
            {
                ClearSignal();

                // 사운드 재생. 클리어 소리.

                walls.SetActive(false);

                for (int i = 0;i < bombs.Length;i++)
                {
                    bombs[i].GetComponent<Gimmick_Standing_Bomb>().Ignite();
                }
            }
        }
    }

    public void ClearSignal()
    {
        isClear = true;

        StageGenerator stageGenerator;
        stageGenerator = FindObjectOfType<StageGenerator>();

        MapGenerator[,] mapGenerator = stageGenerator.GetMapGenerator();

        int x = (int)(transform.position.x / (50 + 1));     // 수정 필요
        int y = (int)(transform.position.y / (50 + 1));

        mapGenerator[x, y].Fields.SetIsClear(true);
    }
}
