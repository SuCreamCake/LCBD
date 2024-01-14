using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanSam : Potion_Parts_Item
{
    private void Awake()
    {
        Price = 95; //������ ����
        item_number = 36; //������ ��ȣ
        item_Name = "SanSam"; ; //�������̸�
        Rank = Item_Rank.Unique; //������ ��͵�
        item_type = Item_Type.Potion_Parts; //������ Ÿ��
        effect_type = Effect_Type.unbeatable; //������?
        effect_info = Effect_Info.Null; //������ ����
        effect_active_type = Effect_Active_Type.Once; //ȿ�� ����
        effect_figures = 0; //�󸶳� �����Դ��� ����
        effect_maintain_time = 10; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
        max_count = 99; //�ִ� ��������
    }


    public override void DestraoyAfterTime()
    {

    }

    public override void Use_Effect() //�̷л� ������
    {
        Debug.Log("ü�� ���ǻ��");
        GameObject findPlayer = GameObject.Find("����Player");
        if (findPlayer != null)
        {
            SimplePlayerMove player = findPlayer.GetComponent<SimplePlayerMove>();
            if (player != null)
            {
                player.HP += effect_figures; // ü���� 10(��ĭ)ȸ����Ŵ
                Debug.Log("ü�� 10ȸ��! ��ĭ��.");
            }
        }
    }

    /*public override void Use_Effect()
    {
        Debug.Log("ü�� ���ǻ��");
        GameObject findPlayer = GameObject.Find("����Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.health += effect_figures; // ü���� 10(��ĭ)ȸ����Ŵ
                Debug.Log("ü�� 10ȸ��! ��ĭ��.");
            }
        }
    }*/
}