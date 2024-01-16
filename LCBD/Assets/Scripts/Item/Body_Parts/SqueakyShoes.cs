using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueakyShoes : Body_Parts_Item
{
    private void Awake() //�ش������ �ʱⰪ ����
    {
        Price = 25;
        item_number = 20;
        item_Name = "SqueakyShoes"; //�������̸�
        Rank = Item_Rank.Rare; //������ ��͵�
        drop_age = Drop_age.Child; //ŉ�� ������ ���
        effect_type = Effect_Type.Enhance; //ȿ�� Ÿ��
        effect_info = Effect_Info.Speed; //ȿ�� ����
        effect_target = Effect_Target.Self; //ȿ�� ���� ���
        effect_figures = 0.3f; //�󸶳� �����Դ��� ����
        effect_active_type = Effect_Active_Type.Infinite; //ȿ�� ����
        effect_maintain_time = 0; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
        //item_sprite; //������ �̹��� �̹����� �����
    }
    private void Update()
    {
        GameObject findPlayer = GameObject.FindWithTag("Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.addSpeed(effect_figures); // �̵��ӵ� 2 ���
                //Debug.Log("�̵��ӵ� 2 ���.");
            }
        }
    }
    public override void DestroyAfterTime() //����� �۾�
    { }

    public override void Use_Effect() //���ȿ��
    {
        GameObject findPlayer = GameObject.Find("����Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.addSpeed((int)effect_figures); // �̵��ӵ� 2 ���
                //Debug.Log("�̵��ӵ� 2 ���.");
            }
        }
    }
}
