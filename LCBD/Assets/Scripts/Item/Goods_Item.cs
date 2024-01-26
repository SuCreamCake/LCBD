using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goods_Item : Item
{
    public override void DestroyAfterTime()
    { }

    public override void Use_Effect() { }

    private void Awake()
    {
        item_type = Item_Type.Goods; //����Ʈ ������ Ÿ��
        drop_age = Drop_age.All; //������ ŉ�� ������ ���
        effect_type = Effect_Type.Purchace; //�ϱ��� ����� �� Null Ÿ��
        effect_info = Effect_Info.Money; //ȿ�� ����
        effect_target = Effect_Target.NPC; //ȿ�� ���� ���
        effect_figures = 0; //�󸶳� �����Դ��� ����
        effect_active_type = Effect_Active_Type.Once; //ȿ�� ����
        effect_maintain_time = 0; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
    }
}
