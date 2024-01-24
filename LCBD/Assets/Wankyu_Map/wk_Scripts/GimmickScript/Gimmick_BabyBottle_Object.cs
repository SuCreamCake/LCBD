using System.Collections;
using UnityEngine;

public class Gimmick_BabyBottle_Object : MonoBehaviour, IControlGimmickObject
{
    private GameObject fallingPlatform;

    private bool isRisingPlatform;

    Player player;

    private void Awake()
    {
        fallingPlatform = transform.parent.parent.GetChild(2).gameObject;
        isRisingPlatform = false;
        player = FindObjectOfType<StageManager>().GetPlayer();
    }

    public void ControlGimmickObject()
    {
        RisePlatform(player.attackPower);
    }

    public void RisePlatform(int attackPower)
    {
        if (!isRisingPlatform)
        {
            // 사운드 재생. 오브젝트 조작 성공 소리. 엘리베이터 띵 소리 느낌.
            float risingDistance = (float)attackPower / 3;
            StartCoroutine(RisePlatformCoroutine(risingDistance));
        }
        else
        {
            // 사운드 재생. 오브젝트 조작 실패 소리. 철컥거리는 소리 느낌.
        }
    }

    private IEnumerator RisePlatformCoroutine(float risingDistance)
    {
        isRisingPlatform = true;

        int layerMask = 1 << LayerMask.NameToLayer("Block");
        bool isHit;

        float risingTime = 0f;
        float risingTotalTime = risingDistance;

        Vector3 startPosition = fallingPlatform.transform.position;
        Vector3 endPosition = fallingPlatform.transform.position + new Vector3(0, risingDistance, 0);

        while (risingTime < risingTotalTime)
        {
            risingTime += Time.deltaTime;

            fallingPlatform.transform.position = Vector3.Lerp(startPosition, endPosition, risingTime / risingTotalTime);

            Vector3 hitFlatformPosition = fallingPlatform.transform.position + new Vector3(0, fallingPlatform.transform.lossyScale.y * 0.1f, 0);
            Vector3 hitFlatformScale = new Vector3(fallingPlatform.transform.lossyScale.x, fallingPlatform.transform.lossyScale.y * 0.8f, fallingPlatform.transform.lossyScale.z);
            isHit = Physics2D.BoxCast(hitFlatformPosition, hitFlatformScale, 0, fallingPlatform.transform.up, 1.0f, layerMask);
            if (isHit)  // 만약, 부딪히면 break;
            {
                isRisingPlatform = false;
                yield break;
            }
            yield return null;
        }

        fallingPlatform.transform.position = endPosition;   // 정상 종료되면, 확실히 하기위해 endPosition 넣어줌

        isRisingPlatform = false;
        yield break;
    }

    void Update()
    {
        FallPlatform();
    }

    private void FallPlatform()
    {
        // 하강
        int layerMask = LayerMask.GetMask("Block");   // 변경 필요 레이어 분리 필요 

        Vector3 flatformPosition = fallingPlatform.transform.position + new Vector3(0, fallingPlatform.transform.lossyScale.y * -0.1f, 0);
        Vector3 flatformScale = new Vector3(fallingPlatform.transform.lossyScale.x, fallingPlatform.transform.lossyScale.y * 0.8f, fallingPlatform.transform.lossyScale.z);
        bool isHit = Physics2D.BoxCast(flatformPosition, flatformScale, 0f, fallingPlatform.transform.up * -1, 1f, layerMask);

        // 플랫폼 하강 (상승 중이 아닐 때 && "background" layer 감지 중이 아닐 때).
        if (isRisingPlatform == false && !isHit)
        {
            fallingPlatform.transform.Translate(0, -0.1f * Time.deltaTime, 0);
        }
    }

    void OnDrawGizmos()
    {
        int layerMask = LayerMask.GetMask("Block");

        bool isHit = Physics2D.BoxCast(fallingPlatform.transform.position, fallingPlatform.transform.lossyScale, 0, fallingPlatform.transform.up, 0, layerMask);

        if (isHit)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(fallingPlatform.transform.position, fallingPlatform.transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(fallingPlatform.transform.position, fallingPlatform.transform.lossyScale);
        }
    }
}