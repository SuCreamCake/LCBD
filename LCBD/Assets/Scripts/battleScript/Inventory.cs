using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> inventoryItemList;// 플레이어가 소지한 아이템리스트
    private int selectedItem;
    private bool itemActivated;
    private bool stopKeypinput;
    public float getNum1Key;
    public float getNum2key;
    public float getNum3Key;
    private int index = -1;
    // Start is called before the first frame update
    void Start()
    {

        inventoryItemList = ItemDatabaseManager.instance.itemList;
    }

    // Update is called once per frame
    void Update()
    {
        GetKeyDown();
        
    }
    void GetKeyDown()
    {
        
        if (Input.GetButtonDown("Item1"))
        {
            index = 0;
            StartUseItem();
            Debug.Log("1번 인덱스 설정됨!!");
        }
                   
        else if (Input.GetButtonDown("Item2"))
        {
            index = 1;
            StartUseItem();
            Debug.Log("2번 인덱스 설정됨!!");
        }
        else if (Input.GetButtonDown("Item3"))
        {
            index = 2;
            StartUseItem();
            Debug.Log("3번 인덱스 설정됨!!");
        }
           
    }
    void StartUseItem()
    {
        if(inventoryItemList[index].itemType.ToString().Equals("Immediate"))
        {
            Debug.Log("즉시 아이템 발동!");
        }
        else if(inventoryItemList[index].itemType.ToString().Equals("Ready"))
        {
            Debug.Log("대기 발동 아이템!!");
            StartCoroutine(ReadyItemToUse());
           
            //아이템 사용 구현 로직 들어감
            
        }
        else if(inventoryItemList[index].itemType.ToString().Equals("Toggle"))
        {
            Debug.Log("토글형 아이템!!");
        }

    }
    IEnumerator ReadyItemToUse()
    {
        float isWaitOver = 0f;
        while (isWaitOver <= inventoryItemList[index].waitingTime)
        {
            isWaitOver += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("중간 입력 받음 취소!!");
                yield break;
            }
            if (GameObject.Find("Player").GetComponent<Player>().ReturnIsHeat())
            {
                Debug.Log("피격 됨 이벤트 실행 취소!");
                yield break;
            }
            Debug.Log(isWaitOver);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Debug.Log("아이템 사용!!");
        yield return null;
    }



}
