using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body_Parts_Item : Item
{
    public int Price; //����

    private void Awake()
    {
        item_type = Item_Type.Body_Parts; //�� ������ ����; //������ Ÿ��
        max_count = 1; //�� �ִ� ���������� 1����
        effect_target = Effect_Target.Self; //���� ������ �ڱ��ڽ�����
    }
    public override void DestroyAfterTime()
    { }

    public override void Use_Effect() { }
}
