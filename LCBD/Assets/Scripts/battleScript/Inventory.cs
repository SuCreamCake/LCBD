using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> inventoryItemList;// �÷��̾ ������ �����۸���Ʈ
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
            Debug.Log("1�� �ε��� ������!!");
        }
                   
        else if (Input.GetButtonDown("Item2"))
        {
            index = 1;
            StartUseItem();
            Debug.Log("2�� �ε��� ������!!");
        }
        else if (Input.GetButtonDown("Item3"))
        {
            index = 2;
            StartUseItem();
            Debug.Log("3�� �ε��� ������!!");
        }
           
    }
    void StartUseItem()
    {
        if(inventoryItemList[index].itemType.ToString().Equals("Immediate"))
        {
            Debug.Log("��� ������ �ߵ�!");
        }
        else if(inventoryItemList[index].itemType.ToString().Equals("Ready"))
        {
            Debug.Log("��� �ߵ� ������!!");
            StartCoroutine(ReadyItemToUse());
           
            //������ ��� ���� ���� ��
            
        }
        else if(inventoryItemList[index].itemType.ToString().Equals("Toggle"))
        {
            Debug.Log("����� ������!!");
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
                Debug.Log("�߰� �Է� ���� ���!!");
                yield break;
            }
            if (GameObject.Find("Player").GetComponent<Player>().ReturnIsHeat())
            {
                Debug.Log("�ǰ� �� �̺�Ʈ ���� ���!");
                yield break;
            }
            Debug.Log(isWaitOver);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Debug.Log("������ ���!!");
        yield return null;
    }



}
