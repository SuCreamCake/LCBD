using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class Gimmick_Mom_Dial_Lever : MonoBehaviour, IControlGimmickObject
{
    public GameObject DialObject;
    public GameObject CurrentCount;

    private bool isMovingSlot;  // 슬롯이 이동 중인 지

    private void Awake()
    {
        isMovingSlot = false;
    }

    public void ControlGimmickObject()
    {
        if (isMovingSlot == false)
        {
            StartCoroutine(MoveSlotCoroutine());
        }
    }

    IEnumerator MoveSlotCoroutine()
    {
        isMovingSlot = true;

        int.TryParse(CurrentCount.transform.GetChild(0).GetComponent<TextMeshPro>().text, out int count);   // TextMechPro-Text에서 텍스트 읽어와서, int로 캐스팅하여 count에 저장.

        CurrentCount.transform.GetChild(0).GetComponent<TextMeshPro>().SetText((count + 1).ToString());     // TextMechPro-Text에 텍스트 적어주기. (count +1).


        Vector3 fromOffset = new(0, count % 3, 0);      // 시작 위치
        Vector3 toOffset = new(0, (count + 1) % 3, 0);  // 끝 위치

        float time = 0f;
        float duration = 0.5f;    //수행 시간
        while (time < duration)
        {
            DialObject.transform.localPosition = Vector3.Lerp(fromOffset, toOffset, time / duration);      // 선형 보간 이동 (부드러운 이동).
            time += Time.deltaTime;

            yield return null;
        }

        DialObject.transform.localPosition = toOffset;   //마지막에 끝위치로 위치 지정

        isMovingSlot = false;

        yield break;
    }
}
