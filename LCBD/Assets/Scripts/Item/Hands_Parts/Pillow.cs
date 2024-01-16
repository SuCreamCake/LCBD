using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : Hand_Parts_Item
{
    private void Awake() //�ش������ �ʱⰪ ����
    {
        item_number = 2;
        item_Name = "Pillow"; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        Weapon_Attack = 7;
        Weapon_Range = 1;
        drop_age = Drop_age.Baby; //ŉ�� ������ ���
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
