using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow_Personality_Control : Item
{
    private void Awake()
    {
        item_number = 52; //������ ��ȣ
        item_Name = "Grow_Personality_Control"; //�������̸�
        Rank = Item_Rank.Unique; //������ ��͵�
        item_type = Item_Type.Status; //������ Ÿ��
        drop_age = Drop_age.All; //ŉ�� ������ ���
        effect_type = Effect_Type.Null; //ȿ�� Ÿ��
        effect_info = Effect_Info.Null; //ȿ�� ����
        effect_target = Effect_Target.Null; //ȿ�� ���� ���
        effect_figures = 0; //�󸶳� �����Դ��� ����
        effect_active_type = Effect_Active_Type.Null; //ȿ�� ����
        effect_maintain_time = 0; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
                                  //item_sprite; //������ �̹���
        max_count = 1; //�ִ� ��������
    }
    public override void DestraoyAfterTime()
    {
        throw new System.NotImplementedException();
    }

    public override void Use_Effect()
    {
        throw new System.NotImplementedException();
    }
}

