using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BlackMushroom : Potion_Parts_Item
{
    private void Awake()
    {
        Price = 55; //������ ����
        item_number = 35; //������ ��ȣ
        item_Name = "BlackMushroom"; ; //�������̸�
        Rank = Item_Rank.Rare; //������ ��͵�
        item_type = Item_Type.Potion_Parts; //������ Ÿ��
        effect_info = Effect_Info.Random; //������ ����
        effect_active_type = Effect_Active_Type.Once; //ȿ�� ����
        effect_figures = 0; //�󸶳� �����Դ��� ����
        effect_maintain_time = 15; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
        max_count = 99; //�ִ� ��������
        now_Count = 0; //���� ���� ����
    }

    // �������� ���õ� �Ӽ� ����Ʈ
    Effect_Info[] effects = new Effect_Info[] {
            Effect_Info.Attack_Speed,
            Effect_Info.Offense_Power,
            Effect_Info.Health,
            Effect_Info.Speed,
            Effect_Info.Stamina,
            Effect_Info.Defense,
            Effect_Info.Range,
            Effect_Info.Endurance
        };


    public override void DestroyAfterTime()
    {

    }

    public override void Use_Effect() //�̷л� ������
    {
        Debug.Log("��������");
        GameObject findPlayer = GameObject.FindWithTag("Player");
        if (findPlayer != null)
        {
            Player player = findPlayer.GetComponent<Player>();
            if (player != null)
            {
                ApplyRandomEffect(player);
                Debug.Log("�����ҷ�.");
            }
        }
    }
    private void ApplyRandomEffect(Player player)
    {
       // SimplePlayerMove Splayer = player.GetComponent<SimplePlayerMove>();
        if (gameObject.activeInHierarchy) // GameObject�� Ȱ��ȭ �������� Ȯ��
        {
            Debug.Log("��������");
            StartCoroutine(TemporaryEffect(player));
        }

    }


    private IEnumerator TemporaryEffect(Player player)
    {
        System.Random random = new System.Random();
        int effectIndex = random.Next(effects.Length); // ������ �ε��� ����
        Effect_Info randomEffect = effects[effectIndex];


        // ���õ� ���� ȿ�� ����
        switch (randomEffect)
        {
            case Effect_Info.Attack_Speed:
                player.attackSpeed += 1; // ���ݼӵ� ����
                Debug.Log("���ݼӵ� 1����");
                break;
            case Effect_Info.Offense_Power:
                player.attackPower += 1; // ���ݷ� ����
                Debug.Log("���ݷ� 1����");
                break;
            case Effect_Info.Health:
                player.health += 1; // ü�� ����
                Debug.Log("ü�� 1����");
                break;
            case Effect_Info.Speed:
                player.addSpeed(1); // �̵��ӵ� ����
                Debug.Log("�̵��ӵ� 1����");
                break;
            case Effect_Info.Stamina:
                player.endurance += 1; // ���¹̳� ����
                Debug.Log("���׹̳� 1����");
                break;
            case Effect_Info.Defense:
                player.defense += 1; // ���� ����
                Debug.Log("���� 1����");
                break;
            case Effect_Info.Range:
                player.crossroads += 1; // ��Ÿ� ����
                Debug.Log("��Ÿ� 1����");
                break;
            case Effect_Info.Endurance:
                player.tenacity += 1; // ���ε� ����
                Debug.Log("���ε� 1����");
                break;
        }

        Debug.Log("����� ȿ��: " + randomEffect.ToString());
        yield return new WaitForSeconds(effect_maintain_time); // ������ �ð� ���� ���

        switch (randomEffect)
        {
            case Effect_Info.Attack_Speed:
                player.attackSpeed += 1; // ���ݼӵ� ����
                Debug.Log("ȿ������");
                break;
            case Effect_Info.Offense_Power:
                player.attackPower += 1; // ���ݷ� ����
                Debug.Log("ȿ������");
                break;
            case Effect_Info.Health:
                player.health += 1; // ü�� ����
                Debug.Log("ȿ������");
                break;
            case Effect_Info.Speed:
                player.addSpeed(1); // �̵��ӵ� ����
                Debug.Log("ȿ������");
                break;
            case Effect_Info.Stamina:
                player.endurance += 1; // ���¹̳� ����
                Debug.Log("ȿ������");
                break;
            case Effect_Info.Defense:
                player.defense += 1; // ���� ����
                Debug.Log("ȿ������");
                break;
            case Effect_Info.Range:
                player.crossroads += 1; // ��Ÿ� ����
                Debug.Log("ȿ������");
                break;
            case Effect_Info.Endurance:
                player.tenacity += 1; // ���ε� ����
                Debug.Log("ȿ������");
                break;
        }
    }
}