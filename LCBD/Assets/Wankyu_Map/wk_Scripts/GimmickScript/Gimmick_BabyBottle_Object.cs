using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Gimmick_BabyBottle_Object : MonoBehaviour
{
    public GameObject fallingFlatform;

    private bool isRisingFlatform;
    public bool getIsRisingFlatform() { return isRisingFlatform; }

    private void Awake()
    {
        isRisingFlatform = false;
    }

    public void RiseFlatform(int attackPower)
    {
        attackPower = 3;    //임시
        float risingDistance = (float)attackPower / 3;
        if (!isRisingFlatform)
        {
            StartCoroutine(RiseFlatformCoroutine(risingDistance));
        }
    }

    private IEnumerator RiseFlatformCoroutine(float risingDistance)
    {
        isRisingFlatform = true;

        Vector3 flatformPosition = fallingFlatform.transform.position;
        Vector3 flatformScale = fallingFlatform.transform.localScale;

        int layerMask = 1 << LayerMask.NameToLayer("background");
        bool isHit;

        float risingTime = 0f;

        while (risingTime < 3f)
        {
            isHit = Physics2D.BoxCast(flatformPosition, flatformScale, 0, transform.up, 0.5f, layerMask);
            risingTime += Time.deltaTime;

            transform.Translate(0, risingDistance / 3f * Time.deltaTime, 0);
            if (isHit)
            {
                isRisingFlatform = false;
                yield break;
            }
            yield return null;
        }

        yield break;
    }
}