using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyHammer : Hand_Parts_Item
{
    private void Awake() //�ش������ �ʱⰪ ����
    {
        item_number = 1;
        item_Name="ToyHammer"; //�������̸�
        Rank=Item_Rank.Rare; //������ ��͵�
        Weapon_Attack = 5;
        Weapon_Range = 2;
        drop_age =Drop_age.Baby; //ŉ�� ������ ���
        effect_type=Effect_Type.Null; //ȿ�� Ÿ��
        effect_info=Effect_Info.Null; //ȿ�� ����
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