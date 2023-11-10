using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{
    public bool SettingIsUse = false;
    public GameObject ControlPanel; //����Ű �ǳ�
    public GameObject SettingPanel; //������ư ������ ������ â
    public GameObject BGMPanel; //BGM â
    public GameObject SFXPanel; //SFXâ

    List<GameObject> PanelList;
    List<GameObject> BackList;

    SoundsPlayer SFXPlayer;

    private void Awake()
    {
        SFXPlayer = GameObject.Find("SFXPlayer").GetComponent<SoundsPlayer>();
        SettingIsUse = false;
    }
    private void Start()
    {
        StartPanel();
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //esc�Է½� Setting�޴��� ������̶�� ���
        {
            if (SettingPanel.activeSelf) //�����ǳ��� ���������� ����
            {
                SettingIsUse = false;
                SettingPanel.SetActive(false);
                return;
            }
            BackESC(); //ESC�Է½� �ݱ�
        }
        //Debug.Log(BackList.Count);
    }
    public void ClickSound()
    {
        SFXPlayer.UISound(0);
    }

    private void StartPanel()
    {
        PanelList = new List<GameObject>();
        BackList = new List<GameObject>();

        PanelList.Add(SettingPanel);
        PanelList.Add(ControlPanel);
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
        ClickSound();
        FindActivePanel();
        ControlPanel.SetActive(true);
    }

    public void go_Out() //������
    {
        Debug.Log("���� ������");
    }


    public void SFXBtn() //ȿ�������� â���� ��ư
    {
        ClickSound();
        FindActivePanel();
        SFXPanel.SetActive(true);
    }
    public void BGMBtn() //��������� â���� ��ư
    {
        ClickSound();
        FindActivePanel();
        BGMPanel.SetActive(true);
    }

    public void FullScreenBtn() //��������� â���� ��ư
    {
        ClickSound();
        Debug.Log("FullScreenBTN!!");
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
        SettingIsUse = true;
    }

    public void BackESC() //�ڷΰ��� ��ư
    {
        for (int i = 0; i < PanelList.Count; i++)
        {
            if (PanelList[i].activeSelf == true) //Ȱ��ȭ �Ǿ��ִ� �ǳ� ã��
            {
                PanelList[i].SetActive(false); //���� Ȱ��ȭ �Ǿ��ִ� �ǳ��� ����
                Debug.Log("��Ȱ��ȭ�� �ǳ��̸�: " + PanelList[i]);
            }
        }

        if (BackList.Count == 0)
        { //���̻� �ڷΰ��� ������ ����
            return;
        }

        BackList[BackList.Count - 1].SetActive(true); //�鸮��Ʈ�� �������ǳ� Ȱ��ȭ
        BackList.RemoveAt(BackList.Count - 1); //������ ����Ʈ�� ����
    }

}
