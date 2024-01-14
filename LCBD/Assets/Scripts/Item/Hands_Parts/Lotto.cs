using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lotto : Hand_Parts_Item
{
    private void Awake()
    {
        Price = 68;
        item_number = 11;
        item_Name = "Lotto"; //�������̸�
        Rank = Item_Rank.Unique; //������ ��͵�
        drop_age = Drop_age.Adult; //ŉ�� ������ ���
        Weapon_Attack = 9; //���ݷ�
        Weapon_Range = 0; //���ݹ���
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
