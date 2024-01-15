using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadukEgg : Hand_Parts_Item
{
    private void Awake()
    {
        Price = 85;
        item_number = 16;
        item_Name = "Sand"; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        drop_age = Drop_age.Old; //ŉ�� ������ ���
        Weapon_Attack = 12;
        Weapon_Range = 0;
        effect_type = Effect_Type.Enhance; //ȿ�� Ÿ��
        effect_info = Effect_Info.Attack_Speed; //ȿ�� ����
        effect_target = Effect_Target.Self; //ȿ�� ���� ���
        effect_figures = 3; //�󸶳� �����Դ��� ����
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
                player.attackSpeed += effect_figures; // ���ݼӵ� 3 ���
                //Debug.Log("���ݼӵ� 3 ���.");
            }
        }
    }

    public override void DestraoyAfterTime() //����� �۾�
    { }

    public override void Use_Effect() //���ȿ��
    { }
}
