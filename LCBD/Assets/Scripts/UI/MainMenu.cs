using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool MainMenuIsUse = true; //�޴��� ���̴��� Ȯ��â
    public GameObject MainMenuPanel; //���θ޴�â
    public GameObject HelpMenu; //����â
    public GameObject FileLoadMenu; //���Ϸε� â
    public GameObject NewGameMenu; //������â

    private SettingMenu SettingMenu; //����â

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
        if (Input.GetKeyDown(KeyCode.Escape) && MainMenuIsUse) //esc�Է½� Setting�޴��� ������̶�� ���
        {
            BackESC(); //ESC�Է½� �ݱ�
        }

        if (SettingMenu.SettingPanel.activeSelf)
        {
            MainMenuIsUse = true; //���θ޴� ����.
            MainMenuPanel.SetActive(true);//���θ޴� Ȱ��ȭ
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


        for (int i = 0; i < PanelList.Count; i++) //��� ��Ȱ��ȭ
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
            if (PanelList[i].activeSelf == true) //Ȱ��ȭ �Ǿ��ִ� ���� ã��
            {
                if (MainMenuPanel.activeSelf) //���θ޴��� ���������� �ڷΰ��� ���
                    return;
                PanelList[i].SetActive(false); //Ȱ��ȭ �Ǿ��ִ� �ǳ��� ����
                //Debug.Log("��Ȱ��ȭ�� �ǳ��̸�: " + PanelList[i]);
            }
        }
        BackList[BackList.Count - 1].SetActive(true); //�鸮��Ʈ�� �������ǳ� Ȱ��ȭ
        BackList.RemoveAt(BackList.Count - 1); //������ ����Ʈ�� ����
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
        Debug.Log("����â!");
    }

    public void GoNewGameBTN()
    {
        FindActivePanel();
        MainMenuIsUse = false;
        NewGameMenu.SetActive(true);
        Debug.Log("�����ӹ�ư");
    }

    public void GoFileLoadBTN()
    {
        FindActivePanel();
        MainMenuIsUse = false;
        FileLoadMenu.SetActive(true);
        Debug.Log("���Ϸε�â");
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
        Debug.Log("��������");
    }
    private void FindActivePanel() //��ư�� ���������� Ȱ��ȭ�� �ǳ��� ���� BackList�� �߰�
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
