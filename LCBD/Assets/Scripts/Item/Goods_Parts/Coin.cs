using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Goods_Item
{
    private void Awake() //�ش������ �ʱⰪ ����
    {
        item_number = 44;
        item_Name = "Coin"; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        effect_type = Effect_Type.Purchace; //ȿ�� Ÿ��
        effect_info = Effect_Info.Money; //ȿ�� ����
        effect_target = Effect_Target.NPC; //ȿ�� ���� ���
        effect_figures = 1; //�󸶳� �����Դ��� ����
        effect_maintain_time = 0; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
        //item_sprite; //������ �̹��� �̹����� �����
    }

    public override void DestraoyAfterTime() //����� �۾�
    { }

    public override void Use_Effect() //���ȿ��
    { }
}

