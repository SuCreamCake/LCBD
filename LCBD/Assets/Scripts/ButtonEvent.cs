using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public GameObject MainPanel; //메인메뉴창
    public GameObject SettingPanel; //설정창
    public GameObject HelpPanel; //도움창
    public GameObject FileLoadPanel; //파일로드 창
    public GameObject NewGamePanel; //새게임 창
    public GameObject ControlPanel; //컨트롤 창
    public GameObject MusicPanel; //배경음악 조절 창



    private void Start()
    { //게임 첫 화면시 메인메뉴 먼저 보이도록 설정
        MainPanel.SetActive(false);
        SettingPanel.SetActive(false);
        HelpPanel.SetActive(false);
        FileLoadPanel.SetActive(false);
        NewGamePanel.SetActive(false);
        ControlPanel.SetActive(false);
        MusicPanel.SetActive(true);
    }
    private void Update()
    {
        BackESC(); //ESC입력시 닫기
    }

    public void BackESC() //뒤로가기 버튼
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Back!");
            if (ControlPanel) //컨트롤창에서 뒤로가기를 할경우
            {
                ControlPanel.SetActive(false); //컨트롤창이 꺼지고
                SettingPanel.SetActive(true); //설정창으로 돌아간다.

                HelpPanel.SetActive(false);
                NewGamePanel.SetActive(false);
                MainPanel.SetActive(false);
                FileLoadPanel.SetActive(false);
                MusicPanel.SetActive(false);
                //return;
            }
            else if (HelpPanel)
            {
                HelpPanel.SetActive(false); //도움창이 꺼지고
                MainPanel.SetActive(true); //설정창으로 돌아간다.

                FileLoadPanel.SetActive(false);
                NewGamePanel.SetActive(false);
                ControlPanel.SetActive(false); 
                SettingPanel.SetActive(false);
                MusicPanel.SetActive(false);
                //return;
            }
            else if (SettingPanel)
            {
                Debug.Log("왜안가");
                SettingPanel.SetActive(false); //설정 창이 꺼지고
                MainPanel.SetActive(true); //메인 메뉴창으로 돌아간다.

                HelpPanel.SetActive(false);
                FileLoadPanel.SetActive(false);
                NewGamePanel.SetActive(false);
                ControlPanel.SetActive(false);
                MusicPanel.SetActive(false);
                //return;
            }
            else if (NewGamePanel)
            {
                SettingPanel.SetActive(false); //설정 창이 꺼지고
                FileLoadPanel.SetActive(true); //파일 로드창으로 돌아간다.

                HelpPanel.SetActive(false);
                MainPanel.SetActive(false);
                NewGamePanel.SetActive(false);
                ControlPanel.SetActive(false);
                MusicPanel.SetActive(false);
            }

            else if (FileLoadPanel)
            {
                FileLoadPanel.SetActive(false); //설정 창이 꺼지고
                MainPanel.SetActive(true); //메인메뉴 창으로 돌아간다.

                HelpPanel.SetActive(false);
                SettingPanel.SetActive(false);
                NewGamePanel.SetActive(false);
                ControlPanel.SetActive(false);
                MusicPanel.SetActive(false);
            }
            else if (MusicPanel)
            {
                MusicPanel.SetActive(false); //설정 창이 꺼지고
            }

        }
    }

    public void OnClickStartBtn() //메인메뉴 게임시작버튼
    {
        Debug.Log("Click Start");
        MainPanel.SetActive(false); //메인메뉴 창이 내려가고
        FileLoadPanel.SetActive(true); //파일 로드 창이 활성화
    }

    public void OnClickQuitBtn() //게임 나가기 버튼
    {
        Application.Quit();
        Debug.Log("Click Quit");
    }

    public void OnclickSkipBtn() //오프닝 스킵하기 버튼
    {
        Debug.Log("Skip!");
        SceneManager.LoadScene("StartMenu");
    }

    public void OnClickSettingBtn() //옵션창 버튼
    {
        Debug.Log("Setting!");
        MainPanel.SetActive(false);
        SettingPanel.SetActive(true);
        HelpPanel.SetActive(false);
        FileLoadPanel.SetActive(false);
        NewGamePanel.SetActive(false);
    }

    public void OnclickHelpBtn() //도움창 버튼
    {
        Debug.Log("Help!");
        MainPanel.SetActive(false); //메인메뉴창이 꺼지고
        HelpPanel.SetActive(true); //도움창이 켜진다.
    }

    public void OnclickBackBtn() //뒤로가기 버튼 어느 화면이든 뒤로가기 버튼이면 일단 메인메뉴창으로 가도록설정
    {
        Debug.Log("Back!");
        MainPanel.SetActive(true);
        SettingPanel.SetActive(false);
        HelpPanel.SetActive(false);
        FileLoadPanel.SetActive(false);
        NewGamePanel.SetActive(false);
    }

    public void ControlBtn()
    {
        Debug.Log("Control!");
        SettingPanel.SetActive(false); //셋팅창이 내려가고
        ControlPanel.SetActive(true); //컨트롤창이 활성화
    }

    public void SFXBtn()
    {
        Debug.Log("SFX!");
    }

    public void MusicBtn()
    {
        Debug.Log("Music!");
        MusicPanel.SetActive(true);
    }

    public void FullScreenBtn()
    {
        Debug.Log("FullScreen!");
    }

    public void LanguageBtn()
    {
        Debug.Log("Language!");
    }

    public void FileLoad1() //파일1 선택시
    {
        Debug.Log("File 1 Load!");
    }
    public void FileLoad2() //파일2 선택시
    {
        Debug.Log("File 2 Load!");
    }
    public void FileLoad3() //파일3 선택시
    {
        Debug.Log("File 3 Load!");
    }

    public void NewGameBtn() //새게임 버튼 판넬 빈거 밖에 안나옴
    {
        Debug.Log("New Game!");
        MainPanel.SetActive(false);
        SettingPanel.SetActive(false);
        HelpPanel.SetActive(false);
        FileLoadPanel.SetActive(false);
        NewGamePanel.SetActive(true);
    }

    public void LoadBtn() //로드 버튼
    {
        Debug.Log("Load!");
    }

    public void DeleteBtn() //삭제 버튼
    {
        Debug.Log("Delete!");
    }

    public void FileSceneQuitBtn() //파일 로드씬에서 나가기버튼 메인메뉴로 이동
    {
        Debug.Log("FileSceneQuit!");
        MainPanel.SetActive(true);
        SettingPanel.SetActive(false);
        HelpPanel.SetActive(false);
        FileLoadPanel.SetActive(false);
        NewGamePanel.SetActive(false);
    }
}
