using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Goods_Item
{
    private void Awake() //�ش������ �ʱⰪ ����
    {
        item_number = 43;
        item_Name = "Key"; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        effect_type = Effect_Type.Open_Stage; //ȿ�� Ÿ��
        effect_info = Effect_Info.Null; //ȿ�� ����
        effect_target = Effect_Target.Object; //ȿ�� ���� ���
        effect_figures = 0; //�󸶳� �����Դ��� ����
        effect_maintain_time = 0; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
        //item_sprite; //������ �̹��� �̹����� �����
    }

    public override void DestraoyAfterTime() //����� �۾�
    { }

    public override void Use_Effect() //���ȿ��
    { }
}
