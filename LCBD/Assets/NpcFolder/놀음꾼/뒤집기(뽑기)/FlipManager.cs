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

    // 클래스 내부에 멤버 변수로 선언될 전역 변수들
    GameObject[] prefabs1;
    GameObject[] prefabs2;
    GameObject[] prefabs3;

    public void LoadResources(GameObject[] prefabs)
    {
        // 프리팹의 이름을 디버그 로그에 작성합니다.
        foreach (GameObject prefab in prefabs)
        {
            // 프리팹 컴포넌트에서 스크립트를 가져옵니다.
            MonoBehaviour[] scripts = prefab.GetComponents<Item>();

            // 각 스크립트에 대해 isRank 변수를 0으로 초기화합니다.
            int isRank = 0;

            // 스크립트에 해당 랭크 단어가 포함되어 있는지 확인합니다.
            foreach (Item script in scripts)
            {
                Debug.Log(script.Rank + script.name);

                if (script.Rank.ToString().Contains("Common"))
                {
                    isRank = 1;
                    break;
                }
                if (script.Rank.ToString().Contains("Rare"))
                {
                    isRank = 2;
                    break;
                }
                if (script.Rank.ToString().Contains("Unique"))
                {
                    isRank = 3;
                    break;
                }
            }

            // 해당 프리팹의 랭크에 따라 ItemList에 파일 이름을 추가합니다.
            string fileName = prefab.name; // 파일 이름 예시
            if (isRank == 1)
            {
                ItemList[0].Add(fileName);
            }
            else if (isRank == 2)
            {
                ItemList[1].Add(fileName);
            }
            else if (isRank == 3)
            {
                ItemList[2].Add(fileName);
            }

            // 해당 프리팹의 랭크를 출력합니다.
            Debug.Log("프리팹 " + prefab.name + "의 랭크: " + isRank);
        }
    }

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            ItemList[i] = new List<string>();
        }
        ResetButton.interactable = false;
        prefabs1 = null;
        prefabs2 = null;
        prefabs3 = null;
        // 리소스 폴더의 모든 프리팹을 로드합니다.
        prefabs1 = Resources.LoadAll<GameObject>("ItemPrefab/Body_Parts_Prefab");
        // 리소스 폴더의 모든 프리팹을 로드합니다.
        prefabs2 = Resources.LoadAll<GameObject>("ItemPrefab/Hands_Parts_Prefab");
        // 리소스 폴더의 모든 프리팹을 로드합니다.
        prefabs3 = Resources.LoadAll<GameObject>("ItemPrefab/Postion_Parts_Prefab");
        LoadResources(prefabs1);
        LoadResources(prefabs2);
        LoadResources(prefabs3);
       
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
            string randomName = randomList[i]; // RandomList의 요소의 이름을 가져옵니다

            GameObject matchingPrefab = null;

            // prefab1 배열에서 이름이 같은 GameObject를 찾습니다
            matchingPrefab = System.Array.Find(prefabs1, obj => obj.name == randomName);

            if (matchingPrefab == null)
            {
                // prefab2 배열에서 이름이 같은 GameObject를 찾습니다
                matchingPrefab = System.Array.Find(prefabs2, obj => obj.name == randomName);
            }

            if (matchingPrefab == null)
            {
                // prefab3 배열에서 이름이 같은 GameObject를 찾습니다
                matchingPrefab = System.Array.Find(prefabs3, obj => obj.name == randomName);
            }

            if (matchingPrefab != null)
            {
                cardArray[i].ResetCard(matchingPrefab);
            }
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

    public void Reward(GameObject RewardObject)
    {
        SpawnObjectAtPlayerPosition(RewardObject);
    }

    void SpawnObjectAtPlayerPosition(GameObject RewardObject)
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Vector3 playerPosition = playerObject.transform.position;
            Vector3 newPlayerPosition = playerPosition;
            newPlayerPosition.z -= 0.1f;

            GameObject instantiatedObject = Instantiate(RewardObject); // 새로운 게임 오브젝트 생성
            instantiatedObject.transform.position = newPlayerPosition; // 위치 설정
            instantiatedObject.layer = LayerMask.NameToLayer("monster");

            //// 스크립트 추가
            //if (script != null)
            //{
            //    instantiatedObject.AddComponent(script.GetClass());
            //    // SpriteRenderer 컴포넌트 추가
            //    SpriteRenderer spriteRenderer = instantiatedObject.AddComponent<SpriteRenderer>();
            //    spriteRenderer.sprite = sprite; // 스크립트 컴포넌트의 sprite 변수에 접근하여 할당

            //    // BoxCollider2D 컴포넌트 추가
            //    BoxCollider2D boxCollider = instantiatedObject.AddComponent<BoxCollider2D>();

            //    // Rigidbody2D 컴포넌트 추가
            //    Rigidbody2D rigidbody2D = instantiatedObject.AddComponent<Rigidbody2D>();
            //    rigidbody2D.gravityScale = 1f; // 그래비티 설정
            //}
            //else
            //{
            //    Debug.LogError("스크립트를 찾을 수 없습니다.");
            //}

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
