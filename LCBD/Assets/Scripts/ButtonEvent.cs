using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public GameObject MainPanel; //메인메뉴창
    public GameObject SettingPanel; //옵션창
    public GameObject HelpPanel; //도움창
    public GameObject FileLoadPanel; //파일로드 창
    public GameObject NewGamePanel; //새게임 창

    private void Start()
    { //게임 첫 화면시 메인메뉴 먼저 보이도록 설정
        MainPanel.SetActive(true);
        SettingPanel.SetActive(false);
        HelpPanel.SetActive(false);
        FileLoadPanel.SetActive(false);
        NewGamePanel.SetActive(false);
    }
    public void OnClickStartBtn() //메인메뉴 게임시작버튼
    {
        Debug.Log("Click Start");
        MainPanel.SetActive(false);
        SettingPanel.SetActive(false);
        HelpPanel.SetActive(false);
        FileLoadPanel.SetActive(true);
        NewGamePanel.SetActive(false);
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
        MainPanel.SetActive(false);
        SettingPanel.SetActive(false);
        HelpPanel.SetActive(true);
        FileLoadPanel.SetActive(false);
        NewGamePanel.SetActive(false);
    }

    public void OnclickBackBtn() //뒤로가기 버튼
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
    }

    public void SFXBtn()
    {
        Debug.Log("SFX!");
    }

    public void MusicBtn()
    {
        Debug.Log("Music!");
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

    public void NewGameBtn()
    {
        Debug.Log("New Game!");
        MainPanel.SetActive(false);
        SettingPanel.SetActive(false);
        HelpPanel.SetActive(false);
        FileLoadPanel.SetActive(false);
        NewGamePanel.SetActive(true);
    }

    public void LoadBtn()
    {
        Debug.Log("Load!");
    }

    public void DeleteBtn()
    {
        Debug.Log("Delete!");
    }

    public void FileSceneQuitBtn()
    {
        Debug.Log("FileSceneQuit!");
        MainPanel.SetActive(true);
        SettingPanel.SetActive(false);
        HelpPanel.SetActive(false);
        FileLoadPanel.SetActive(false);
        NewGamePanel.SetActive(false);
    }
}
