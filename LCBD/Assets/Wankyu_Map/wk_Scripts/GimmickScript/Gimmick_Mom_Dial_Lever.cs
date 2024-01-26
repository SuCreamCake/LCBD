using System.Collections;
using TMPro;
using UnityEngine;

public class Gimmick_Mom_Dial_Lever : MonoBehaviour, IControlGimmickObject
{
    [SerializeField] private GameObject DialObject;
    [SerializeField] private TextMeshPro CurrentCountText;

    private bool isMovingSlot;  // 슬롯이 이동 중인 지

    SoundsPlayer SFXPlayer;

    private void Awake()
    {
        isMovingSlot = false;
        SFXPlayer = GameObject.Find("SFXPlayer").GetComponent<SoundsPlayer>();
    }

    public void ControlGimmickObject()
    {
        if (isMovingSlot == false)
        {
            // 사운드 재생. 바위 같은거 움직이는 소리?
            SFXPlayer.Gimmick01Sound(4);
            StartCoroutine(MoveSlotCoroutine());
        }
    }

    private IEnumerator MoveSlotCoroutine()
    {
        isMovingSlot = true;

        int.TryParse(CurrentCountText.text.ToString(), out int count);   // TextMechPro-Text에서 텍스트 읽어와서, int로 캐스팅하여 count에 저장. / int.TryParse() : 문자열을 정수로 변환, 정수 이외이면 변환 실패로 0 리턴함.

        Vector3 fromOffset = transform.parent.GetChild(1).localPosition;    // 시작 위치
        Vector3 toOffset = new(0, (fromOffset.y + 1) % 3, 0);               // 끝 위치 (시작 위치의 y+1)

        float time = 0f;
        float duration = 0.5f;    //수행 시간

        while (time < duration)
        {
            DialObject.transform.localPosition = Vector3.Lerp(fromOffset, toOffset, time / duration);      // 선형 보간 이동 (부드러운 이동 느낌).
            time += Time.deltaTime;

            yield return null;
        }

        DialObject.transform.localPosition = toOffset;   //마지막에 끝위치로 위치 지정

        CurrentCountText.SetText((count + 1).ToString());     // TextMechPro-Text에 텍스트 적어주기. (count +1).

        isMovingSlot = false;

        yield break;
    }
}
