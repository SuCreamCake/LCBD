using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBundle : Goods_Item
{
    private void Awake() //�ش������ �ʱⰪ ����
    {
        item_number = 45;
        item_Name = "CoinBundle"; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        effect_type = Effect_Type.Purchace; //ȿ�� Ÿ��
        effect_info = Effect_Info.Money; //ȿ�� ����
        effect_target = Effect_Target.NPC; //ȿ�� ���� ���
        effect_figures = 10; //�󸶳� �����Դ��� ����
        effect_maintain_time = 0; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
        //item_sprite; //������ �̹��� �̹����� �����

        now_Count = 0; //���� ���� ����
    }

    public override void DestroyAfterTime() //����� �۾�
    { }

    public override void Use_Effect() //���ȿ��
    {
        Debug.Log("������ġ");
        GameObject findPlayer = GameObject.FindWithTag("Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.plusMoney((int)effect_figures); // �� ���
                Debug.Log("10�� �߰�");
            }
        }
    }
}
