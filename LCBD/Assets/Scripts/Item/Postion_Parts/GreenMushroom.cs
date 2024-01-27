using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMushroom : Potion_Parts_Item
{
    private void Awake()
    {
        Price = 35; //������ ����
        item_number = 33; //������ ��ȣ
        item_Name = "GreenMushroom"; ; //�������̸�
        Rank = Item_Rank.Common; //������ ��͵�
        item_type = Item_Type.Potion_Parts; //������ Ÿ��
        effect_info = Effect_Info.Speed; //������ ����
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
            Debug.Log("�ʷϹ���");
            StartCoroutine(TemporaryEffect());
        }
    }

    private IEnumerator TemporaryEffect()
    {
        GameObject findPlayer = GameObject.Find("����Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                player.addSpeed((int)effect_figures); // �̼� 0.3 ����
                Debug.Log("�̵��ӵ� 0.3 ����.");
            

                yield return new WaitForSeconds(effect_maintain_time); // 15�� ���

                player.subSpeed((int)effect_figures); // ���׹̳� ������ �ǵ�����
                Debug.Log("�̵��ӵ� ���� ȿ�� ����.");
            }
        }

    }


}
