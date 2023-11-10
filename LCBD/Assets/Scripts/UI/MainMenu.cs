using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel; //���θ޴�â
    public GameObject HelpMenu; //����â
    public GameObject FileLoadMenu; //���Ϸε� â
    public GameObject NewGameMenu; //������â

    private SettingMenu SettingMenu; //����â

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
            Debug.Log("���θ޴� �ڷΰ���");
            BackESC(); //ESC�Է½� �ݱ�
        }
        if (!HelpMenu.activeSelf && !FileLoadMenu.activeSelf && !NewGameMenu.activeSelf && !SettingMenu.SettingIsUse)
        { //�ٸ� �ǳڵ�� Setting�޴��� ������� �ƴҶ� Ȱ��ȭ
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


        for (int i = 0; i < PanelList.Count; i++) //��� ��Ȱ��ȭ
        {
            if (i == 0) //ù��° �����ǳڸ� Ȱ��ȭ
                PanelList[i].SetActive(true);
            else //�� �ܿ��� ��� ����
                PanelList[i].SetActive(false);
        }
    }

    private void BackESC()
    {
        for (int i = 0; i < PanelList.Count; i++) //�ǳ� ����Ʈ ����
        {
            if (PanelList[i].activeSelf == true) //Ȱ��ȭ �Ǿ��ִ� �ǳ� ã��
            {
                if (MainMenuPanel.activeSelf)
                {
                    return;
                }
                else
                {
                    PanelList[i].SetActive(false); //Ȱ��ȭ �Ǿ��ִ� �ǳ��� ����
                    Debug.Log("��Ȱ��ȭ�� �ǳ��̸�: " + PanelList[i]);
                }
            }
        }

        if (BackList.Count == 0) //�ڷΰ� �ǳ��� ������ ����
            return;
        BackList[BackList.Count - 1].SetActive(true); //�鸮��Ʈ�� �������ǳ� Ȱ��ȭ
        BackList.RemoveAt(BackList.Count - 1); //������ ����Ʈ�� ����
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
        Debug.Log("����â!");
    }

    public void GoNewGameBTN()
    {
        ClickSound();
        FindActivePanel();
        NewGameMenu.SetActive(true);
        Debug.Log("�����ӹ�ư");
    }

    public void GoFileLoadBTN()
    {
        ClickSound();
        FindActivePanel();
        FileLoadMenu.SetActive(true);
        Debug.Log("���Ϸε�â");
    }

    public void GameStartBTN()
    {
        ClickSound();
        SceneManager.LoadScene("RandomMap");
    }

    private void FindActivePanel() //��ư�� ���������� Ȱ��ȭ�� �ǳ��� ���� BackList�� �߰�
    {
        for (int i = 0; i < PanelList.Count; i++)
        {
            if (PanelList[i].activeSelf == true) //Ȱ��ȭ �Ǿ��ִ� �ǳ��� ã�´�.
            {
                BackList.Add(PanelList[i]); //Ȱ��ȭ �Ǿ��ִ� �ǳ��� BackList�� ���ʷ� �߰�
                Debug.Log("�� ����Ʈ�� �߰��� �ǳ��̸�: " + PanelList[i]);
                PanelList[i].SetActive(false); //Ȱ��ȭ �Ǿ��ִ� �ǳ��� ����.
            }
        }
    }

    public void QuitBtn() //���� ������ ��ư
    {
        ClickSound();
        Application.Quit();
        Debug.Log("Click Quit");
    }



    public void FileLoad1() //����1 ���ý�
    {
        ClickSound();
        Debug.Log("File 1 Load!");
    }

    public void LoadBtn() //�ҷ����� ��ư
    {
        ClickSound();
        Debug.Log("Load!");
    }
}
