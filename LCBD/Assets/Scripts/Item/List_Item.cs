using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List_Item : Item
{
    public override void DestraoyAfterTime()
    { }

    public override void Use_Effect() { }

    private void Awake()
    {
        item_type = Item_Type.List; //����Ʈ ������ Ÿ��
        drop_age = Drop_age.All; //������ ŉ�� ������ ���
        Rank = Item_Rank.Unique;
        effect_type = Effect_Type.Null; //�ϱ��� ����� �� Null Ÿ��
        effect_info = Effect_Info.Null; //ȿ�� ����
        effect_target = Effect_Target.Null; //ȿ�� ���� ���
        effect_figures=0; //�󸶳� �����Դ��� ����
        effect_active_type=Effect_Active_Type.Null; //ȿ�� ����
        effect_maintain_time=0; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
    }
}
