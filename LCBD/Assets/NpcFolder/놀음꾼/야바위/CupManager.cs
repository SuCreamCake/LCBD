using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupManager : MonoBehaviour
{
    public GameObject Cup1;
    public GameObject Cup2;
    public GameObject Cup3;

    private bool isGameRunning = false;

    // UI 버튼에 연결할 메서드를 만듭니다.
    public void OnStartButtonClicked()
    {
        if (!isGameRunning)
        {
            // 게임이 실행 중이 아닌 경우에만 실행합니다.
            StartCoroutine(StartGame());
        }
    }

    private IEnumerator StartGame()
    {
        isGameRunning = true;
        for (int i = 0; i < 5; i++)
        {
            // 2개의 컵을 랜덤으로 선택하여 배열에 저장합니다.
            GameObject[] chosenCups = GetRandomCups(2);

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

            // Wait for 3 seconds before proceeding to the next iteration.
            yield return new WaitForSeconds(1f);
        }

        // After the loop is finished, enable the CupClickChecker script on the Cup objects.
        EnableCupClickChecker();
    }
    public void GameRunningEnd()
    {
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
}


