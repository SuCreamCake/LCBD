using TMPro;
using UnityEngine;

public class Gimmick_Mom_Left_Lever : MonoBehaviour, IControlGimmickObject
{
    [SerializeField] private Sprite left_state;     // 왼쪽 이미지 (off).
    [SerializeField] private Sprite right_state;    // 오른쪽 이미지 (on).

    private Transform gimmickObject;       // 전체적인 엄마 기믹 맵 기믹 오브젝트.

    private Transform leftDialsParent;      // 왼쪽 다이얼 기믹 오브젝트들의 부모.
    public GameObject[] LeftDials { get; private set; }     // 왼쪽 다이얼 기믹 오브젝트들.

    private Transform rightDialsParent;     // 오른쪽 다이얼 기믹 오브젝트들의 부모.
    public GameObject[] RightDials { get; private set; }    // 오른쪽 다이얼 기믹 오브젝트들.

    public TextMeshPro NeedCountText { get; private set; }      // (왼쪽) 필요 횟수 디스플레이 오브젝트의 텍스트.
    public TextMeshPro CurrentCountText { get; private set; }   // (오른쪽) 현재 횟수 디스플레이 오브젝트의 텍스트.

    private bool isRunning = false; // 레버가 작동 중인지
    private bool isActive = false;  // 레버가 (기믹오브젝트가) 켜진 상태인지.

    private int needCount = 0;      // 필요 횟수 카운트

    private void Awake()
    {
        // 기믹 오브젝트
        gimmickObject = transform.parent.GetChild(0);

        // 왼쪽 다이얼들
        leftDialsParent = gimmickObject.transform.GetChild(0).GetChild(0);
        LeftDials = new GameObject[4];
        LeftDials[0] = leftDialsParent.GetChild(0).gameObject;
        LeftDials[1] = leftDialsParent.GetChild(1).gameObject;
        LeftDials[2] = leftDialsParent.GetChild(2).gameObject;
        LeftDials[3] = leftDialsParent.GetChild(3).gameObject;

        // 오른쪽 다이얼들
        rightDialsParent = gimmickObject.transform.GetChild(0).GetChild(1);
        RightDials = new GameObject[4];
        RightDials[0] = rightDialsParent.GetChild(0).gameObject;
        RightDials[1] = rightDialsParent.GetChild(1).gameObject;
        RightDials[2] = rightDialsParent.GetChild(2).gameObject;
        RightDials[3] = rightDialsParent.GetChild(3).gameObject;

        NeedCountText = gimmickObject.GetChild(1).GetChild(0).GetComponent<TextMeshPro>();

        CurrentCountText = gimmickObject.GetChild(2).GetChild(0).GetComponent<TextMeshPro>();

        // 스프라이트 모양을 왼쪽 그림으로.
        GetComponent<SpriteRenderer>().sprite = left_state;

        // 기믹 오브젝트 꺼둠. (왼쪽 다이얼 작동 시 켜져야 함).
        gimmickObject.gameObject.SetActive(false);
    }

    public void ControlGimmickObject()
    {
        if (!isRunning)
        {
            PullLever();
        }
    }

    private void PullLever()
    {
        isRunning = true;

        if (!isActive)  // 꺼진 상태이면, 켜기
        {
            // 기믹 오브젝트 켜기
            gimmickObject.gameObject.SetActive(true);

            needCount = 0;
            // 왼쪽 다이얼 상태 랜덤 결정.
            for (int i = 0; i < LeftDials.Length; i++)
            {
                int random = UnityEngine.Random.Range(0, 6);    //3칸 * 2배
                LeftDials[i].transform.GetChild(1).localPosition = new(0, random % 3, 0);
                needCount += random;
            }

            // 필요 횟수 디스플레이에 횟수 띄움.
            NeedCountText.SetText(needCount.ToString());

            // 오른쪽 다이얼 상태 초기화.
            for (int i = 0; i < RightDials.Length; i++)
            {
                RightDials[i].transform.GetChild(1).localPosition = new(0, 0, 0);
            }

            // 현재 횟수 디스플레이에 0으로 띄움. 
            CurrentCountText.SetText("0");


            // 스프라이트 모양을 오른쪽 그림으로.
            GetComponent<SpriteRenderer>().sprite = right_state;

            isActive = true;
        }
        else
        {
            // 기믹 오브젝트 끄기
            gimmickObject.gameObject.SetActive(false);

            // 스프라이트 모양을 왼쪽 그림으로.
            GetComponent<SpriteRenderer>().sprite = left_state;

            isActive = false;
        }

        isRunning = false;
    }
}
