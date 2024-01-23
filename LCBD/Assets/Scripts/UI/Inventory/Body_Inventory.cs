using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body_Inventory: MonoBehaviour
{
    public static Body_Inventory instance;
    public BodySlot[] BodySlots;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;

        //�ӽ÷� �ʱ�ȭ�� ����
        BodySlots = new BodySlot[9];

        for(int i = 0; i < BodySlots.Length; ++i)
        {
            BodySlots[i] = new BodySlot();
        }
    }

    private void Update()
    {
        for (int i = 0; i < BodySlots.Length; i++)
        {
            if (BodySlots[i].isUse)
            {
                BodySlots[i].UseItem(); //�������� ���
                BodySlots[i].isUse = false;
            }
        }
    }

    public bool AddItem(string itemName, Sprite itemSprite, Item itemObejct)
    {
        for (int i = 0; i < BodySlots.Length; i++)
        {
            if (!BodySlots[i].isUse)
            {
                BodySlots[i].itemName = itemName;
                BodySlots[i].itemSprite = itemSprite;
                BodySlots[i].isUse = true;
                BodySlots[i].item = itemObejct;
                // ���⿡ Image ������Ʈ�� ������Ʈ�ϴ� �ڵ带 �߰��մϴ�.
                BodySlots[i].BodyParts_image.sprite = itemSprite;
                BodySlots[i].BodyParts_image.enabled = true; // �̹����� Ȱ��ȭ�մϴ�.

                return true; // �������� ���������� �߰�����
            }
        }
        return false; // �κ��丮�� ���� ��
    }

    public string[] GetAllName()
    {
        string[] names = new string[BodySlots.Length];

        for (int i = 0; i < BodySlots.Length; i++)
        {
            names[i] = BodySlots[i].itemName;
        }
        Debug.Log("바디인벤토리 접근"); // 디버그 로그로 names 배열 출력
        return names;
    }

    public void GetDebug()
    {
        Debug.Log("바디인벤토d리 접근");
    }
}
