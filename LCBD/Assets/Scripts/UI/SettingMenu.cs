using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{
    public bool GameIsPause = false;
    public GameObject ControlPanel; //����Ű �ǳ�
    public GameObject SettingPanel; //������ư ������ ������ â
    public GameObject BGMPanel; //BGM â
    public GameObject SFXPanel; //SFXâ
    public GameObject LananguagePanel;

    List<GameObject> PanelList;
    List<GameObject> BackList;

    private PauseMenu Ispause;

    private void Start()
    {
        StartPanel();
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //esc�Է½� Setting�޴��� ������̶�� ���
        {
            if(GameIsPause)
                BackESC(); //ESC�Է½� �ݱ�
        }
    }

    private void StartPanel()
    {
        PanelList = new List<GameObject>();
        BackList = new List<GameObject>();

        PanelList.Add(SettingPanel);
        PanelList.Add(ControlPanel);
        PanelList.Add(LananguagePanel);
        PanelList.Add(SFXPanel);
        PanelList.Add(BGMPanel);

        for (int i = 0; i < PanelList.Count; i++) //��� ��Ȱ��ȭ
        {
            PanelList[i].SetActive(false);
        }
    }


    public void go_Menu() //��������
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void go_ControlBtn() //��Ʈ��â ����
    {
        FindActivePanel();
        ControlPanel.SetActive(true);
    }

    public void go_Out() //������
    {
        Debug.Log("���� ������");
    }

    public void LanguageBtn() //���â���� ��ư
    {
        FindActivePanel();
        LananguagePanel.SetActive(true);
    }
    public void SFXBtn() //ȿ�������� â���� ��ư
    {
        FindActivePanel();
        SFXPanel.SetActive(true);
    }
    public void BGMBtn() //��������� â���� ��ư
    {
        FindActivePanel();
        BGMPanel.SetActive(true);
    }

    public void FullScreenBtn() //��������� â���� ��ư
    {
        Debug.Log("FullScreenBTN!!");
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

    public void BackESC() //�ڷΰ��� ��ư
    {
        for (int i = 0; i < PanelList.Count; i++) //
        {
            if (PanelList[i].activeSelf == true) //Ȱ��ȭ �Ǿ��ִ� ���� ã��
            {
                if (PanelList[0].activeSelf)
                { //����â ��ư �ǳ��̸� ����â ����
                    GameIsPause = false; //���� Ǯ��
                    Debug.Log("���� �����");
                }
                PanelList[i].SetActive(false); //Ȱ��ȭ �Ǿ��ִ� �ǳ��� ����
                Debug.Log("��Ȱ��ȭ�� �ǳ��̸�: " + PanelList[i]);
            }

        }
        if (BackList.Count != 0) //�鸮��Ʈ ī��Ʈ�� 0�� �ƴϸ� ���� ������� �迭 �����ɸ�.
        {
            BackList[BackList.Count - 1].SetActive(true); //�鸮��Ʈ�� �������ǳ� Ȱ��ȭ
            BackList.RemoveAt(BackList.Count - 1); //������ ����Ʈ�� ����
        }
    }

}
