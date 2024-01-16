using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : Hand_Parts_Item
{
    private void Awake()
    {
        Price = 80;
        item_number = 13;
        item_Name = "Wand"; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        drop_age = Drop_age.Old; //ŉ�� ������ ���
        Weapon_Attack = 28; //���ݷ�
        Weapon_Range = 2; //���ݹ���
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
