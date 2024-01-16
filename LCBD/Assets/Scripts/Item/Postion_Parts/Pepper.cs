using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pepper : Potion_Parts_Item
{
    private void Awake()
    {
        Price = 30; //������ ����
        item_number = 28; //������ ��ȣ
        item_Name = "Pepper"; ; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        item_type = Item_Type.Potion_Parts; //������ Ÿ��
        effect_info = Effect_Info.Offense_Power; //������ ����
        effect_active_type = Effect_Active_Type.Once; //ȿ�� ����
        effect_figures = 1; //�󸶳� �����Դ��� ����
        effect_maintain_time = 15; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
        max_count = 99; //�ִ� ��������
    }


    public override void DestroyAfterTime()
    {

    }

    public override void Use_Effect() //�̷л� ������
    {
        Debug.Log("û����� ���");
        GameObject findPlayer = GameObject.Find("����Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.attackPower += (int)effect_figures; // ���ݷ�����
                Debug.Log("���ݷ� ����.");
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
