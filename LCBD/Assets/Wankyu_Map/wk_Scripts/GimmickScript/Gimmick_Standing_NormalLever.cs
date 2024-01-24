﻿using System.Collections;
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

    private void Awake()
    {
        control = transform.parent.GetComponent<Gimmick_Standing_LeverControl>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = off_state;
    }

    public void ControlGimmickObject()
    {
        if (!isPulling)
        {
            if (!isPulled)
            {
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
