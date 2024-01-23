using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Linq;
using UnityEditor;

public class FlipManager : MonoBehaviour
{
    public CardFlip[] cardArray; // 카드 오브젝트 배열
    public Button ResetButton;
    public Button UseButton;
    public GameObject Window;
    private int CheckforReset;
    private Player PlayerScript;
    List<string>[] ItemList = new List<string>[3]; // 뽑기 아이템 저장
    int stageNum; //스테이지 넘버
    List<string> randomList;
    public Button ExitButton;
    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            ItemList[i] = new List<string>();
        }
        ResetButton.interactable = false;
        string folderPath = "Assets/Scripts/Item/"; // 검색할 폴더 경로를 지정해주세요.
        string[] csFiles = Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories);
        foreach (string csFile in csFiles)
        {
            string[] fileLines = File.ReadAllLines(csFile);
            string fileName = Path.GetFileName(csFile);

            // 파일 내에서 CS파일 찾기
            bool hasRankFunction = false;
            bool isAdded = false;
            foreach (string line in fileLines)
            {
                if (line.Contains(": Body_Parts_Item") || line.Contains(": Hand_Parts_Item") || line.Contains(": Potion_Parts_Item"))
                    hasRankFunction = true;
                if (line.Contains("Drop_age.Baby") || line.Contains(": Potion_Parts_Item"))
                    isAdded = true;
                if (hasRankFunction && isAdded)
                    break;
            }

            if (hasRankFunction && isAdded)
            {
                int isRank = 0;
                foreach (string line in fileLines) // line 변수를 정의하여 문제 해결
                {
                    if (line.Contains("Item_Rank.Common"))
                    {
                        isRank = 1;
                        break;
                    }
                    if (line.Contains("Item_Rank.Rare"))
                    {
                        isRank = 2;
                        break;
                    }
                    if (line.Contains("Item_Rank.Unique"))
                    {
                        isRank = 3;
                        break;
                    }
                }
                    if (isRank == 1)
                        ItemList[0].Add(fileName);
                    if (isRank == 2)
                        ItemList[1].Add(fileName);
                    if (isRank == 3)
                        ItemList[2].Add(fileName);
            }
        }
        RandomListItem();
    }

    public void RandomListItem()
    {
        // ItemList[0]에서 3개 항목을 랜덤으로 선택하여 가져오기
        List<string> selectedItems0 = ItemList[0].OrderBy(x => Guid.NewGuid()).Take(3).ToList();

        // ItemList[1]에서 2개 항목을 랜덤으로 선택하여 가져오기
        List<string> selectedItems1 = ItemList[1].OrderBy(x => Guid.NewGuid()).Take(2).ToList();

        // ItemList[2]에서 1개 항목을 랜덤으로 선택하여 가져오기
        List<string> selectedItems2 = ItemList[2].OrderBy(x => Guid.NewGuid()).Take(1).ToList();

        // 새로운 리스트에 선택된 항목들을 무작위 순서로 추가하기
        randomList = selectedItems0.Concat(selectedItems1).Concat(selectedItems2).OrderBy(x => Guid.NewGuid()).ToList();
        AllReset();
    }
    private void Start()
    {
        // "Player" 태그를 가진 첫 번째 오브젝트를 찾아 스크립트 가져오기
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            PlayerScript = playerObject.GetComponent<Player>();
        }
        if (Window != null)
        {
            Window.SetActive(false);
        }
        

        for (int i = 0; i < cardArray.Length; i++)
        {
            cardArray[i].canClickOff();
        }

        // 스테이지 이름에서 숫자 부분을 추출하여 현재 스테이지 번호를 확인
        String stageString = SceneManager.GetActiveScene().name;
        if (stageString.StartsWith("stage"))
        {
            string numberPart = stageString.Substring(5);
            int.TryParse(numberPart, out stageNum);
        }
        else
            stageNum = 0;
    }

    public void AllClickOff()
    {
        for (int i = 0; i < cardArray.Length; i++)
        {
            cardArray[i].canClickOff();
        }

        CheckforReset++;
        //if (CheckforReset >= cardArray.Length)
        //{
        //    StartCoroutine(ButtonDelay(2f, ResetButton));
        //}
        //else
        //{
        //    UseButton.interactable = true;
        //}

    }

    IEnumerator ButtonDelay(float delay, Button button)
    {
        yield return new WaitForSeconds(delay);
        button.interactable = true;
    }

    public void RotationEvent()
    {
        ExitButton.interactable = true;
        if (CheckforReset >= cardArray.Length)
        {
            ResetButton.interactable = true;
        } else
        {
            UseButton.interactable = true;
        }
    }

    public void AllReset()
    {
        for (int i = 0; i < cardArray.Length; i++)
        {
            cardArray[i].ResetCard(randomList[i]);
        }
        UseButton.interactable = true;
        ResetButton.interactable = false;
        CheckforReset = 0;
    }

    private void OnDisable()
    {
        //AllReset();
    }

    private void OnEnable()
    {
        Start();
    }

    public void UseButtonClick()
    {
        MinusMoney(10);
        for (int i = 0; i < cardArray.Length; i++)
        {
            cardArray[i].canClickOn();
        }
        UseButton.interactable = false;
        ExitButton.interactable = false;
    }

    public void Reward(MonoScript script, Sprite sprite)
    {
        SpawnObjectAtPlayerPosition(script, sprite);
    }

    void SpawnObjectAtPlayerPosition(MonoScript script, Sprite sprite)
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Vector3 playerPosition = playerObject.transform.position;
            Vector3 newPlayerPosition = playerPosition;
            newPlayerPosition.z -= 0.1f;

            GameObject instantiatedObject = new GameObject(); // 새로운 게임 오브젝트 생성
            instantiatedObject.transform.position = newPlayerPosition; // 위치 설정
            instantiatedObject.layer = LayerMask.NameToLayer("monster");

            // 스크립트 추가
            if (script != null)
            {
                instantiatedObject.AddComponent(script.GetClass());
                // SpriteRenderer 컴포넌트 추가
                SpriteRenderer spriteRenderer = instantiatedObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite; // 스크립트 컴포넌트의 sprite 변수에 접근하여 할당

                // BoxCollider2D 컴포넌트 추가
                BoxCollider2D boxCollider = instantiatedObject.AddComponent<BoxCollider2D>();

                // Rigidbody2D 컴포넌트 추가
                Rigidbody2D rigidbody2D = instantiatedObject.AddComponent<Rigidbody2D>();
                rigidbody2D.gravityScale = 1f; // 그래비티 설정
            }
            else
            {
                Debug.LogError("스크립트를 찾을 수 없습니다.");
            }

        }
        else
        {
            Debug.LogError("플레이어 오브젝트를 찾을 수 없습니다. 'Player' 태그를 확인해주세요.");
        }
    }


    public void PlusMoney(int money)
    {
        Debug.Log("성공 Money : " + money);
    }

    public void MinusMoney(int money)
    {
        if (PlayerScript != null)
        {
            if (PlayerScript.GetMoney() >= money)
            {
                PlayerScript.minusMoney(money);
                Debug.Log("차감 Money : " + money);
            }
        }
    }
}
