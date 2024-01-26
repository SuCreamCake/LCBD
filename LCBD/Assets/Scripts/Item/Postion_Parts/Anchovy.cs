using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchovy : Potion_Parts_Item
{
    
    private void Awake()
    {
        Price = 30; //������ ����
        item_number = 30; //������ ��ȣ
        item_Name = "Anchovy"; ; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        item_type = Item_Type.Potion_Parts; //������ Ÿ��
        effect_info = Effect_Info.Defense; //������ ����
        effect_active_type = Effect_Active_Type.Once; //ȿ�� ����
        effect_figures = 2.0f; //�󸶳� �����Դ��� ����
        effect_maintain_time = 15; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
        max_count = 99; //�ִ� ��������
        now_Count = 0; //���簹��
    }


    public override void DestroyAfterTime()
    {

    }

    public override void Use_Effect() //�̷л� ������
    {
        if (gameObject.activeInHierarchy) // GameObject�� Ȱ��ȭ �������� Ȯ��
        {
            Debug.Log("��ġ");
            StartCoroutine(TemporaryEffect());
        }
    }

    private IEnumerator TemporaryEffect()
    {
        GameObject findPlayer = GameObject.FindWithTag("Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.defense += (int)effect_figures; // ���׹̳� ����
                Debug.Log("���׹̳� 2.0ȸ��.");

                yield return new WaitForSeconds(effect_maintain_time); // 15�� ���

                player.defense -= (int)effect_figures; // ���׹̳� ������ �ǵ�����
                Debug.Log("���׹̳� ȸ�� ȿ�� ����.");
            }
        }
    }
}
