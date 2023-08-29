using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //다른 스크립트에서도 사용가능하게 static으로 설정
    public static bool GameIsPause = false; //메뉴 상태여부
    public GameObject pauseMenuPanel;
    public GameObject SettingPanel;
    public GameObject BGMPanel;
    public GameObject SFXPanel;
    public GameObject SoundChoicePanel;

    List<GameObject> PanelList;
    List<GameObject> BackList;

    private void Start()
    {
        StartPanel();

        Resume();

    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause && pauseMenuPanel.activeSelf) //정지상태에서 정지창이 켜져있으면 게임진행
                Resume();
            else if( GameIsPause && !pauseMenuPanel.activeSelf) //정지상태에서 정지창이 아닌 다른판넬일경우 뒤로가기 
                BackESC(); //ESC입력시 닫기
            else if(!GameIsPause && !pauseMenuPanel.activeSelf) //게임이 진행중이고 판넬이 켜져있으면
                Pause();
        }
            
    }

    private void StartPanel()
    {
        PanelList = new List<GameObject>();
        BackList = new List<GameObject>();

        PanelList.Add(pauseMenuPanel);
        PanelList.Add(SettingPanel);
        PanelList.Add(SoundChoicePanel);
        PanelList.Add(SFXPanel);
        PanelList.Add(BGMPanel);


        for (int i = 0; i < PanelList.Count; i++) //모두 비활성화
        {
            PanelList[i].SetActive(false);
        }
    }


    public void EscPause() //esc누르면 정지시키는 메소드
    {
          if (GameIsPause == true && PanelList[0].activeSelf) //게임이 정지상태일때 누르면 게임 실행되게
          {
              Resume(); //계속하기
          }
          else
          {
              Pause(); //정지하기
          }
       
    }


    public void Resume() //계속하기 시키는 메소드
    {
        for(int i=0; i<PanelList.Count; i++)
        {
            PanelList[i].SetActive(false);
        }
        Time.timeScale = 1f;
        GameIsPause = false;
    }


    public void Pause() //정지시키는 메소드
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }


    public void go_Menu() //메인으로
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void SettingBtn() //설정창 열기
    {
        FindActivePanel();
        SettingPanel.SetActive(true);
    }

    public void go_Out() //나가기
    {
        Debug.Log("게임 종료함");
    }

    public void SoundBtn() //사운드조절 창열기 버튼
    {
        FindActivePanel();
        SoundChoicePanel.SetActive(true);
    }
    public void SFXBtn() //사운드조절 창열기 버튼
    {
        FindActivePanel();
        SFXPanel.SetActive(true);
    }
    public void BGMBtn() //사운드조절 창열기 버튼
    {
        FindActivePanel();
        BGMPanel.SetActive(true);
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
                if (PanelList[i] == pauseMenuPanel) //정지 첫 판넬이면 메소드 탈출
                    return;
                PanelList[i].SetActive(false); //활성화 되어있는 판넬은 종료
                //Debug.Log("비활성화된 판넬이름: " + PanelList[i]);
            }
        }
        BackList[BackList.Count - 1].SetActive(true); //백리스트의 마지막판넬 활성화
        BackList.RemoveAt(BackList.Count - 1); //마지막 리스트는 삭제
        //Debug.Log("백리스트 크기:" + BackList.Count);

    }



}
