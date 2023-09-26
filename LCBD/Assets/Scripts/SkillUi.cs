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

    /*지학 추가*/
    //쿨타임 텍스트
    public Text text_CoolTime;
    //쿨타임 이미지
    public Image image_fill;
    //스킬 재사용까지 남은시간
    private float time_current;
    //time.Time과 비교해서 time 
    private float time_start;
    private bool isEnded = true;
    //hp바 텍스트
    public Text text_hp;
    //hp바 이미지
    public Image img_Hp;
    //스테미나 바 텍스트
    public Text text_endurance;
    //스테미나 바 이미지
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

    /*지학*/
    //image_fill의 fillAmount를 360도 시계 반대 방향으로 회전하게 설정
    private void Init_UI()
    {
        image_fill.type = Image.Type.Filled;
        image_fill.fillMethod = Image.FillMethod.Radial360;
        image_fill.fillOrigin = (int)Image.Origin360.Top;
        image_fill.fillClockwise = false;
    }
    //쿨타임 리셋
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
    //쿨타임이 끝나서 스킬 재사용이 가능해진 시점
    private void End_CoolTime()
    {
        Set_FillAmount(0);
        isEnded = true;
        text_CoolTime.gameObject.SetActive(false);
    }
    //쿨타임 타이머 시작
    private void Reset_CoolTime()
    {
        text_CoolTime.gameObject.SetActive(true);
        time_current = player.attackSpeed;
        time_start = Time.time;
        Set_FillAmount(player.attackSpeed);
        isEnded = false;
    }
    //스킬 재사용 시간 시각화
    private void Set_FillAmount(float _value)
    {
        image_fill.fillAmount = _value / player.attackSpeed;
        string txt = _value.ToString("0.0");
        text_CoolTime.text = txt;
    }
    //HP의 값과 UI 표시 초기화
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
    //hp에서 매개 변수로 받은 float 값을 더함
    private void Change_HP(float _value)
    {
        player.health += _value;
        Set_HP(player.health);
    }
    //hp를 매개변수로 받은 float 값으로 변경
    private void Set_HP(float _value)
    {
        string txt = "";
        player.health = _value;

        img_Hp.fillAmount = player.health / player.maxHealth;
        txt = string.Format("{0}/{1}", player.health, player.maxHealth);
        text_hp.text = txt;
    }
    //endurance에서 매개 변수로 받은 float 값을 더함
    private void Change_Endurance(float _value)
    {
        player.health += _value;
        Set_HP(player.endurance);
    }

    //endurance를 매개변수로 받은 float 값으로 변경
    private void Set_Endurance(float _value)
    {
        string txt = "";
        player.endurance = _value;

        img_endurance.fillAmount = player.endurance / player.maxEndurance;
        txt = string.Format("{0}/{1}", player.endurance, player.maxEndurance);
        text_endurance.text = txt;
    }
}
