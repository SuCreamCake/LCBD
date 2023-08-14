using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu_Btn_Event: MonoBehaviour
{
    public GameObject MainPanel; //메인메뉴창
    public GameObject SettingPanel; //설정창
    public GameObject HelpPanel; //도움창
    public GameObject FileLoadPanel; //파일로드 창
    public GameObject NewGamePanel; //새게임 창
    public GameObject ControlPanel; //컨트롤 창
    public GameObject MusicPanel; //배경음악 조절 창
    public GameObject LanguagePanel; //언어선택 창

    List<GameObject> PanelList;
    List<GameObject> BackList;

    private void Start()
    {
        PanelList = new List<GameObject>();
        BackList = new List<GameObject>();



        //게임 오브젝트 리스트에 추가
        PanelList.Add(MainPanel);
        PanelList.Add(SettingPanel);
        PanelList.Add(HelpPanel);
        PanelList.Add(FileLoadPanel);
        PanelList.Add(NewGamePanel);
        PanelList.Add(ControlPanel);
        PanelList.Add(MusicPanel);
        PanelList.Add(LanguagePanel);

        //메인판넬 말고 모두 false
        MainPanel.SetActive(true);
        SettingPanel.SetActive(false);
        HelpPanel.SetActive(false);
        FileLoadPanel.SetActive(false);
        NewGamePanel.SetActive(false);
        ControlPanel.SetActive(false);
        MusicPanel.SetActive(false);
        LanguagePanel.SetActive(false);

       // Debug.Log("판넬리스트 크기:" + PanelList.Count);
        //Debug.Log("백리스트 크기:" + BackList.Count);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            BackESC(); //ESC입력시 닫기
    }

    private void FindActivePanel() //버튼을 누를때마다 활성화된 판넬은 끄고 BackList에 추가
    {
        for (int i=0; i<PanelList.Count; i++)
        {
            if(PanelList[i].activeSelf == true)
            {
                BackList.Add(PanelList[i]);
                PanelList[i].SetActive(false);
            }
        }
    }


    public void BackESC() //뒤로가기 버튼
    {
        Debug.Log("Back!");

        for (int i = 0; i < PanelList.Count; i++) //
        {
            if (PanelList[i].activeSelf == true) //활성화 되어있는 게임 찾기
            {
                if (PanelList[i] == MainPanel) //메인판넬이면 메소드 탈출
                    return;
                PanelList[i].SetActive(false); //활성화 되어있는 판넬은 종료
                //Debug.Log("비활성화된 판넬이름: " + PanelList[i]);
            }
        }
        BackList[BackList.Count-1].SetActive(true); //백리스트의 마지막판넬 활성화
        BackList.RemoveAt(BackList.Count-1); //마지막 리스트는 삭제
        //Debug.Log("백리스트 크기:" + BackList.Count);

    }

    public void StartBtn() //메인메뉴 게임시작버튼
    {
        Debug.Log("Click Start");
        FindActivePanel();
        NewGamePanel.SetActive(true);
        SceneManager.LoadScene("player");
       // Debug.Log("백리스트 크기:" + BackList.Count);
    }

    public void QuitBtn() //게임 나가기 버튼
    {
        Application.Quit();
        Debug.Log("Click Quit");
    }

    public void SettingBtn() //옵션창 버튼
    {
        Debug.Log("Setting!");
        FindActivePanel();
        //Debug.Log("백리스트 크기:" + BackList.Count);
        SettingPanel.SetActive(true);
    }

    public void HelpBtn() //도움창 버튼
    {
        Debug.Log("Help!");
        FindActivePanel();
        //Debug.Log("백리스트 크기:" + BackList.Count);
        HelpPanel.SetActive(true);
    }

    public void BackBtn() //뒤로가기 버튼 어느 화면이든 뒤로가기 버튼이면 일단 메인메뉴창으로 가도록설정
    {
        Debug.Log("Back!");
        BackESC();
    }

    public void ControlBtn()
    {
        Debug.Log("Control!");
        FindActivePanel();
        //Debug.Log("백리스트 크기:" + BackList.Count);
        ControlPanel.SetActive(true);
    }

    public void SFXBtn()
    {
        Debug.Log("SFX!");
        //FindActivePanel();
    }

    public void MusicBtn()
    {
        Debug.Log("Music!");
        FindActivePanel();
       // Debug.Log("백리스트 크기:" + BackList.Count);
        MusicPanel.SetActive(true);
    }

    public void FullScreenBtn()
    {
        Debug.Log("FullScreen!");
    }

    public void LanguageBtn()
    {
        Debug.Log("Language!");
        FindActivePanel();
       // Debug.Log("백리스트 크기:" + BackList.Count);
        LanguagePanel.SetActive(true);
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
        FindActivePanel();
       // Debug.Log("백리스트 크기:" + BackList.Count);
        NewGamePanel.SetActive(true);
    }

    public void LoadBtn() //불러오기 버튼
    {
        Debug.Log("Load!");
    }

    public void DeleteBtn() //삭제 버튼
    {
        Debug.Log("Delete!");
    }


    public void KoreanBtn()
    {
        Debug.Log("한국어");
    }

    public void EnglishBtn()
    {
        Debug.Log("English");
    }
}
