using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : Goods_Item
{
    private void Awake() //�ش������ �ʱⰪ ����
    {
        item_number = 46;
        item_Name = "Money"; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        effect_type = Effect_Type.Purchace; //ȿ�� Ÿ��
        effect_info = Effect_Info.Money; //ȿ�� ����
        effect_target = Effect_Target.NPC; //ȿ�� ���� ���
        effect_figures = 20; //�󸶳� �����Դ��� ����
        effect_maintain_time = 0; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
        //item_sprite; //������ �̹��� �̹����� �����
    }

    public override void DestraoyAfterTime() //����� �۾�
    { }

    public override void Use_Effect() //���ȿ��
    {
        Debug.Log("����");
        GameObject findPlayer = GameObject.Find("����Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.plusMoney((int)effect_figures); // �� ���
                Debug.Log("20�� �߰�");
            }
        }
    }
}