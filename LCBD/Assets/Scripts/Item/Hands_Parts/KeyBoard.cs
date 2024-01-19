using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoard : Hand_Parts_Item
{
    private void Awake()
    {
        Price = 75;
        item_number = 10;
        item_Name = "Keyboard"; //�������̸�
        Rank = Item_Rank.Unique; //������ ��͵�
        drop_age = Drop_age.Adult; //ŉ�� ������ ���
        Weapon_Attack = 21; //���ݷ�
        Weapon_Range = 2; //���ݹ���
        effect_type = Effect_Type.Null; //ȿ�� Ÿ��
        effect_info = Effect_Info.Null; //ȿ�� ����
        effect_target = Effect_Target.Null; //ȿ�� ���� ���
        effect_figures = 0; //�󸶳� �����Դ��� ����
        effect_active_type = Effect_Active_Type.Null; //ȿ�� ����
        effect_maintain_time = 0; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
                                  //item_sprite; //������ �̹��� �̹����� �����

        now_Count = 0; //���� ���� ����
    }

    public override void DestroyAfterTime() //����� �۾�
    { }

    public override void Use_Effect() //���ȿ��
    { }
}
