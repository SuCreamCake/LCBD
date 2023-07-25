using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public GameObject MainPanel; //���θ޴�â
    public GameObject SettingPanel; //�ɼ�â
    public GameObject HelpPanel; //����â
    public GameObject FileLoadPanel; //���Ϸε� â
    public GameObject NewGamePanel; //������ â

    private void Start()
    { //���� ù ȭ��� ���θ޴� ���� ���̵��� ����
        MainPanel.SetActive(true);
        SettingPanel.SetActive(false);
        HelpPanel.SetActive(false);
        FileLoadPanel.SetActive(false);
        NewGamePanel.SetActive(false);
    }
    public void OnClickStartBtn() //���θ޴� ���ӽ��۹�ư
    {
        Debug.Log("Click Start");
        MainPanel.SetActive(false);
        SettingPanel.SetActive(false);
        HelpPanel.SetActive(false);
        FileLoadPanel.SetActive(true);
        NewGamePanel.SetActive(false);
    }

    public void OnClickQuitBtn() //���� ������ ��ư
    {
        Application.Quit();
        Debug.Log("Click Quit");
    }

    public void OnclickSkipBtn() //������ ��ŵ�ϱ� ��ư
    {
        Debug.Log("Skip!");
        SceneManager.LoadScene("StartMenu");
    }

    public void OnClickSettingBtn() //�ɼ�â ��ư
    {
        Debug.Log("Setting!");
        MainPanel.SetActive(false);
        SettingPanel.SetActive(true);
        HelpPanel.SetActive(false);
        FileLoadPanel.SetActive(false);
        NewGamePanel.SetActive(false);
    }

    public void OnclickHelpBtn() //����â ��ư
    {
        Debug.Log("Help!");
        MainPanel.SetActive(false);
        SettingPanel.SetActive(false);
        HelpPanel.SetActive(true);
        FileLoadPanel.SetActive(false);
        NewGamePanel.SetActive(false);
    }

    public void OnclickBackBtn() //�ڷΰ��� ��ư
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

    public void FileLoad1() //����1 ���ý�
    {
        Debug.Log("File 1 Load!");
    }
    public void FileLoad2() //����2 ���ý�
    {
        Debug.Log("File 2 Load!");
    }
    public void FileLoad3() //����3 ���ý�
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
