using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu_Btn_Event: MonoBehaviour
{
    public GameObject MainPanel; //���θ޴�â
    public GameObject SettingPanel; //����â
    public GameObject HelpPanel; //����â
    public GameObject FileLoadPanel; //���Ϸε� â
    public GameObject NewGamePanel; //������ â
    public GameObject ControlPanel; //��Ʈ�� â
    public GameObject MusicPanel; //������� ���� â
    public GameObject LanguagePanel; //���� â

    List<GameObject> PanelList;
    List<GameObject> BackList;

    private void Start()
    {
        PanelList = new List<GameObject>();
        BackList = new List<GameObject>();



        //���� ������Ʈ ����Ʈ�� �߰�
        PanelList.Add(MainPanel);
        PanelList.Add(SettingPanel);
        PanelList.Add(HelpPanel);
        PanelList.Add(FileLoadPanel);
        PanelList.Add(NewGamePanel);
        PanelList.Add(ControlPanel);
        PanelList.Add(MusicPanel);
        PanelList.Add(LanguagePanel);

        //�����ǳ� ���� ��� false
        MainPanel.SetActive(true);
        SettingPanel.SetActive(false);
        HelpPanel.SetActive(false);
        FileLoadPanel.SetActive(false);
        NewGamePanel.SetActive(false);
        ControlPanel.SetActive(false);
        MusicPanel.SetActive(false);
        LanguagePanel.SetActive(false);

       // Debug.Log("�ǳڸ���Ʈ ũ��:" + PanelList.Count);
        //Debug.Log("�鸮��Ʈ ũ��:" + BackList.Count);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            BackESC(); //ESC�Է½� �ݱ�
    }

    private void FindActivePanel() //��ư�� ���������� Ȱ��ȭ�� �ǳ��� ���� BackList�� �߰�
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


    public void BackESC() //�ڷΰ��� ��ư
    {
        Debug.Log("Back!");

        for (int i = 0; i < PanelList.Count; i++) //
        {
            if (PanelList[i].activeSelf == true) //Ȱ��ȭ �Ǿ��ִ� ���� ã��
            {
                if (PanelList[i] == MainPanel) //�����ǳ��̸� �޼ҵ� Ż��
                    return;
                PanelList[i].SetActive(false); //Ȱ��ȭ �Ǿ��ִ� �ǳ��� ����
                //Debug.Log("��Ȱ��ȭ�� �ǳ��̸�: " + PanelList[i]);
            }
        }
        BackList[BackList.Count-1].SetActive(true); //�鸮��Ʈ�� �������ǳ� Ȱ��ȭ
        BackList.RemoveAt(BackList.Count-1); //������ ����Ʈ�� ����
        //Debug.Log("�鸮��Ʈ ũ��:" + BackList.Count);

    }

    public void StartBtn() //���θ޴� ���ӽ��۹�ư
    {
        Debug.Log("Click Start");
        FindActivePanel();
        NewGamePanel.SetActive(true);
        SceneManager.LoadScene("player");
       // Debug.Log("�鸮��Ʈ ũ��:" + BackList.Count);
    }

    public void QuitBtn() //���� ������ ��ư
    {
        Application.Quit();
        Debug.Log("Click Quit");
    }

    public void SettingBtn() //�ɼ�â ��ư
    {
        Debug.Log("Setting!");
        FindActivePanel();
        //Debug.Log("�鸮��Ʈ ũ��:" + BackList.Count);
        SettingPanel.SetActive(true);
    }

    public void HelpBtn() //����â ��ư
    {
        Debug.Log("Help!");
        FindActivePanel();
        //Debug.Log("�鸮��Ʈ ũ��:" + BackList.Count);
        HelpPanel.SetActive(true);
    }

    public void BackBtn() //�ڷΰ��� ��ư ��� ȭ���̵� �ڷΰ��� ��ư�̸� �ϴ� ���θ޴�â���� �����ϼ���
    {
        Debug.Log("Back!");
        BackESC();
    }

    public void ControlBtn()
    {
        Debug.Log("Control!");
        FindActivePanel();
        //Debug.Log("�鸮��Ʈ ũ��:" + BackList.Count);
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
       // Debug.Log("�鸮��Ʈ ũ��:" + BackList.Count);
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
       // Debug.Log("�鸮��Ʈ ũ��:" + BackList.Count);
        LanguagePanel.SetActive(true);
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
        FindActivePanel();
       // Debug.Log("�鸮��Ʈ ũ��:" + BackList.Count);
        NewGamePanel.SetActive(true);
    }

    public void LoadBtn() //�ҷ����� ��ư
    {
        Debug.Log("Load!");
    }

    public void DeleteBtn() //���� ��ư
    {
        Debug.Log("Delete!");
    }


    public void KoreanBtn()
    {
        Debug.Log("�ѱ���");
    }

    public void EnglishBtn()
    {
        Debug.Log("English");
    }
}
