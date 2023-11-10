using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel; //메인메뉴창
    public GameObject HelpMenu; //도움창
    public GameObject FileLoadMenu; //파일로드 창
    public GameObject NewGameMenu; //새게임창

    private SettingMenu SettingMenu; //설정창

    SoundsPlayer SFXPlayer;

    List<GameObject> PanelList;
    List<GameObject> BackList;
    void Start()
    {
        SettingMenu = FindObjectOfType<SettingMenu>();
        SFXPlayer = GameObject.Find("SFXPlayer").GetComponent<SoundsPlayer>();
        StartPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("메인메뉴 뒤로가기");
            BackESC(); //ESC입력시 닫기
        }
        if (!HelpMenu.activeSelf && !FileLoadMenu.activeSelf && !NewGameMenu.activeSelf && !SettingMenu.SettingIsUse)
        { //다른 판넬들과 Setting메뉴가 사용중이 아닐때 활성화
            MainMenuPanel.SetActive(true);
        }

    }
    public void ClickSound()
    {
        SFXPlayer.UISound(0);
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
            if (i == 0) //첫번째 메인판넬만 활성화
                PanelList[i].SetActive(true);
            else //그 외에는 모두 끄기
                PanelList[i].SetActive(false);
        }
    }

    private void BackESC()
    {
        for (int i = 0; i < PanelList.Count; i++) //판넬 리스트 돌기
        {
            if (PanelList[i].activeSelf == true) //활성화 되어있는 판넬 찾기
            {
                if (MainMenuPanel.activeSelf)
                {
                    return;
                }
                else
                {
                    PanelList[i].SetActive(false); //활성화 되어있는 판넬은 종료
                    Debug.Log("비활성화된 판넬이름: " + PanelList[i]);
                }
            }
        }

        if (BackList.Count == 0) //뒤로갈 판넬이 없으면 리턴
            return;
        BackList[BackList.Count - 1].SetActive(true); //백리스트의 마지막판넬 활성화
        BackList.RemoveAt(BackList.Count - 1); //마지막 리스트는 삭제
    }

    public void GoSettingBTN()
    {
        ClickSound();
        MainMenuPanel.SetActive(false);
        SettingMenu.SettingPanel.SetActive(true);
        SettingMenu.SettingIsUse = true;
    }

    public void GoHelpBTN()
    {
        ClickSound();
        FindActivePanel();
        HelpMenu.SetActive(true);
        Debug.Log("도움창!");
    }

    public void GoNewGameBTN()
    {
        ClickSound();
        FindActivePanel();
        NewGameMenu.SetActive(true);
        Debug.Log("새게임버튼");
    }

    public void GoFileLoadBTN()
    {
        ClickSound();
        FindActivePanel();
        FileLoadMenu.SetActive(true);
        Debug.Log("파일로드창");
    }

    public void GameStartBTN()
    {
        ClickSound();
        SceneManager.LoadScene("RandomMap");
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
    }

    public void QuitBtn() //게임 나가기 버튼
    {
        ClickSound();
        Application.Quit();
        Debug.Log("Click Quit");
    }



    public void FileLoad1() //파일1 선택시
    {
        ClickSound();
        Debug.Log("File 1 Load!");
    }

    public void LoadBtn() //불러오기 버튼
    {
        ClickSound();
        Debug.Log("Load!");
    }
}
