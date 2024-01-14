using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPlane : Hand_Parts_Item
{
    private void Awake()
    {
        Price = 57;
        item_number = 8;
        item_Name = "PaperPlane"; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        drop_age = Drop_age.Young; //ŉ�� ������ ���
        Weapon_Attack = 6; //���ݷ�
        Weapon_Range = 0; //���ݹ���
        effect_type = Effect_Type.Enhance; //ȿ�� Ÿ��
        effect_info = Effect_Info.Attack_Speed; //ȿ�� ����
        effect_target = Effect_Target.Self; //ȿ�� ���� ���
        effect_figures = 2; //�󸶳� �����Դ��� ����
        effect_active_type = Effect_Active_Type.Infinite; //ȿ�� ����
        effect_maintain_time = 0; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
                                  //item_sprite; //������ �̹��� �̹����� �����
    }

    public override void DestraoyAfterTime() //����� �۾�
    { }

    public override void Use_Effect() //���ȿ��
    { }
}

