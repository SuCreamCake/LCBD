using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee_ID_card : Body_Parts_Item
{
    private void Awake() //�ش������ �ʱⰪ ����
    {
        Price = 70;
        item_number = 23;
        item_Name = "Employee_ID_card"; //�������̸�
        Rank = Item_Rank.Rare; //������ ��͵�
        drop_age = Drop_age.Adult; //ŉ�� ������ ���
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
