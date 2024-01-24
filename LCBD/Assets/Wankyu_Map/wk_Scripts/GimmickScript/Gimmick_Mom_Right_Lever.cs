using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Gimmick_Mom_Right_Lever : MonoBehaviour, IControlGimmickObject
{
    [SerializeField] private Sprite left_state;     // 왼쪽 이미지 (off).
    [SerializeField] private Sprite right_state;    // 오른쪽 이미지 (on).

    private Transform Dials;
    private Transform leftDials;
    private Transform rightDials;

    private Transform wallsParent;          // 벽 오브젝트.

    private bool isRunning = false; // 레버가 작동 중인지.

    private GameObject[] leftDialsSelectedSlot;  // 왼쪽 다이얼 기믹 오브젝트들의 selectedObj.
    private GameObject[] rightDialsSelectedSlot; // 오른쪽 다이얼 기믹 오브젝트들의 selectedObj.
    
    private TextMeshPro needCountText;      // (왼쪽) 필요 횟수 디스플레이 오브젝트의 텍스트.
    private TextMeshPro currentCountText;   // (오른쪽) 현재 횟수 디스플레이 오브젝트의 텍스트.

    private void Awake()
    {
        // 스프라이트 모양을 왼쪽 그림으로.
        GetComponent<SpriteRenderer>().sprite = left_state;

        // 각 다이얼의 selectedObj의 Transform 가져옴.
        Dials = transform.parent.GetChild(0);
        if (Dials != null )
        {
            leftDials = Dials.GetChild(0);
            if (leftDials != null)
            {
                leftDialsSelectedSlot = new GameObject[leftDials.childCount];
                for (int i = 0; i < leftDialsSelectedSlot.Length; i++)
                {
                    if (leftDials.GetChild(i) != null)
                    {
                        leftDialsSelectedSlot[i] = leftDials.GetChild(i).GetChild(1).gameObject;
                    }
                }
            }

            rightDials = Dials.GetChild(1);
            if (rightDials != null)
            {
                rightDialsSelectedSlot = new GameObject[rightDials.childCount];
                for (int i = 0; i < rightDialsSelectedSlot.Length; i++)
                {
                    if (rightDials.GetChild(i) != null)
                    {
                        rightDialsSelectedSlot[i] = rightDials.GetChild(i).GetChild(1).gameObject;
                    }
                }
            }
        }

        // 벽 가져옴. 벽 켜기.
        wallsParent = transform.parent.parent.GetChild(1);
        wallsParent.gameObject.SetActive(true);

        // 왼쪽 필요 횟수 디스플레이 가져옴.
        needCountText = transform.parent.GetChild(1).GetChild(0).GetComponent<TextMeshPro>();
        // 오른쪽 현재 횟수 디스플레이 가져옴.
        currentCountText = transform.parent.GetChild(2).GetChild(0).GetComponent<TextMeshPro>();
    }

    public void ControlGimmickObject()
    {
        if (!isRunning)
        {
            // 사운드 재생. 레버 조작 소리.
            PullLever();
        }
    }

    private void PullLever()
    {
        isRunning = true;

        if (CheckCorrect())
        {
            // 클리어.
            // 벽 끄기.
            wallsParent.gameObject.SetActive(false);
            // 클리어 신호 보내기.
            ClearSignal();

            // 사운드 재생. 클리어 소리.
        }
        else
        {
            // 틀림.
            // 슬롯 상태 초기화.
            ResetSlot();
        }

        isRunning = false;
    }

    private bool CheckCorrect()
    {
        string needCount = needCountText.text;
        string currentCount = currentCountText.text;

        float[] leftValue = new float[leftDialsSelectedSlot.Length];
        float[] rightValue = new float[rightDialsSelectedSlot.Length];

        for (int i = 0; i < leftValue.Length; i++)
        {
            leftValue[i] = leftDialsSelectedSlot[i].transform.localPosition.y;
        }
        for (int i = 0; i < rightValue.Length; i++)
        {
            rightValue[i] = rightDialsSelectedSlot[i].transform.localPosition.y;
        }

        bool isEqual = false;
        // 필요 횟수와 현재 횟수가 같으면,
        if (needCount.Equals(currentCount))
        {
            isEqual = true;
            // 각 슬롯 검사.
            for (int i = 0; i < leftValue.Length; i++)
            {
                if (leftValue[i] != rightValue[i])  // 다르면 break;
                {
                    isEqual = false;
                    break;
                }
            }
        }

        return isEqual;
    }

    private void ResetSlot()
    {
        // 선택 슬롯 초기화.
        for (int i = 0; i< rightDialsSelectedSlot.Length; i++)
        {
            rightDialsSelectedSlot[i].transform.localPosition = new(0, 0, 0);
        }
        // 현재 횟수 0으로 초기화.
        currentCountText.SetText("0");
    }

    private void ClearSignal()
    {
        StageGenerator stageGenerator;
        stageGenerator = FindObjectOfType<StageGenerator>();

        MapGenerator[,] mapGenerator = stageGenerator.GetMapGenerator();

        int x = (int)(transform.position.x / (50 + 1));     // 수정 필요
        int y = (int)(transform.position.y / (50 + 1));

        mapGenerator[x, y].Fields.SetIsClear(true);

        GetComponent<SpriteRenderer>().sprite = right_state;
    }
}
