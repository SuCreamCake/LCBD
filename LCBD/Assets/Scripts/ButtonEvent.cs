using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public GameObject MainPanel; //���θ޴�â
    public GameObject SettingPanel; //����â
    public GameObject HelpPanel; //����â
    public GameObject FileLoadPanel; //���Ϸε� â
    public GameObject NewGamePanel; //������ â
    public GameObject ControlPanel; //��Ʈ�� â
    public GameObject MusicPanel; //������� ���� â



    private void Start()
    { //���� ù ȭ��� ���θ޴� ���� ���̵��� ����
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
        BackESC(); //ESC�Է½� �ݱ�
    }

    public void BackESC() //�ڷΰ��� ��ư
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Back!");
            if (ControlPanel) //��Ʈ��â���� �ڷΰ��⸦ �Ұ��
            {
                ControlPanel.SetActive(false); //��Ʈ��â�� ������
                SettingPanel.SetActive(true); //����â���� ���ư���.

                HelpPanel.SetActive(false);
                NewGamePanel.SetActive(false);
                MainPanel.SetActive(false);
                FileLoadPanel.SetActive(false);
                MusicPanel.SetActive(false);
                //return;
            }
            else if (HelpPanel)
            {
                HelpPanel.SetActive(false); //����â�� ������
                MainPanel.SetActive(true); //����â���� ���ư���.

                FileLoadPanel.SetActive(false);
                NewGamePanel.SetActive(false);
                ControlPanel.SetActive(false); 
                SettingPanel.SetActive(false);
                MusicPanel.SetActive(false);
                //return;
            }
            else if (SettingPanel)
            {
                Debug.Log("�־Ȱ�");
                SettingPanel.SetActive(false); //���� â�� ������
                MainPanel.SetActive(true); //���� �޴�â���� ���ư���.

                HelpPanel.SetActive(false);
                FileLoadPanel.SetActive(false);
                NewGamePanel.SetActive(false);
                ControlPanel.SetActive(false);
                MusicPanel.SetActive(false);
                //return;
            }
            else if (NewGamePanel)
            {
                SettingPanel.SetActive(false); //���� â�� ������
                FileLoadPanel.SetActive(true); //���� �ε�â���� ���ư���.

                HelpPanel.SetActive(false);
                MainPanel.SetActive(false);
                NewGamePanel.SetActive(false);
                ControlPanel.SetActive(false);
                MusicPanel.SetActive(false);
            }

            else if (FileLoadPanel)
            {
                FileLoadPanel.SetActive(false); //���� â�� ������
                MainPanel.SetActive(true); //���θ޴� â���� ���ư���.

                HelpPanel.SetActive(false);
                SettingPanel.SetActive(false);
                NewGamePanel.SetActive(false);
                ControlPanel.SetActive(false);
                MusicPanel.SetActive(false);
            }
            else if (MusicPanel)
            {
                MusicPanel.SetActive(false); //���� â�� ������
            }

        }
    }

    public void OnClickStartBtn() //���θ޴� ���ӽ��۹�ư
    {
        Debug.Log("Click Start");
        MainPanel.SetActive(false); //���θ޴� â�� ��������
        FileLoadPanel.SetActive(true); //���� �ε� â�� Ȱ��ȭ
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
        MainPanel.SetActive(false); //���θ޴�â�� ������
        HelpPanel.SetActive(true); //����â�� ������.
    }

    public void OnclickBackBtn() //�ڷΰ��� ��ư ��� ȭ���̵� �ڷΰ��� ��ư�̸� �ϴ� ���θ޴�â���� �����ϼ���
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
        SettingPanel.SetActive(false); //����â�� ��������
        ControlPanel.SetActive(true); //��Ʈ��â�� Ȱ��ȭ
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

    public void NewGameBtn() //������ ��ư �ǳ� ��� �ۿ� �ȳ���
    {
        Debug.Log("New Game!");
        MainPanel.SetActive(false);
        SettingPanel.SetActive(false);
        HelpPanel.SetActive(false);
        FileLoadPanel.SetActive(false);
        NewGamePanel.SetActive(true);
    }

    public void LoadBtn() //�ε� ��ư
    {
        Debug.Log("Load!");
    }

    public void DeleteBtn() //���� ��ư
    {
        Debug.Log("Delete!");
    }

    public void FileSceneQuitBtn() //���� �ε������ �������ư ���θ޴��� �̵�
    {
        Debug.Log("FileSceneQuit!");
        MainPanel.SetActive(true);
        SettingPanel.SetActive(false);
        HelpPanel.SetActive(false);
        FileLoadPanel.SetActive(false);
        NewGamePanel.SetActive(false);
    }
}
