using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupManager : MonoBehaviour
{
    public GameObject Cup1;
    public GameObject Cup2;
    public GameObject Cup3;
    public GameObject Ball;

    private Vector3 originalPos1;
    private Vector3 originalPos2;
    private Vector3 originalPos3;
    private float duration = 1.0f; // 이동하는데 걸리는 시간 (1초)
    public float rotationSpeed = 3.75f;

    private Vector3 originalBallPos;
    private Vector2 firstPos1;
    private Vector2 firstPos2;
    private Vector2 firstPos3;
    private Vector2 firstBallPos;

    private bool isGameRunning = false;

    private bool first = true;

    private GameObject[] chosenCups;

    public Button StartButton;
    public Button ResetButton;
    public Button ExitButton;

    public GameObject Window;

    private Player PlayerScript;

    void Awake()
    {
        firstPos1 = Cup1.GetComponent<RectTransform>().anchoredPosition;
        firstPos2 = Cup2.GetComponent<RectTransform>().anchoredPosition;
        firstPos3 = Cup3.GetComponent<RectTransform>().anchoredPosition;
        firstBallPos = Ball.GetComponent<RectTransform>().anchoredPosition;
    }

    void OnDisable()
    {
        // Cup 오브젝트의 위치를 초기 위치로 되돌립니다.
        Cup1.GetComponent<RectTransform>().anchoredPosition = firstPos1;
        Cup2.GetComponent<RectTransform>().anchoredPosition = firstPos2;
        Cup3.GetComponent<RectTransform>().anchoredPosition = firstPos3;

        // 공의 위치를 초기 위치로 되돌립니다.
        Ball.GetComponent<RectTransform>().anchoredPosition = firstBallPos;
        StartButton.interactable = true;
        ResetButton.interactable = false;
    }

    void Start()
    {
        if (Window != null)
        {
            Window.SetActive(false);
        }

        if (first)
        {
            first = false;
        }
        //컵 오브젝트의 초기 위치 저장
        originalPos1 = Cup1.transform.position;
        originalPos2 = Cup2.transform.position;
        originalPos3 = Cup3.transform.position;
        originalBallPos = Ball.transform.position;

        ResetGame(); // Start에서 ResetGame 함수를 호출합니다.

        // "Player" 태그를 가진 첫 번째 오브젝트를 찾아 스크립트 가져오기
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            PlayerScript = playerObject.GetComponent<Player>();
        }
        else
        {
            Debug.Log("Player 태그를 가진 오브젝트를 찾을 수 없습니다.");
        }
    }

    // UI 버튼을 처리하는 메소드를 정의합니다.
    public void OnStartButtonClicked()
    {
        MinusMoney(10);

        if (!isGameRunning)
        {
            isGameRunning = true;
            // 게임이 실행 중이 아니라면 실행합니다.
            StartCoroutine(StartGame());
        }
        StartButton.interactable = false;
    }

    public IEnumerator StartGame()
    {
        Cup1.GetComponent<Character>().ChangerotationSpeed(rotationSpeed);
        Cup2.GetComponent<Character>().ChangerotationSpeed(rotationSpeed);
        Cup3.GetComponent<Character>().ChangerotationSpeed(rotationSpeed);
        float RotationWait = 180f / (rotationSpeed * 60.0f);
        ExitButton.interactable = false;
        ResetButton.interactable = false;
        StartCoroutine(DownMove());
        // 1.5초 동안 대기합니다.
        yield return new WaitForSeconds(duration);

        for (int i = 0; i < 5; i++)
        {
            // 2개의 랜덤한 컵을 선택합니다.
            chosenCups = GetRandomCups(2);

            //chosenCups 배열을 Character 컴포넌트로 전달합니다.
            PassChosenCupsToCharacters();

            // 선택한 컵을 로그로 출력합니다.
            LogChosenCups(chosenCups);

            // 2개의 컵 이름을 교환합니다.
            SwapCupNames(chosenCups[0], chosenCups[1]);

            // 교환된 컵 이름을 로그로 출력합니다.
            LogSwappedCupNames(chosenCups);

            // 2개의 컵 위치를 교환합니다.
            SwapCupPositions(chosenCups[0], chosenCups[1]);

            // 공이 있는 컵을 찾아 로그로 출력합니다.
            FindCupWithBall(chosenCups);

            // 1.0초 동안 대기합니다.
            yield return new WaitForSeconds(RotationWait + 0.3f);
        }

        ExitButton.interactable = true;
        // 루프가 끝난 후, CupClickChecker 스크립트를 활성화합니다.
        EnableCupClickChecker();
    }
    public void GameRunningEnd()
    {
        StartCoroutine(UpMove());
        isGameRunning = false;
        ResetButton.interactable = true;
    }

    private GameObject[] GetRandomCups(int count)
    {
        // 3개의 컵을 담은 배열을 생성합니다.
        GameObject[] cups = new GameObject[] { Cup1, Cup2, Cup3 };

        // 랜덤하게 컵을 섞습니다.
        for (int i = 0; i < cups.Length; i++)
        {
            int randomIndex = Random.Range(i, cups.Length);
            GameObject temp = cups[i];
            cups[i] = cups[randomIndex];
            cups[randomIndex] = temp;
        }

        // count 개수만큼의 컵을 선택하여 새로운 배열을 생성합니다.
        GameObject[] chosenCups = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            chosenCups[i] = cups[i];
        }

        return chosenCups;
    }

    private void LogChosenCups(GameObject[] chosenCups)
    {
        string cupsInfo = "Chosen Cups: ";
        foreach (GameObject cup in chosenCups)
        {
            cupsInfo += cup.name + " ";
        }
        Debug.Log(cupsInfo);
    }

    private void FindCupWithBall(GameObject[] cups)
    {
        foreach (GameObject cup in cups)
        {
            // 선택한 컵에서 공을 찾아봅니다.
            bool hasBall = false;
            foreach (Transform child in cup.transform)
            {
                if (child.CompareTag("Ball"))
                {
                    hasBall = true;
                    break;
                }
            }

            // 공을 찾은 컵을 로그로 출력합니다.
            if (hasBall)
            {
                Debug.Log(cup.name + "에 공이 있습니다.");
            }
        }
    }

    private void SwapCupNames(GameObject cup1, GameObject cup2)
    {
        string tempName = cup1.name;
        cup1.name = cup2.name;
        cup2.name = tempName;
    }

    private void LogSwappedCupNames(GameObject[] chosenCups)
    {
        //string swappedCupsInfo = "Swapped Cup Names: ";
        //foreach (GameObject cup in chosenCups)
        //{
        //    swappedCupsInfo += cup.name + " ";
        //}
        //Debug.Log(swappedCupsInfo);
    }

    private void SwapCupPositions(GameObject cup1, GameObject cup2)
    {
        Vector3 tempPosition = cup1.transform.position;
        cup1.transform.position = cup2.transform.position;
        cup2.transform.position = tempPosition;
    }

    private void EnableCupClickChecker()
    {
        // 각 컵의 CupClickChecker 스크립트의 Reset 메소드를 호출합니다.
        Cup1.GetComponent<CupClickChecker>().Reset();
        Cup2.GetComponent<CupClickChecker>().Reset();
        Cup3.GetComponent<CupClickChecker>().Reset();
        // 각 컵 객체의 Ing() 메소드를 호출합니다.
        Cup1.GetComponent<CupClickChecker>().Ing();
        Cup2.GetComponent<CupClickChecker>().Ing();
        Cup3.GetComponent<CupClickChecker>().Ing();
    }

    // 선택한 컵 배열을 Character 컴포넌트로 전달하는 메소드입니다.
    public void PassChosenCupsToCharacters()
    {
        Character[] characters = FindObjectsOfType<Character>();

        foreach (Character character in characters)
        {
            character.ReceiveChosenCups(chosenCups);
        }
    }

    // 아래로 이동하는 애니메이션
    private IEnumerator DownMove()
    {
        yield return null;  // 1 프레임 대기

        Start();
        // 지정한 시간 동안 애니메이션을 실행합니다.
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / duration);

            Cup1.transform.position = Vector3.Lerp(originalPos1, new Vector3(originalPos1.x, originalPos1.y - 50f, originalPos1.z), t);
            Cup2.transform.position = Vector3.Lerp(originalPos2, new Vector3(originalPos2.x, originalPos2.y - 50f, originalPos2.z), t);
            Cup3.transform.position = Vector3.Lerp(originalPos3, new Vector3(originalPos3.x, originalPos3.y - 50f, originalPos3.z), t);

            // 공의 이동도 실행합니다.
            Ball.transform.position = Vector3.Lerp(originalBallPos, new Vector3(originalBallPos.x, originalBallPos.y + 50f, originalBallPos.z), t);

            yield return null; // 1 프레임 대기
        }

    }

    // 위로 이동하는 애니메이션
    private IEnumerator UpMove()
    {
        yield return null;

        originalBallPos = new Vector3(Ball.transform.position.x, Ball.transform.position.y - 50f, Ball.transform.position.z);
        Vector3 targetBallPos = new Vector3(originalBallPos.x, originalBallPos.y - 50f, originalBallPos.z);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / duration);

            Cup1.transform.position = Vector3.Lerp(Cup1.transform.position, new Vector3(Cup1.transform.position.x, originalPos1.y, originalPos1.z), t);
            Cup2.transform.position = Vector3.Lerp(Cup2.transform.position, new Vector3(Cup2.transform.position.x, originalPos1.y, originalPos2.z), t);
            Cup3.transform.position = Vector3.Lerp(Cup3.transform.position, new Vector3(Cup3.transform.position.x, originalPos1.y, originalPos3.z), t);

            // 공의 이동도 실행합니다.
            Ball.transform.position = Vector3.Lerp(Ball.transform.position, originalBallPos, t);

            yield return null;
        }
    }

    // 게임 초기화 메소드
    public void ResetGame()
    {
        // Cup 오브젝트의 위치를 초기 위치로 되돌립니다.
        Cup1.transform.position = originalPos1;
        Cup2.transform.position = originalPos2;
        Cup3.transform.position = originalPos3;

        // 공의 위치를 초기 위치로 되돌립니다.
        Ball.transform.position = originalBallPos;

        //// �� �̸��� �ʱ�ȭ�մϴ�.
        //Cup1.name = "Cup1";
        //Cup2.name = "Cup2";
        //Cup3.name = "Cup3";

        // CupClickChecker ��ũ��Ʈ�� �ʱ�ȭ�մϴ�.
        Cup1.GetComponent<CupClickChecker>().Reset();
        Cup2.GetComponent<CupClickChecker>().Reset();
        Cup3.GetComponent<CupClickChecker>().Reset();

        isGameRunning = false;
    }

    public void onResetButton()
    {
        Start();
        ResetButton.interactable = false;
        StartButton.interactable = true;
    }

    public void PlusMoney(int money)
    {
        if (PlayerScript != null)
        {
            PlayerScript.plusMoney(money);
            Debug.Log("성공 Money : " + money);
        }
    }

    public void MinusMoney(int money)
    {
        if (PlayerScript != null)
        {
            if (PlayerScript.GetMoney >= money)
            {
                PlayerScript.minusMoney(money);
                Debug.Log("차감 Money : " + money);
            }
        }
    }
}


