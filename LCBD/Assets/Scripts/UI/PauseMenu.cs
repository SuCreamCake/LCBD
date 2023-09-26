using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject PausePanel;
    public static bool GameIsPause = false;

    private SettingMenu SettingMenu; //����â

    void Start()
    {
        SettingMenu = FindObjectOfType<SettingMenu>(); //����â ������Ʈ ��������
        PausePanel.SetActive(false);
        GameIsPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (SettingMenu.SettingPanel.activeSelf)
            {
                PausePanel.SetActive(true);
            }
            if (!GameIsPause) //�����ǳ��� Ȱ��ȭ �Ǿ��ְ� �������°� �ƴҶ� ������ ������Ų��.
            {
                Pause();
            }
            else if(PausePanel.activeSelf && GameIsPause) //�������¿��� �����޴��� Ȱ��ȭ �������� ����
            {
                Resume(); //����ϱ�
            }
        }
    }

    private void Pause()
    {
        Debug.Log("����");
        Time.timeScale = 0.0f;
        GameIsPause = true;
        PausePanel.SetActive(true);
    }
    private void Resume()
    {
        Debug.Log("����ϱ�");
        PausePanel.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPause = false;
    }

    public void go_Main()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void go_Setting()
    {
        PausePanel.SetActive(false);
        SettingMenu.SettingPanel.SetActive(true);
    }

    public void go_Quit()
    {
        Debug.Log("���� ������ ��¸���");
    }
}
