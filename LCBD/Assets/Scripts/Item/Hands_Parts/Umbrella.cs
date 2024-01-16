using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : Hand_Parts_Item
{
    private void Awake()
    {
        Price = 70;
        item_number = 9;
        item_Name = "Umbrella"; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        drop_age = Drop_age.Adult; //ŉ�� ������ ���
        Weapon_Attack = 15; //���ݷ�
        Weapon_Range = 4; //���ݹ���
        effect_type = Effect_Type.Null; //ȿ�� Ÿ��
        effect_info = Effect_Info.Null; //ȿ�� ����
        effect_target = Effect_Target.Null; //ȿ�� ���� ���
        effect_figures = 0; //�󸶳� �����Դ��� ����
        effect_active_type = Effect_Active_Type.Null; //ȿ�� ����
        effect_maintain_time = 0; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
                                  //item_sprite; //������ �̹��� �̹����� �����
    }

    public override void DestroyAfterTime() //����� �۾�
    { }

    public override void Use_Effect() //���ȿ��
    { }
}
