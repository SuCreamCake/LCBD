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

    // 컵 업다운
    private Vector3 originalPos1;
    private Vector3 originalPos2;
    private Vector3 originalPos3;
    private float duration = 1.0f; // 이동에 걸리는 시간 (1초)

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
    
    void Awake()
    {
        firstPos1 = Cup1.GetComponent<RectTransform>().anchoredPosition;
        firstPos2 = Cup2.GetComponent<RectTransform>().anchoredPosition;
        firstPos3 = Cup3.GetComponent<RectTransform>().anchoredPosition;
        firstBallPos = Ball.GetComponent<RectTransform>().anchoredPosition;
    }

    void OnDisable()
    {
        // Cup 오브젝트들의 위치를 초기 위치로 변경합니다.
        Cup1.GetComponent<RectTransform>().anchoredPosition = firstPos1;
        Cup2.GetComponent<RectTransform>().anchoredPosition = firstPos2;
        Cup3.GetComponent<RectTransform>().anchoredPosition = firstPos3;

        // 공의 위치도 초기 위치로 변경합니다.
        Ball.GetComponent<RectTransform>().anchoredPosition = firstBallPos;
        StartButton.interactable = true;
        ResetButton.interactable = false;
    }

    void Start()
    {
        if (first)
        {
            first = false;
        }
        //컵 업다운을 위한 오브젝트 위치 얻기
        originalPos1 = Cup1.transform.position;
        originalPos2 = Cup2.transform.position;
        originalPos3 = Cup3.transform.position;
        originalBallPos = Ball.transform.position;

        ResetGame(); // Start에서 ResetGame 메서드를 호출합니다.
    }

    // UI 버튼에 연결할 메서드를 만듭니다.
    public void OnStartButtonClicked()
    {
        if (!isGameRunning)
        {
            isGameRunning = true;
            // 게임이 실행 중이 아닌 경우에만 실행합니다.
            StartCoroutine(StartGame());
        }
        StartButton.interactable = false;
    }

    public IEnumerator StartGame()
    {
        ExitButton.interactable = false;
        ResetButton.interactable = false;
        StartCoroutine(DownMove());
        // Wait for 1.5 seconds before proceeding to the next iteration.
        yield return new WaitForSeconds(duration);

        for (int i = 0; i < 5; i++)
        {
            // 2개의 컵을 랜덤으로 선택하여 배열에 저장합니다.
            chosenCups = GetRandomCups(2);

            //chosenCups 배열을 Character 스크립트에게 전달합니다.
            PassChosenCupsToCharacters();

            // 선택된 컵을 Log로 출력합니다.
            LogChosenCups(chosenCups);

            // 2개의 선택된 컵의 이름을 교차하여 변경합니다.
            SwapCupNames(chosenCups[0], chosenCups[1]);

            // 변경된 컵 이름을 Log로 출력합니다.
            LogSwappedCupNames(chosenCups);

            // 2개의 컵의 위치를 교차하여 변경합니다.
            SwapCupPositions(chosenCups[0], chosenCups[1]);

            // Ball이 있는 컵을 찾아 Log로 출력합니다.
            FindCupWithBall(chosenCups);

            // Wait for 1.5 seconds before proceeding to the next iteration.
            yield return new WaitForSeconds(1.0f);
        }

        ExitButton.interactable = true;
        // After the loop is finished, enable the CupClickChecker script on the Cup objects.
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
        // 3개의 컵을 배열에 저장합니다.
        GameObject[] cups = new GameObject[] { Cup1, Cup2, Cup3 };

        // 랜덤으로 컵을 선택하기 위해 배열을 섞습니다.
        for (int i = 0; i < cups.Length; i++)
        {
            int randomIndex = Random.Range(i, cups.Length);
            GameObject temp = cups[i];
            cups[i] = cups[randomIndex];
            cups[randomIndex] = temp;
        }

        // count 개수만큼 앞에서부터 선택합니다.
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
            // 컵의 하위 오브젝트를 검사하여 Ball이 있는지 확인합니다.
            bool hasBall = false;
            foreach (Transform child in cup.transform)
            {
                if (child.CompareTag("Ball"))
                {
                    hasBall = true;
                    break;
                }
            }

            // Ball이 있는 컵인 경우 Log로 출력합니다.
            if (hasBall)
            {
                Debug.Log(cup.name + "가 공을 가지고 있다.");
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
        // Call the Reset method on each cup click checker script.
        Cup1.GetComponent<CupClickChecker>().Reset();
        Cup2.GetComponent<CupClickChecker>().Reset();
        Cup3.GetComponent<CupClickChecker>().Reset();
        // Call the Ing() method on each cup object.
        Cup1.GetComponent<CupClickChecker>().Ing();
        Cup2.GetComponent<CupClickChecker>().Ing();
        Cup3.GetComponent<CupClickChecker>().Ing();
    }

    // 이 메서드를 통해 chosenCups 배열을 Character 스크립트에게 전달합니다.
    public void PassChosenCupsToCharacters()
    {
        Character[] characters = FindObjectsOfType<Character>();

        foreach (Character character in characters)
        {
            character.ReceiveChosenCups(chosenCups);
        }
    }

    // 컵 다운
    private IEnumerator DownMove()
    {
        yield return null; // 1 프레임 대기

        Start();
        // 원하는 시간에 따라 보간 함수를 사용하여 오브젝트를 이동시킵니다.
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / duration); // 0과 1 사이의 보간 값

            Cup1.transform.position = Vector3.Lerp(originalPos1, new Vector3(originalPos1.x, originalPos1.y - 50f, originalPos1.z), t);
            Cup2.transform.position = Vector3.Lerp(originalPos2, new Vector3(originalPos2.x, originalPos2.y - 50f, originalPos2.z), t);
            Cup3.transform.position = Vector3.Lerp(originalPos3, new Vector3(originalPos3.x, originalPos3.y - 50f, originalPos3.z), t);

            // 공도 천천히 이동시킵니다.
            Ball.transform.position = Vector3.Lerp(originalBallPos, new Vector3(originalBallPos.x, originalBallPos.y + 50f, originalBallPos.z), t);

            yield return null; // 1 프레임 대기
        }

    }

    // 컵 업
    private IEnumerator UpMove()
    {
        yield return null; // 1 프레임 대기

        originalBallPos = new Vector3(Ball.transform.position.x, Ball.transform.position.y - 50f, Ball.transform.position.z); // 현재 공의 위치 저장
        Vector3 targetBallPos = new Vector3(originalBallPos.x, originalBallPos.y - 50f, originalBallPos.z);


        // 원하는 시간에 따라 보간 함수를 사용하여 오브젝트를 이동시킵니다.
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / duration); // 0과 1 사이의 보간 값

            Cup1.transform.position = Vector3.Lerp(Cup1.transform.position, new Vector3(Cup1.transform.position.x, originalPos1.y, originalPos1.z), t);
            Cup2.transform.position = Vector3.Lerp(Cup2.transform.position, new Vector3(Cup2.transform.position.x, originalPos1.y, originalPos2.z), t);
            Cup3.transform.position = Vector3.Lerp(Cup3.transform.position, new Vector3(Cup3.transform.position.x, originalPos1.y, originalPos3.z), t);

            // 공도 천천히 이동시킵니다. y값만 변경됩니다.
            Ball.transform.position = Vector3.Lerp(Ball.transform.position, originalBallPos, t);

            yield return null; // 1 프레임 대기
        }
    }

    // 게임 상태를 초기화하는 메서드를 추가합니다.
    public void ResetGame()
    {
        // 컵 위치를 초기화합니다.
        Cup1.transform.position = originalPos1;
        Cup2.transform.position = originalPos2;
        Cup3.transform.position = originalPos3;

        // 공 위치를 초기화합니다.
        Ball.transform.position = originalBallPos;

        //// 컵 이름을 초기화합니다.
        //Cup1.name = "Cup1";
        //Cup2.name = "Cup2";
        //Cup3.name = "Cup3";

        // CupClickChecker 스크립트를 초기화합니다.
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
}


