using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private int item_number; //������ ��ȣ
    private string item_Name; //�������̸�
    private string Rank; //������ ��͵�
    private string item_type; //������ Ÿ��
    private float effect_time; //��� ���� �ð�
    private string drop_age; //ŉ�� ������ ���

    public abstract void Use_Effect(); //���ȿ�� �߻�޼ҵ�
    public abstract void DestraoyAfterTime(); //��� �� �ı� �߻�޼ҵ�

    public enum ItemRarity
    {

    }


}
