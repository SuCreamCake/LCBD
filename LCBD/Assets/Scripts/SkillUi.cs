using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using UnityEngine.UI;

public class SkillUi : MonoBehaviour
{
    GameObject playerObject;
    Player player;

    /*���� �߰�*/
    //��Ÿ�� �ؽ�Ʈ
    public Text text_CoolTime;
    //��Ÿ�� �̹���
    public Image image_fill;
    //��ų ������� �����ð�
    private float time_current;
    //time.Time�� ���ؼ� time 
    private float time_start;
    private bool isEnded = true;
    //hp�� �ؽ�Ʈ
    public Text text_hp;
    //hp�� �̹���
    public Image img_Hp;
    //���׹̳� �� �ؽ�Ʈ
    public Text text_endurance;
    //���׹̳� �� �̹���
    public Image img_endurance;


    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();

        Init_UI();
        Init_HP_endurance();
        SetFunction_UI();
    }

    // Update is called once per frame
    void Update()
    {
        Check_CoolTime();
        SetFunction_UI();
        Set_HP(player.health);
        Set_Endurance(player.endurance);
    }

    /*����*/
    //image_fill�� fillAmount�� 360�� �ð� �ݴ� �������� ȸ���ϰ� ����
    private void Init_UI()
    {
        image_fill.type = Image.Type.Filled;
        image_fill.fillMethod = Image.FillMethod.Radial360;
        image_fill.fillOrigin = (int)Image.Origin360.Top;
        image_fill.fillClockwise = false;
    }
    //��Ÿ�� ����
    private void Check_CoolTime()
    {
        time_current = Time.time - time_start;
        if (time_current < player.attackSpeed)
        {
            Set_FillAmount(player.attackSpeed - time_current);
        }
        else if (!isEnded)
        {
            End_CoolTime();
        }
    }
    //��Ÿ���� ������ ��ų ������ �������� ����
    private void End_CoolTime()
    {
        Set_FillAmount(0);
        isEnded = true;
        text_CoolTime.gameObject.SetActive(false);
    }
    //��Ÿ�� Ÿ�̸� ����
    private void Reset_CoolTime()
    {
        text_CoolTime.gameObject.SetActive(true);
        time_current = player.attackSpeed;
        time_start = Time.time;
        Set_FillAmount(player.attackSpeed);
        isEnded = false;
    }
    //��ų ���� �ð� �ð�ȭ
    private void Set_FillAmount(float _value)
    {
        image_fill.fillAmount = _value / player.attackSpeed;
        string txt = _value.ToString("0.0");
        text_CoolTime.text = txt;
    }
    //HP�� ���� UI ǥ�� �ʱ�ȭ
    private void Init_HP_endurance()
    {
        Set_HP(player.maxHealth);
        Set_Endurance(player.maxEndurance);
    }

    private void SetFunction_UI()
    {
        //Fill Amount Type
        img_Hp.type = Image.Type.Filled;
        img_Hp.fillMethod = Image.FillMethod.Horizontal;
        img_Hp.fillOrigin = (int)Image.OriginHorizontal.Left;
        img_endurance.type = Image.Type.Filled;
        img_endurance.fillMethod = Image.FillMethod.Horizontal;
        img_endurance.fillOrigin = (int)Image.OriginHorizontal.Left;
    }
    //hp���� �Ű� ������ ���� float ���� ����
    private void Change_HP(float _value)
    {
        player.health += _value;
        Set_HP(player.health);
    }
    //hp�� �Ű������� ���� float ������ ����
    private void Set_HP(float _value)
    {
        string txt = "";
        player.health = _value;

        img_Hp.fillAmount = player.health / player.maxHealth;
        txt = string.Format("{0}/{1}", player.health, player.maxHealth);
        text_hp.text = txt;
    }
    //endurance���� �Ű� ������ ���� float ���� ����
    private void Change_Endurance(float _value)
    {
        player.health += _value;
        Set_HP(player.endurance);
    }

    //endurance�� �Ű������� ���� float ������ ����
    private void Set_Endurance(float _value)
    {
        string txt = "";
        player.endurance = _value;

        img_endurance.fillAmount = player.endurance / player.maxEndurance;
        txt = string.Format("{0}/{1}", player.endurance, player.maxEndurance);
        text_endurance.text = txt;
    }
}
