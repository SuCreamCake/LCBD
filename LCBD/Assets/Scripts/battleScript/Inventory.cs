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
        inventoryItemList = new List<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        GetKeyDown();
        
    }
    void GetKeyDown()
    {
        
        if (Input.GetButtonDown("Item1"))
            index = 0;
        if (Input.GetButtonDown("Item2"))
            index = 1;
        if (Input.GetButtonDown("Item3"))
            index = 2;
    }
    void StartUseItem()
    {
        if(inventoryItemList[index].itemType.ToString().Equals("Ready"))
        {
            float isWaitOver = 0f;
            while(isWaitOver <= inventoryItemList[index].waitingTime)
            {
                isWaitOver += Time.deltaTime;
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    Debug.Log("중간 입력 받음 취소!!");
                    return;
                }
                if (GameObject.Find("Player").GetComponent<Player>().ReturnIsHeat())
                {
                    Debug.Log("피격 됨 이벤트 실행 취소!");
                    return;
                }
            }
            //아이템 사용 구현 로직 들어감
            
        }
        else if(inventoryItemList[index].itemType.ToString().Equals("Toggle"))
        {
            
        }

    }
}
