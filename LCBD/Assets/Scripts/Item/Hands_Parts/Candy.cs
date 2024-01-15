using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : Hand_Parts_Item
{
    private void Awake()
    {
        Price = 40;
        item_number = 3;
        item_Name = "Candy"; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        drop_age = Drop_age.Child; //ŉ�� ������ ���
        Weapon_Attack = 4; //������ݷ�
        Weapon_Range = 0; //���ݹ���
        effect_type = Effect_Type.Enhance; //ȿ�� Ÿ��
        effect_info = Effect_Info.Attack_Speed; //ȿ�� ����
        effect_target = Effect_Target.Self; //ȿ�� ���� ���
        effect_figures = 2; //�󸶳� �����Դ��� ����
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
                player.attackSpeed += effect_figures; // ���ݼӵ� 2 ���
                //Debug.Log("���ݼӵ� 2 ���.");
            }
        }
    }
    public override void DestraoyAfterTime() //����� �۾�
    { }

    public override void Use_Effect() //���ȿ��
    { }
}
