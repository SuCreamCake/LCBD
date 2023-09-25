using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{
    public bool GameIsPause = false;
    public GameObject ControlPanel; //단축키 판넬
    public GameObject SettingPanel; //설정버튼 누르면 나오는 창
    public GameObject BGMPanel; //BGM 창
    public GameObject SFXPanel; //SFX창
    public GameObject LananguagePanel;

    List<GameObject> PanelList;
    List<GameObject> BackList;

    private PauseMenu Ispause;

    private void Start()
    {
        StartPanel();
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //esc입력시 Setting메뉴가 사용중이라면 사용
        {
            if(GameIsPause)
                BackESC(); //ESC입력시 닫기
        }
    }

    private void StartPanel()
    {
        PanelList = new List<GameObject>();
        BackList = new List<GameObject>();

        PanelList.Add(SettingPanel);
        PanelList.Add(ControlPanel);
        PanelList.Add(LananguagePanel);
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
        FindActivePanel();
        ControlPanel.SetActive(true);
    }

    public void go_Out() //나가기
    {
        Debug.Log("게임 종료함");
    }

    public void LanguageBtn() //언어창열기 버튼
    {
        FindActivePanel();
        LananguagePanel.SetActive(true);
    }
    public void SFXBtn() //효과음조절 창열기 버튼
    {
        FindActivePanel();
        SFXPanel.SetActive(true);
    }
    public void BGMBtn() //배경음조절 창열기 버튼
    {
        FindActivePanel();
        BGMPanel.SetActive(true);
    }

    public void FullScreenBtn() //배경음조절 창열기 버튼
    {
        Debug.Log("FullScreenBTN!!");
    }

    private void FindActivePanel() //버튼을 누를때마다 활성화된 판넬은 끄고 BackList에 추가
    {
        for (int i = 0; i < PanelList.Count; i++)
        {
            if (PanelList[i].activeSelf == true)
            {
                BackList.Add(PanelList[i]);
                PanelList[i].SetActive(false);
            }
        }
    }

    public void BackESC() //뒤로가기 버튼
    {
        for (int i = 0; i < PanelList.Count; i++) //
        {
            if (PanelList[i].activeSelf == true) //활성화 되어있는 게임 찾기
            {
                if (PanelList[0].activeSelf)
                { //설정창 버튼 판넬이면 설정창 종료
                    GameIsPause = false; //정지 풀기
                    Debug.Log("게임 재시작");
                }
                PanelList[i].SetActive(false); //활성화 되어있는 판넬은 종료
                Debug.Log("비활성화된 판넬이름: " + PanelList[i]);
            }

        }
        if (BackList.Count != 0) //백리스트 카운트가 0이 아니면 실행 없을경우 배열 에러걸림.
        {
            BackList[BackList.Count - 1].SetActive(true); //백리스트의 마지막판넬 활성화
            BackList.RemoveAt(BackList.Count - 1); //마지막 리스트는 삭제
        }
    }

}
