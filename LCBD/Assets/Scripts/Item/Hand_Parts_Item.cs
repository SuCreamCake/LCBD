using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Parts_Item : Item
{
    public int Weapon_Attack; //������ݷ�
    public int Weapon_Range; //���ݰ����� ��Ÿ�
    public int Price; //����
    public override void DestroyAfterTime()
    { }
    
    public override void Use_Effect() { }

    private void Awake()
    {
        item_type = Item_Type.Hand_Parts; //�� ������ ����; //������ Ÿ��
        max_count=1; //���� �ִ� ���������� 1����
        effect_target = Effect_Target.Self; //����� �ڱ��ڽſ��� ����
    }
}
