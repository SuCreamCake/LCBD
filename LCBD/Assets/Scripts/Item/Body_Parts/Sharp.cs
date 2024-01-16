using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharp : Body_Parts_Item
{
    private void Awake() //�ش������ �ʱⰪ ����
    {
        Price = 65;
        item_number = 22;
        item_Name = "Sharp"; //�������̸�
        Rank = Item_Rank.Rare; //������ ��͵�
        drop_age = Drop_age.Young; //ŉ�� ������ ���
        effect_type = Effect_Type.Enhance; //ȿ�� Ÿ��
        effect_info = Effect_Info.Offense_Power; //ȿ�� ����
        effect_target = Effect_Target.Self; //ȿ�� ���� ���
        effect_figures = 2.0f; //�󸶳� �����Դ��� ����
        effect_active_type = Effect_Active_Type.Infinite; //ȿ�� ����
        effect_maintain_time = 0; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
        //item_sprite; //������ �̹��� �̹����� �����
    }

    private void Update()
    {

    }

    public override void DestroyAfterTime() //����� �۾�
    { }

    public override void Use_Effect() //���ȿ��
    {
        GameObject findPlayer = GameObject.FindWithTag("Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.attackPower += (int)effect_figures; // �̵��ӵ� 2 ���
                //Debug.Log("�̵��ӵ� 2 ���.");
            }
        }
    }
}