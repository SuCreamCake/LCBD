using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbalMedicine : Potion_Parts_Item //�Ѿ�
{
    private void Awake()
    {
        Price = 40; //������ ����
        item_number = 29; //������ ��ȣ
        item_Name = "HerbalMedicine"; ; //�������̸� �Ѿ࿵����
        Rank = Item_Rank.Rare; //������ ��͵�
        item_type = Item_Type.Potion_Parts; //������ Ÿ��
        effect_info = Effect_Info.Stamina; //������ ����
        effect_active_type = Effect_Active_Type.Once; //ȿ�� ����
        effect_figures = 3.0f; //�󸶳� �����Դ��� ����
        effect_maintain_time = 15; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
        max_count = 99; //�ִ� ��������
        now_Count = 0; //���� ���� ����
    }


    public override void DestroyAfterTime()
    {

    }

    public override void Use_Effect() //�̷л� ������
    {
        if (gameObject.activeInHierarchy) // GameObject�� Ȱ��ȭ �������� Ȯ��
        {
            Debug.Log("�Ѿ�");
            StartCoroutine(TemporaryEffect());
        }
    }

    private IEnumerator TemporaryEffect()
    {
        Debug.Log("�Ѿ�");
        GameObject findPlayer = GameObject.FindWithTag("Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.endurance += effect_figures; // ���׹̳�(������) ����
                //Debug.Log("���׹̳� 3.0ȸ��.");

                yield return new WaitForSeconds(effect_maintain_time); // 15�� ���

                player.endurance -= (int)effect_figures; // ���׹̳� ������ �ǵ�����
                Debug.Log("���׹̳� ȸ�� ȿ�� ����.");
            }
        }
    }
}
