using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> inventoryItemList;// �÷��̾ ������ �����۸���Ʈ
    private int selectedItem;
    private bool itemActivated = false;
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
        if (inventoryItemList[index].itemType.ToString().Equals("Immediate"))
        {
            if(!itemActivated)
            {
                itemActivated = true;
                UseImmediateItem();
            }
                
        }
        else if (inventoryItemList[index].itemType.ToString().Equals("Ready"))
        {
            Debug.Log("��� �ߵ� ������!!" + itemActivated);
            if (!itemActivated)
            {
                itemActivated = true;
                StartCoroutine(ReadyItemToUse(inventoryItemList[index].waitingTime));
            }
            //������ ��� ���� ���� ��
        }
        else if (inventoryItemList[index].itemType.ToString().Equals("Toggle"))
        {
            Debug.Log("����� ������!!");
        }
        else if(inventoryItemList[index].itemType.ToString().Equals("Throw"))
        {
            Debug.Log("��ô�� ������!!");
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<ThrowItem>();

        }

    }
    public void UseImmediateItem()
    {
        //������ �����
        itemActivated = false;
    }

   
    IEnumerator ReadyItemToUse(float time)
    {
        float isWaitOver = 0f;
        while (isWaitOver <= time)
        {
            isWaitOver += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.I))
            {
                itemActivated = false;
                Debug.Log("�߰� �Է� ���� ���!!");
                yield break;
            }
            if (GameObject.Find("Player").GetComponent<Player>().ReturnIsHeat())
            {
                Debug.Log("�ǰ� �� �̺�Ʈ ���� ���!");
                itemActivated = false;
                yield break;
            }
            Debug.Log(isWaitOver);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        itemActivated = false;
        Debug.Log("��� ������ ���!!");
        yield return null;
    }


}
