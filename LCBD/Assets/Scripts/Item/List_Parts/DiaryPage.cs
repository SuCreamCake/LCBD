using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryPage : List_Item
{
    private void Awake() //�ش������ �ʱⰪ ����
    {
        item_number = 37;
        item_Name = "DiaryPage"; //�������̸�
        Rank = Item_Rank.Unique; //������ ��͵�
        drop_age = Drop_age.All; //ŉ�� ������ ���
        effect_type = Effect_Type.ending_key; //ȿ�� Ÿ��
        effect_info = Effect_Info.Null; //ȿ�� ����
        effect_target = Effect_Target.Null; //ȿ�� ���� ���
        effect_figures = 0; //�󸶳� �����Դ��� ����
        effect_active_type = Effect_Active_Type.Null; //ȿ�� ����
        effect_maintain_time = 0; //ȿ���ߵ� �� ȿ������Ǵ� �ð�
        //item_sprite; //������ �̹��� �̹����� �����
    }

    public override void DestraoyAfterTime() //����� �۾�
    { }

    public override void Use_Effect() //���ȿ��
    { }
}
