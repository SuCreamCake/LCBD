using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Potion_Parts_Item
{
    private void Awake()
    {
        Price = 30; //������ ����
        item_number = 31; //������ ��ȣ
        item_Name = "Carrot"; ; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        item_type = Item_Type.Potion_Parts; //������ Ÿ��
        effect_info = Effect_Info.Range; //������ ����
        effect_active_type = Effect_Active_Type.Once; //ȿ�� ����
        effect_figures = 0.3f; //�󸶳� �����Դ��� ����
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
            Debug.Log("���");
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
                player.crossroads += effect_figures; // ���ݻ�Ÿ� ����
                Debug.Log("���ݻ�Ÿ� ����");
            
                yield return new WaitForSeconds(effect_maintain_time); // 15�� ���

                player.crossroads -= (int)effect_figures; // ���׹̳� ������ �ǵ�����
                Debug.Log("���ݻ�Ÿ� ���� ȿ�� ����.");
            }
        }
    }
}

