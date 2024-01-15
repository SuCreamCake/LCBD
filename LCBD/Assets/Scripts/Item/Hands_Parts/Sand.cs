using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : Hand_Parts_Item
{
    private void Awake()
    {
        Price = 25;
        item_number = 4;
        item_Name = "Sand"; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        drop_age = Drop_age.Child; //ŉ�� ������ ���
        Weapon_Attack = 3;
        Weapon_Range = 0;
        effect_type = Effect_Type.Enhance; //ȿ�� Ÿ��
        effect_info = Effect_Info.Attack_Speed; //ȿ�� ����
        effect_target = Effect_Target.Self; //ȿ�� ���� ���
        effect_figures = 1; //�󸶳� �����Դ��� ����
        effect_active_type = Effect_Active_Type.Infinite; //ȿ�� ����
        effect_maintain_time = 0; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
                                  //item_sprite; //������ �̹��� �̹����� �����
    }
    private void Update()
    {
        GameObject findPlayer = GameObject.Find("����Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.attackSpeed += effect_figures; // ���ݼӵ� 1 ���
                //Debug.Log("���ݼӵ� 1 ���.");
            }
        }
    }
    public override void DestraoyAfterTime() //����� �۾�
    { }

    public override void Use_Effect() //���ȿ��
    { }
}
