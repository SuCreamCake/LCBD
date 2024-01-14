using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMushroom : Potion_Parts_Item
{
    private void Awake()
    {
        Price = 0; //������ ����
        item_number = 32; //������ ��ȣ
        item_Name = "GreenMushroom"; ; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        item_type = Item_Type.Potion_Parts; //������ Ÿ��
        effect_info = Effect_Info.Speed; //������ ����
        effect_active_type = Effect_Active_Type.Once; //ȿ�� ����
        effect_figures = 0.4f; //�󸶳� �����Դ��� ����
        effect_maintain_time = 15; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
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
