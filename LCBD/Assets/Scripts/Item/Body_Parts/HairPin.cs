using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairPin : Body_Parts_Item
{
    private void Awake() //�ش������ �ʱⰪ ����
    {
        Price = 30;
        item_number = 19;
        item_Name = "HairPin"; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        drop_age = Drop_age.Child; //ŉ�� ������ ���
        effect_type = Effect_Type.Null; //ȿ�� Ÿ��
        effect_info = Effect_Info.Null; //ȿ�� ����
        effect_target = Effect_Target.Null; //ȿ�� ���� ���
        effect_figures = 0; //�󸶳� �����Դ��� ����
        effect_active_type = Effect_Active_Type.Null; //ȿ�� ����
        effect_maintain_time = 0; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
        //item_sprite; //������ �̹��� �̹����� �����
    }

    public override void DestraoyAfterTime() //����� �۾�
    { }

    public override void Use_Effect() //���ȿ��
    { }
}

