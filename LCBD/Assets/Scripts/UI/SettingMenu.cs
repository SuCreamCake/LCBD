using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{
    public bool SettingIsUse = false;
    public GameObject ControlPanel; //단축키 판넬
    public GameObject SettingPanel; //설정버튼 누르면 나오는 창
    public GameObject BGMPanel; //BGM 창
    public GameObject SFXPanel; //SFX창

    List<GameObject> PanelList;
    List<GameObject> BackList;

    SoundsPlayer SFXPlayer;

    private void Awake()
    {
        SFXPlayer = GameObject.Find("SFXPlayer").GetComponent<SoundsPlayer>();
        SettingIsUse = false;
    }
    private void Start()
    {
        StartPanel();
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //esc입력시 Setting메뉴가 사용중이라면 사용
        {
            if (SettingPanel.activeSelf) //셋팅판넬이 켜져있으면 리턴
            {
                SettingIsUse = false;
                SettingPanel.SetActive(false);
                return;
            }
            BackESC(); //ESC입력시 닫기
        }
        //Debug.Log(BackList.Count);
    }
    public void ClickSound()
    {
        SFXPlayer.UISound(0);
    }

    private void StartPanel()
    {
        PanelList = new List<GameObject>();
        BackList = new List<GameObject>();

        PanelList.Add(SettingPanel);
        PanelList.Add(ControlPanel);
        PanelList.Add(SFXPanel);
        PanelList.Add(BGMPanel);

        for (int i = 0; i < PanelList.Count; i++) //모두 비활성화
        {
            PanelList[i].SetActive(false);
        }
    }


    public void go_Menu() //메인으로
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void go_ControlBtn() //컨트롤창 열기
    {
        ClickSound();
        FindActivePanel();
        ControlPanel.SetActive(true);
    }

    public void go_Out() //나가기
    {
        Debug.Log("게임 종료함");
    }


    public void SFXBtn() //효과음조절 창열기 버튼
    {
        ClickSound();
        FindActivePanel();
        SFXPanel.SetActive(true);
    }
    public void BGMBtn() //배경음조절 창열기 버튼
    {
        ClickSound();
        FindActivePanel();
        BGMPanel.SetActive(true);
    }

    public void FullScreenBtn() //배경음조절 창열기 버튼
    {
        ClickSound();
        Debug.Log("FullScreenBTN!!");
    }

    private void FindActivePanel() //버튼을 누를때마다 활성화된 판넬은 끄고 BackList에 추가
    {
        for (int i = 0; i < PanelList.Count; i++)
        {
            if (PanelList[i].activeSelf == true) //활성화 되어있는 판넬을 찾는다.
            {
                BackList.Add(PanelList[i]); //활성화 되어있는 판넬을 BackList에 차례로 추가
                Debug.Log("백 리스트에 추가된 판넬이름: " + PanelList[i]);
                PanelList[i].SetActive(false); //활성화 되어있는 판넬은 끈다.
            }
        }
        SettingIsUse = true;
    }

    public void BackESC() //뒤로가기 버튼
    {
        for (int i = 0; i < PanelList.Count; i++)
        {
            if (PanelList[i].activeSelf == true) //활성화 되어있는 판넬 찾기
            {
                PanelList[i].SetActive(false); //현재 활성화 되어있는 판넬은 종료
                Debug.Log("비활성화된 판넬이름: " + PanelList[i]);
            }
        }

        if (BackList.Count == 0)
        { //더이상 뒤로갈게 없으면 종료
            return;
        }

        BackList[BackList.Count - 1].SetActive(true); //백리스트의 마지막판넬 활성화
        BackList.RemoveAt(BackList.Count - 1); //마지막 리스트는 삭제
    }

}
