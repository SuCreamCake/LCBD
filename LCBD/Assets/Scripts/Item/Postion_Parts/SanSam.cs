using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanSam : Potion_Parts_Item
{
    private void Awake()
    {
        Price = 95; //������ ����
        item_number = 36; //������ ��ȣ
        item_Name = "SanSam"; ; //�������̸�
        Rank = Item_Rank.Unique; //������ ��͵�
        item_type = Item_Type.Potion_Parts; //������ Ÿ��
        effect_type = Effect_Type.unbeatable; //������?
        effect_info = Effect_Info.Null; //������ ����
        effect_active_type = Effect_Active_Type.Once; //ȿ�� ����
        effect_figures = 0; //�󸶳� �����Դ��� ����
        effect_maintain_time = 10; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
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
            Debug.Log("��� ���");
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
                //player.HP += effect_figures; // ���ٰ� ����
                Debug.Log("�������.");
            
                yield return new WaitForSeconds(effect_maintain_time); // 15�� ���

                //player.HP -= (int)effect_figures; // ���׹̳� ������ �ǵ�����
                Debug.Log("��� ȿ�� ����.");
            }
        }
    }
}