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

    // �� ���ٿ�
    private Vector3 originalPos1;
    private Vector3 originalPos2;
    private Vector3 originalPos3;
    private float duration = 1f; // �̵��� �ɸ��� �ð� (1��)
    private bool isMoving = false;
    private Vector3 originalBallPos;

    private bool isGameRunning = false;

    public string cupTag = "Cup"; // Cup ������Ʈ�� ������ �±� �̸�
    public Character characterScript; // Character ��ũ��Ʈ�� �����ϱ� ���� ����

    public GameObject[] chosenCups;

    void Start()
    {
        //�� ���ٿ��� ���� ������Ʈ ��ġ ���
        originalPos1 = Cup1.transform.position;
        originalPos2 = Cup2.transform.position;
        originalPos3 = Cup3.transform.position;
        originalBallPos = Ball.transform.position;
    }

    // UI ��ư�� ������ �޼��带 ����ϴ�.
    public void OnStartButtonClicked()
    {
        if (!isGameRunning)
        {
            // ������ ���� ���� �ƴ� ��쿡�� �����մϴ�.
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
            // 2���� ���� �������� �����Ͽ� �迭�� �����մϴ�.
            chosenCups = GetRandomCups(2);

            //chosenCups �迭�� Character ��ũ��Ʈ���� �����մϴ�.
            PassChosenCupsToCharacters();

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

    // �� �޼��带 ���� chosenCups �迭�� Character ��ũ��Ʈ���� �����մϴ�.
    public void PassChosenCupsToCharacters()
    {
        Character[] characters = FindObjectsOfType<Character>();

        foreach (Character character in characters)
        {
            character.ReceiveChosenCups(chosenCups);
        }
    }

    // �� ���ٿ�
    private IEnumerator UpDownMove()
    {
        yield return null; // 1 ������ ���

        // ���ϴ� �ð��� ���� ���� �Լ��� ����Ͽ� ������Ʈ�� �̵���ŵ�ϴ�.
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / duration); // 0�� 1 ������ ���� ��

            Cup1.transform.position = Vector3.Lerp(originalPos1, new Vector3(originalPos1.x, originalPos1.y - 50f, originalPos1.z), t);
            Cup2.transform.position = Vector3.Lerp(originalPos2, new Vector3(originalPos2.x, originalPos2.y - 50f, originalPos2.z), t);
            Cup3.transform.position = Vector3.Lerp(originalPos3, new Vector3(originalPos3.x, originalPos3.y - 50f, originalPos3.z), t);

            // ���� õõ�� �̵���ŵ�ϴ�.
            Ball.transform.position = Vector3.Lerp(originalBallPos, new Vector3(originalBallPos.x, originalPos1.y - 50f, originalBallPos.z), t);

            yield return null; // 1 ������ ���
        }

        isMoving = false;
    }

    // �� ���ٿ�
    private IEnumerator UpMove()
    {
        yield return null; // 1 ������ ���

        // ���ϴ� �ð��� ���� ���� �Լ��� ����Ͽ� ������Ʈ�� �̵���ŵ�ϴ�.
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / duration); // 0�� 1 ������ ���� ��

            Cup1.transform.position = Vector3.Lerp(originalPos1, new Vector3(originalPos1.x, originalPos1.y + 50f, originalPos1.z), t);
            Cup2.transform.position = Vector3.Lerp(originalPos2, new Vector3(originalPos2.x, originalPos2.y + 50f, originalPos2.z), t);
            Cup3.transform.position = Vector3.Lerp(originalPos3, new Vector3(originalPos3.x, originalPos3.y + 50f, originalPos3.z), t);

            // ���� õõ�� �̵���ŵ�ϴ�.
            Ball.transform.position = Vector3.Lerp(originalBallPos, new Vector3(originalBallPos.x, originalPos1.y - 80f, originalBallPos.z), t);

            yield return null; // 1 ������ ���
        }

        isMoving = false;
    }
}


