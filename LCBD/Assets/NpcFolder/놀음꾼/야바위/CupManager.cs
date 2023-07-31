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

    // UI ��ư�� ������ �޼��带 ����ϴ�.
    public void OnStartButtonClicked()
    {
        if (!isGameRunning)
        {
            // ������ ���� ���� �ƴ� ��쿡�� �����մϴ�.
            StartCoroutine(StartGame());
        }
    }

    private IEnumerator StartGame()
    {
        isGameRunning = true;
        for (int i = 0; i < 5; i++)
        {
            // 2���� ���� �������� �����Ͽ� �迭�� �����մϴ�.
            GameObject[] chosenCups = GetRandomCups(2);

            // ���õ� ���� Log�� ����մϴ�.
            LogChosenCups(chosenCups);

            // 2���� ���õ� ���� �̸��� �����Ͽ� �����մϴ�.
            SwapCupNames(chosenCups[0], chosenCups[1]);

            // ����� �� �̸��� Log�� ����մϴ�.
            LogSwappedCupNames(chosenCups);

            // 2���� ���� ��ġ�� �����Ͽ� �����մϴ�.
            SwapCupPositions(chosenCups[0], chosenCups[1]);

            // Ball�� �ִ� ���� ã�� Log�� ����մϴ�.
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
        // 3���� ���� �迭�� �����մϴ�.
        GameObject[] cups = new GameObject[] { Cup1, Cup2, Cup3 };

        // �������� ���� �����ϱ� ���� �迭�� �����ϴ�.
        for (int i = 0; i < cups.Length; i++)
        {
            int randomIndex = Random.Range(i, cups.Length);
            GameObject temp = cups[i];
            cups[i] = cups[randomIndex];
            cups[randomIndex] = temp;
        }

        // count ������ŭ �տ������� �����մϴ�.
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
            // ���� ���� ������Ʈ�� �˻��Ͽ� Ball�� �ִ��� Ȯ���մϴ�.
            bool hasBall = false;
            foreach (Transform child in cup.transform)
            {
                if (child.CompareTag("Ball"))
                {
                    hasBall = true;
                    break;
                }
            }

            // Ball�� �ִ� ���� ��� Log�� ����մϴ�.
            if (hasBall)
            {
                Debug.Log(cup.name + "�� ���� ������ �ִ�.");
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


