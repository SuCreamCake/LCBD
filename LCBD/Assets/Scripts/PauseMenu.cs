using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //�ٸ� ��ũ��Ʈ������ ��밡���ϰ� static���� ����
    public static bool GameIsPause = false; //�޴� ���¿���
    public GameObject pauseMenuPanel;
    public GameObject SettingPanel;
    public GameObject BGMPanel;
    public GameObject SFXPanel;
    public GameObject SoundChoicePanel;

    List<GameObject> PanelList;
    List<GameObject> BackList;

    private void Start()
    {
        StartPanel();

        Resume();

    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause && pauseMenuPanel.activeSelf) //�������¿��� ����â�� ���������� ��������
                Resume();
            else if( GameIsPause && !pauseMenuPanel.activeSelf) //�������¿��� ����â�� �ƴ� �ٸ��ǳ��ϰ�� �ڷΰ��� 
                BackESC(); //ESC�Է½� �ݱ�
            else if(!GameIsPause && !pauseMenuPanel.activeSelf) //������ �������̰� �ǳ��� ����������
                Pause();
        }
            
    }

    private void StartPanel()
    {
        PanelList = new List<GameObject>();
        BackList = new List<GameObject>();

        PanelList.Add(pauseMenuPanel);
        PanelList.Add(SettingPanel);
        PanelList.Add(SoundChoicePanel);
        PanelList.Add(SFXPanel);
        PanelList.Add(BGMPanel);


        for (int i = 0; i < PanelList.Count; i++) //��� ��Ȱ��ȭ
        {
            PanelList[i].SetActive(false);
        }
    }


    public void EscPause() //esc������ ������Ű�� �޼ҵ�
    {
          if (GameIsPause == true && PanelList[0].activeSelf) //������ ���������϶� ������ ���� ����ǰ�
          {
              Resume(); //����ϱ�
          }
          else
          {
              Pause(); //�����ϱ�
          }
       
    }


    public void Resume() //����ϱ� ��Ű�� �޼ҵ�
    {
        for(int i=0; i<PanelList.Count; i++)
        {
            PanelList[i].SetActive(false);
        }
        Time.timeScale = 1f;
        GameIsPause = false;
    }


    public void Pause() //������Ű�� �޼ҵ�
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }


    public void go_Menu() //��������
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void SettingBtn() //����â ����
    {
        FindActivePanel();
        SettingPanel.SetActive(true);
    }

    public void go_Out() //������
    {
        Debug.Log("���� ������");
    }

    public void SoundBtn() //�������� â���� ��ư
    {
        FindActivePanel();
        SoundChoicePanel.SetActive(true);
    }
    public void SFXBtn() //�������� â���� ��ư
    {
        FindActivePanel();
        SFXPanel.SetActive(true);
    }
    public void BGMBtn() //�������� â���� ��ư
    {
        FindActivePanel();
        BGMPanel.SetActive(true);
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
                if (PanelList[i] == pauseMenuPanel) //���� ù �ǳ��̸� �޼ҵ� Ż��
                    return;
                PanelList[i].SetActive(false); //Ȱ��ȭ �Ǿ��ִ� �ǳ��� ����
                //Debug.Log("��Ȱ��ȭ�� �ǳ��̸�: " + PanelList[i]);
            }
        }
        BackList[BackList.Count - 1].SetActive(true); //�鸮��Ʈ�� �������ǳ� Ȱ��ȭ
        BackList.RemoveAt(BackList.Count - 1); //������ ����Ʈ�� ����
        //Debug.Log("�鸮��Ʈ ũ��:" + BackList.Count);

    }



}
