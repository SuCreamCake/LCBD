using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FallingPlatform_BabyBottle_g : MonoBehaviour
{
    private RaycastHit2D hit;
    private float maxDistance = 0.8f;

    void Update()
    {
        // raycast 초당 -0.5씩 y축 이동 (background layer 감지 중이 아닐 때,).

        int layerMask = LayerMask.GetMask("background");

        bool isHit = Physics2D.BoxCast(transform.position, transform.lossyScale, 0f, transform.up * -1, 1f, layerMask);

        Debug.Log("hit.collider:" + hit.collider);
        if (!isHit)
        {
            transform.Translate(0, -0.5f * Time.deltaTime, 0);
        }
    }

    void OnDrawGizmos()
    {
        int layerMask = LayerMask.GetMask("background");
        // Physics.BoxCast (레이저를 발사할 위치, 사각형의 각 좌표의 절판 크기, 발사 방향, 충돌 결과, 회전 각도, 최대 거리)
        bool isHit = Physics2D.BoxCast(transform.position, transform.lossyScale, 0, transform.up,0 ,layerMask);

        if (isHit)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(transform.position, transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, transform.lossyScale);
        }
    }
}