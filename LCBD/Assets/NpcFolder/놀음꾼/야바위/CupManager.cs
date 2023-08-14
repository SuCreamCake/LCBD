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
    private float duration = 1f; // 이동에 걸리는 시간 (1초)
    private bool isMoving = false;
    private Vector3 originalBallPos;

    private bool isGameRunning = false;

    public string cupTag = "Cup"; // Cup 오브젝트에 설정할 태그 이름
    public Character characterScript; // Character 스크립트에 접근하기 위한 변수

    public GameObject[] chosenCups;

    void Start()
    {
        //컵 업다운을 위한 오브젝트 위치 얻기
        originalPos1 = Cup1.transform.position;
        originalPos2 = Cup2.transform.position;
        originalPos3 = Cup3.transform.position;
        originalBallPos = Ball.transform.position;
    }

    // UI 버튼에 연결할 메서드를 만듭니다.
    public void OnStartButtonClicked()
    {
        if (!isGameRunning)
        {
            // 게임이 실행 중이 아닌 경우에만 실행합니다.
            StartCoroutine(StartGame());
        }
    }

    public IEnumerator StartGame()
    {
        isGameRunning = true;

        StartCoroutine(UpDownMove());
        // Wait for 1.5 seconds before proceeding to the next iteration.
        yield return new WaitForSeconds(1.5f);

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
            yield return new WaitForSeconds(2.0f);
        }

        // After the loop is finished, enable the CupClickChecker script on the Cup objects.
        EnableCupClickChecker();
    }
    public void GameRunningEnd()
    {
        Start();
        StartCoroutine(UpMove());
        isGameRunning = false;
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

    // 컵 업다운
    private IEnumerator UpDownMove()
    {
        yield return null; // 1 프레임 대기

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
            Ball.transform.position = Vector3.Lerp(originalBallPos, new Vector3(originalBallPos.x, originalPos1.y - 50f, originalBallPos.z), t);

            yield return null; // 1 프레임 대기
        }

        isMoving = false;
    }

    // 컵 업다운
    private IEnumerator UpMove()
    {
        yield return null; // 1 프레임 대기

        // 원하는 시간에 따라 보간 함수를 사용하여 오브젝트를 이동시킵니다.
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / duration); // 0과 1 사이의 보간 값

            Cup1.transform.position = Vector3.Lerp(originalPos1, new Vector3(originalPos1.x, originalPos1.y + 50f, originalPos1.z), t);
            Cup2.transform.position = Vector3.Lerp(originalPos2, new Vector3(originalPos2.x, originalPos2.y + 50f, originalPos2.z), t);
            Cup3.transform.position = Vector3.Lerp(originalPos3, new Vector3(originalPos3.x, originalPos3.y + 50f, originalPos3.z), t);

            // 공도 천천히 이동시킵니다.
            Ball.transform.position = Vector3.Lerp(originalBallPos, new Vector3(originalBallPos.x, originalPos1.y - 80f, originalBallPos.z), t);

            yield return null; // 1 프레임 대기
        }

        isMoving = false;
    }
}


