using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldRing : Body_Parts_Item
{
    private void Awake() //�ش������ �ʱⰪ ����
    {
        Price = 85;
        item_number = 25;
        item_Name = "GoldRing"; //�������̸�
        Rank = Item_Rank.Unique; //������ ��͵�
        drop_age = Drop_age.Old; //ŉ�� ������ ���
        effect_type = Effect_Type.Null; //ȿ�� Ÿ��
        effect_info = Effect_Info.Null; //ȿ�� ����
        effect_target = Effect_Target.Null; //ȿ�� ���� ���
        effect_figures = 0; //�󸶳� �����Դ��� ����
        effect_active_type = Effect_Active_Type.Null; //ȿ�� ����
        effect_maintain_time = 0; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
        //item_sprite; //������ �̹��� �̹����� �����
    }
    private void Update()
    {
        Use_Effect();
    }
    public override void DestraoyAfterTime() //����� �۾�
    { }

    public override void Use_Effect() //���ȿ��
    {
        GameObject findPlayer = GameObject.Find("����Player");
        if (findPlayer != null)
        {
            SimplePlayerMove player = findPlayer.GetComponent<SimplePlayerMove>();
            if (player != null)
            {
                player.Attack += (int)effect_figures; // �̵��ӵ� 2 ���
                //Debug.Log("�̵��ӵ� 2 ���.");
            }
        }
    }
}