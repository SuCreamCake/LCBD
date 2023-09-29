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
                    Debug.Log("�̰Ը�����?");
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
        MainMenuPanel.SetActive(false);
        SettingMenu.SettingPanel.SetActive(true);
        SettingMenu.SettingIsUse = true;
    }

    public void GoHelpBTN()
    {
        FindActivePanel();
        HelpMenu.SetActive(true);
        Debug.Log("����â!");
    }

    public void GoNewGameBTN()
    {
        FindActivePanel();
        NewGameMenu.SetActive(true);
        Debug.Log("�����ӹ�ư");
    }

    public void GoFileLoadBTN()
    {
        FindActivePanel();
        FileLoadMenu.SetActive(true);
        Debug.Log("���Ϸε�â");
    }

    public void GameStartBTN()
    {
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
        Application.Quit();
        Debug.Log("Click Quit");
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
