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
            if (!SettingMenu.GameIsPause) //�������°� �ƴҶ� ������ ������Ų��.
            {
                Pause();
            }
            else if (SettingMenu.GameIsPause && PausePanel.activeSelf)
            {
                Resume();
            }
            else if(SettingMenu.GameIsPause && SettingMenu.SettingPanel.activeSelf)
            {
                SettingMenu.SettingPanel.SetActive(false);
            }
        }
    }

    private void Pause()
    {
        Debug.Log("����");
        Time.timeScale = 0.0f;
        SettingMenu.GameIsPause = true;
        PausePanel.SetActive(true);
    }
    private void Resume()
    {
        Debug.Log("����ϱ�");
        PausePanel.SetActive(false);
        Time.timeScale = 1.0f;
        SettingMenu.GameIsPause = false;
    }

    public void go_Main()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void go_Setting()
    {
        SettingMenu.GameIsPause = true;
        SettingMenu.SettingPanel.SetActive(true);
    }

    public void go_Quit()
    {
        Debug.Log("���� ������ ��¸���");
    }
}
