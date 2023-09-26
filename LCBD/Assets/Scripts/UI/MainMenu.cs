using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool MainMenuIsUse = true; //메뉴가 쓰이는지 확인창
    public GameObject MainMenuPanel; //메인메뉴창
    public GameObject HelpMenu; //도움창
    public GameObject FileLoadMenu; //파일로드 창
    public GameObject NewGameMenu; //새게임창

    private SettingMenu SettingMenu; //설정창

    List<GameObject> PanelList;
    List<GameObject> BackList;
    void Start()
    {
        SettingMenu = FindObjectOfType<SettingMenu>();
        StartPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && MainMenuIsUse) //esc입력시 Setting메뉴가 사용중이라면 사용
        {
            BackESC(); //ESC입력시 닫기
        }

        if (SettingMenu.SettingPanel.activeSelf)
        {
            MainMenuIsUse = true; //메인메뉴 쓰임.
            MainMenuPanel.SetActive(true);//메인메뉴 활성화
        }

    }

    private void StartPanel()
    {
        PanelList = new List<GameObject>();
        BackList = new List<GameObject>();

        PanelList.Add(MainMenuPanel);
        PanelList.Add(HelpMenu);
        PanelList.Add(FileLoadMenu);
        PanelList.Add(NewGameMenu);


        for (int i = 0; i < PanelList.Count; i++) //모두 비활성화
        {
            if (i == 0)
                PanelList[i].SetActive(true);
            else
                PanelList[i].SetActive(false);
        }
    }

    private void BackESC()
    {
        for (int i = 0; i < PanelList.Count; i++) //
        {
            if (PanelList[i].activeSelf == true) //활성화 되어있는 게임 찾기
            {
                if (MainMenuPanel.activeSelf) //메인메뉴가 켜져있으면 뒤로가기 취소
                    return;
                PanelList[i].SetActive(false); //활성화 되어있는 판넬은 종료
                //Debug.Log("비활성화된 판넬이름: " + PanelList[i]);
            }
        }
        BackList[BackList.Count - 1].SetActive(true); //백리스트의 마지막판넬 활성화
        BackList.RemoveAt(BackList.Count - 1); //마지막 리스트는 삭제
    }

    public void GoSettingBTN()
    {
        MainMenuIsUse = false;
        MainMenuPanel.SetActive(false);
        SettingMenu.SettingPanel.SetActive(true);
    }

    public void GoHelpBTN()
    {
        FindActivePanel();
        MainMenuIsUse = false;
        HelpMenu.SetActive(true);
        Debug.Log("도움창!");
    }

    public void GoNewGameBTN()
    {
        FindActivePanel();
        MainMenuIsUse = false;
        NewGameMenu.SetActive(true);
        Debug.Log("새게임버튼");
    }

    public void GoFileLoadBTN()
    {
        FindActivePanel();
        MainMenuIsUse = false;
        FileLoadMenu.SetActive(true);
        Debug.Log("파일로드창");
    }

    public void GameStartBTN()
    {
        MainMenuIsUse = false;
        SceneManager.LoadScene("RandomMap");
    }

    public void QuitBTN()
    {
        FindActivePanel();
        MainMenuIsUse = false;
        FileLoadMenu.SetActive(true);
        Debug.Log("게임종료");
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
}
