using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick_Standing_NormalLever : MonoBehaviour, IControlGimmickObject
{
    [SerializeField] private Sprite off_state;  // off 이미지.
    [SerializeField] private Sprite on_state;   // on 이미지.

    private SpriteRenderer spriteRenderer;

    private bool isPulled = false;
    private bool isPulling = false;
    private Gimmick_Standing_LeverControl control;

    SoundsPlayer SFXPlayer;


    private void Awake()
    {
        control = transform.parent.GetComponent<Gimmick_Standing_LeverControl>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = off_state;

        SFXPlayer = GameObject.Find("SFXPlayer").GetComponent<SoundsPlayer>();

    }

    public void ControlGimmickObject()
    {
        if (!isPulling)
        {
            if (!isPulled)
            {
                // 사운드 재생. 레버 조작 소리.
                SFXPlayer.Gimmick01Sound(3);
                PullLever();
            }
        }
    }

    private void PullLever()
    {
        isPulling = true;
        isPulled = true;

        spriteRenderer.sprite = on_state;
        control.LeverControlCountUp();

        isPulling = false;
    }

}
