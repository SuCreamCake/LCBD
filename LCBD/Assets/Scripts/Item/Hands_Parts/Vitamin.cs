using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitamin : Hand_Parts_Item
{
    private void Awake()
    {
        Price = 75;
        item_number = 12;
        item_Name = "Vitamin"; //�������̸�
        Rank = Item_Rank.Rare; //������ ��͵�
        drop_age = Drop_age.Adult; //ŉ�� ������ ���
        Weapon_Attack = 12; //���ݷ�
        Weapon_Range = 0; //���ݹ���
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
