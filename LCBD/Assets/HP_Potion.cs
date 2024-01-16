using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Potion : Item
{

    private void Awake()
    {
        item_number = 27; //������ ��ȣ
        item_Name = "HP_Potion";; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        item_type = Item_Type.Potion_Parts; //������ Ÿ��
        drop_age = Drop_age.All; //ŉ�� ������ ���
        effect_type = Effect_Type.Enhance; //������ Ÿ��
        effect_info = Effect_Info.Health; //������ ����
        effect_target = Effect_Target.Self; //ȿ�� ���� ���
        effect_figures = 10; //�󸶳� �����Դ��� ����
        effect_active_type = Effect_Active_Type.Once; //ȿ�� Ȱ�� Ÿ�� ��ȸ������ ���Ӽ�����
        effect_maintain_time=0; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
        max_count = 99; //�ִ� ��������
}


    public override void DestroyAfterTime()
    {
        
    }

    public override void Use_Effect()
    {
        Debug.Log("ü�� ���ǻ��");
        GameObject findPlayer = GameObject.Find("Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.health += effect_figures; // ü���� 10(��ĭ)ȸ����Ŵ
                Debug.Log("ü�� 10ȸ��! ��ĭ��.");
            }
        }
    }

}
