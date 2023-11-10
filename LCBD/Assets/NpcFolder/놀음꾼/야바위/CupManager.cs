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
    private float duration = 1.0f; // �̵��� �ɸ��� �ð� (1��)

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
        // Cup ������Ʈ���� ��ġ�� �ʱ� ��ġ�� �����մϴ�.
        Cup1.GetComponent<RectTransform>().anchoredPosition = firstPos1;
        Cup2.GetComponent<RectTransform>().anchoredPosition = firstPos2;
        Cup3.GetComponent<RectTransform>().anchoredPosition = firstPos3;

        // ���� ��ġ�� �ʱ� ��ġ�� �����մϴ�.
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
        //�� ���ٿ��� ���� ������Ʈ ��ġ ���
        originalPos1 = Cup1.transform.position;
        originalPos2 = Cup2.transform.position;
        originalPos3 = Cup3.transform.position;
        originalBallPos = Ball.transform.position;

        ResetGame(); // Start���� ResetGame �޼��带 ȣ���մϴ�.
    }

    // UI ��ư�� ������ �޼��带 ����ϴ�.
    public void OnStartButtonClicked()
    {
        //여기에 돈 관련 메소드 작성해야겠지?

        if (!isGameRunning)
        {
            isGameRunning = true;
            // ������ ���� ���� �ƴ� ��쿡�� �����մϴ�.
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
        // Call the Reset method on each cup click checker script.
        Cup1.GetComponent<CupClickChecker>().Reset();
        Cup2.GetComponent<CupClickChecker>().Reset();
        Cup3.GetComponent<CupClickChecker>().Reset();
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

    // �� �ٿ�
    private IEnumerator DownMove()
    {
        yield return null; // 1 ������ ���

        Start();
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
            Ball.transform.position = Vector3.Lerp(originalBallPos, new Vector3(originalBallPos.x, originalBallPos.y + 50f, originalBallPos.z), t);

            yield return null; // 1 ������ ���
        }

    }

    // �� ��
    private IEnumerator UpMove()
    {
        yield return null; // 1 ������ ���

        originalBallPos = new Vector3(Ball.transform.position.x, Ball.transform.position.y - 50f, Ball.transform.position.z); // ���� ���� ��ġ ����
        Vector3 targetBallPos = new Vector3(originalBallPos.x, originalBallPos.y - 50f, originalBallPos.z);


        // ���ϴ� �ð��� ���� ���� �Լ��� ����Ͽ� ������Ʈ�� �̵���ŵ�ϴ�.
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / duration); // 0�� 1 ������ ���� ��

            Cup1.transform.position = Vector3.Lerp(Cup1.transform.position, new Vector3(Cup1.transform.position.x, originalPos1.y, originalPos1.z), t);
            Cup2.transform.position = Vector3.Lerp(Cup2.transform.position, new Vector3(Cup2.transform.position.x, originalPos1.y, originalPos2.z), t);
            Cup3.transform.position = Vector3.Lerp(Cup3.transform.position, new Vector3(Cup3.transform.position.x, originalPos1.y, originalPos3.z), t);

            // ���� õõ�� �̵���ŵ�ϴ�. y���� ����˴ϴ�.
            Ball.transform.position = Vector3.Lerp(Ball.transform.position, originalBallPos, t);

            yield return null; // 1 ������ ���
        }
    }

    // ���� ���¸� �ʱ�ȭ�ϴ� �޼��带 �߰��մϴ�.
    public void ResetGame()
    {
        // �� ��ġ�� �ʱ�ȭ�մϴ�.
        Cup1.transform.position = originalPos1;
        Cup2.transform.position = originalPos2;
        Cup3.transform.position = originalPos3;

        // �� ��ġ�� �ʱ�ȭ�մϴ�.
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
}


