using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion_Parts_Item : Item
{
    public override void DestraoyAfterTime()
    { }

    public override void Use_Effect() { }

    private void Awake()
    {
        item_type = Item_Type.Potion_Parts; //�� ������ ����; //������ Ÿ��
        max_count = 99; //���Ƿ� �������� �ִ� �������� 99��
        effect_target = Effect_Target.Self; //���Ƿ��� ������ �ڱ��ڽ�����
        effect_active_type = Effect_Active_Type.Once; //�Ҹ� Ÿ��
        effect_type = Effect_Type.Enhance; //�����ϸ� ������ ����� �������� �������̵�
        drop_age = Drop_age.All; //�����ɿ��� ���� �� ����
    }
}
