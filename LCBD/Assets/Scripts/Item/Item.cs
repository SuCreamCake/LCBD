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


    public abstract void Use_Effect(); //���ȿ�� �߻�޼ҵ�
    public abstract void DestraoyAfterTime(); //��� �� �ı� �߻�޼ҵ�

    public enum Item_Type //������ Ÿ��
    {
        Hand_Parts, //�� ������ ����
        Body_Parts, //�� ������ ����
        Potion_Parts, //�Ҹ��� ����
        List, //����Ʈ��
        Goods //��ȭ��
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ItemInventory.instance.AddItem(item_Name, item_sprite, this))
            {
                //Destroy(gameObject); // �������� ������ ����
                gameObject.SetActive(false);
            }
            else
            {
                // �κ��丮�� ���� �� �ִٸ�, �޽����� ǥ���ϰų� �ٸ� ������ ����
            }
        }
    }
}
