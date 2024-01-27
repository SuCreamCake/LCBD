using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int item_number; //������ ��ȣ
    public string item_Name; //�������̸�
    public Item_Rank Rank; //������ ��͵�
    public Item_Type item_type; //������ Ÿ��
    public Drop_age drop_age; //ŉ�� ������ ���
    public Effect_Type effect_type; //ȿ�� Ÿ��
    public Effect_Info effect_info; //ȿ�� ����
    public Effect_Target effect_target; //ȿ�� ���� ���
    public float effect_figures; //�󸶳� �����Դ��� ����
    public Effect_Active_Type effect_active_type; //ȿ�� ����
    public float effect_maintain_time; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
    public Sprite item_sprite; //������ �̹���
    public int max_count; //�ִ� ��������
    public int now_Count; //���� ��������


    public bool isGet = false; //������� Ȯ���ϴ� ����
    public abstract void Use_Effect(); //���ȿ�� �߻�޼ҵ�
    public abstract void DestroyAfterTime(); //��� �� �ı� �߻�޼ҵ�

    public enum Item_Type //������ Ÿ��
    {
        Hand_Parts, //�� ������ ����
        Body_Parts, //�� ������ ����
        Potion_Parts, //�Ҹ��� ����
        List, //����Ʈ��
        Goods, //��ȭ��
        Status //����??
    }

    public enum Item_Rank //������ ��͵�
    {
        Rare, //��͵��
        Common,  //�Ϲݵ��
        Unique //���ϵ��
    }

    public enum Drop_age //���� �� �ִ� �����
    {
        Baby,  //���Ʊ�
        Child, //�Ƶ���
        Young, //û���
        Adult, //���α�
        Old, //����
        All //��ü���� ��Ⱑ��

    }
    public enum Effect_Type //ȿ�� Ÿ��
    {
        Enhance, //������
        unbeatable, //������
        ending_key, //����Ű
        Open_Stage, //���½�������
        Purchace, //���ŷ�
        Null
    }

    public enum Effect_Info //ȿ������ ������ �ٽû��
    {
        Attack_Speed, //���ݼӵ�
        Offense_Power, //���ݷ�
        Infinite, //����ȿ��
        Health, //ü��ȸ��
        Speed, //�̵��ӵ�
        Stamina, //������
        Defense, //����
        Range, //��Ÿ�
        Endurance, //���ε�
        Random, //����
        Money, //��
        Null
    }

    public enum Effect_Target //ȿ����� �������� ����
    {
        Self, //�ڱ��ڽ�
        Object, //������Ʈ
        NPC, //NPC
        Null //���������
    }

    public enum Effect_Active_Type //ȿ������ ����,��Ÿ�� ��
    {
        Infinite, //����ȿ��
        Once, //�ѹ�
        Null //������
    }

    private void OnTriggerEnter2D(Collider2D collision) //�����۰� �浹�� �ߵ�
    {
        if (collision.CompareTag("Player") && !isGet)
        {
            isGet = true; // �������� ����Ǿ��ٰ� ǥ��
            bool wasAdded = GetItem(collision); //������ ���� �Լ�

            // �������� ���������� �߰��ߴٸ�, �ð������� �������� ����ϴ�.
            if (wasAdded)
            {
                HideItem(); // �ð������� ������ �����
            }
            else
            {
                // �������� �߰����� ���ߴٸ�, isGet�� �ٽ� false�� �����մϴ�.
                isGet = false;
            }
        }
    }

    private void HideItem() //������ ������ �����
    {
        // ��: �������� ��Ȱ��ȭ�ϰų�, �������� ī�޶󿡼� �ָ� �̵���Ű�� ��
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
    }

    private bool GetItem(Collider2D player)
    {
        if (this.item_type == Item_Type.Body_Parts) //�ٵ�������
        {
            Debug.Log("�ٵ����� ����");
            if (Body_Inventory.instance.AddItem(item_Name, item_sprite, this)) //�ٵ��κ��丮�� �߰�
            {
                this.Use_Effect();
            }
            else
            {
                Debug.Log("�ٵ������ ������");
                // �κ��丮�� ���� �� �ִٸ�, �޽����� ǥ���ϰų� �ٸ� ������ ����
                return false;
            }
        }

        if (this.item_type == Item_Type.Hand_Parts) //�ڵ�(����)������
        {
           Debug.Log("�ڵ����� ����");
            if (WeaponInventory.instance.AddItem(item_Name, item_sprite, this)) //�ٵ��κ��丮�� �߰�
            {
                this.Use_Effect();
                    //gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("�ڵ������ ������");
                return false;
                // �κ��丮�� ���� �� �ִٸ�, �޽����� ǥ���ϰų� �ٸ� ������ ����
            }
        }

       if (this.item_type == Item_Type.Potion_Parts) //������������
       {
           Debug.Log("���� ����");
           if (ItemInventory.instance.AddItem(item_Name, item_sprite, this)) //�ٵ��κ��丮�� �߰�
           {
                    //Destroy(gameObject); // �������� ������ ����
                    //gameObject.SetActive(false);
           }
           else
           {
                Debug.Log("���Ǿ����� ������");
                return false;
                // �κ��丮�� ���� �� �ִٸ�, �޽����� ǥ���ϰų� �ٸ� ������ ����
           }
        }
        return true;
        // ������ ���� ����
        // ��: �κ��丮�� ������ �߰�, �÷��̾�� ȿ�� ���� ��
    }

    private bool IsInventoryFullForItemType() //�κ��丮�� ��á���� Ȯ���ϴ� �޼ҵ�
    {
        switch (item_type)
        {
            case Item_Type.Body_Parts:
                return Body_Inventory.instance.IsInventoryFull();
            case Item_Type.Hand_Parts:
                return WeaponInventory.instance.IsInventoryFull();
            case Item_Type.Potion_Parts:
                return ItemInventory.instance.IsInventoryFull();
            default:
                return false;
        }
    }

}