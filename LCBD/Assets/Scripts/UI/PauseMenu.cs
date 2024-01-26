using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject PausePanel;
    private bool GameIsPause = false;
    private SoundsPlayer SFXPlayer;

    private SettingMenu SettingMenu; //����â


    private void Awake()
    {
        SettingMenu = FindObjectOfType<SettingMenu>(); //����â ������Ʈ ��������
        SFXPlayer = GameObject.Find("SFXPlayer").GetComponent<SoundsPlayer>();
    }
    void Start()
    {
        PausePanel.SetActive(false);
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause && PausePanel.activeSelf && !SettingMenu.SettingIsUse)
                Resume();
            else if (!GameIsPause)
                Pause();
        }
            


    }

    private void Pause()
    {
        Debug.Log("����");
        Time.timeScale = 0.0f;
        SFXPlayer.SFX_Mute(true);
        PausePanel.SetActive(true);
        GameIsPause = true;
    }
    private void Resume()
    { //�۽��ǳ��� ���� / Ÿ�̸Ӥø� 1.0���� �������� �������´�. GameIsPause�� false��
        Debug.Log("����ϱ�");
        PausePanel.SetActive(false);
        SFXPlayer.SFX_Mute(false);
        Time.timeScale = 1.0f;
        GameIsPause = false;
    }

    public void go_Main()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void go_Setting()
    {
        SettingMenu.SettingIsUse = true;
        PausePanel.SetActive(true);
        SettingMenu.SettingPanel.SetActive(true);
    }

    public void go_Quit()
    {
        Debug.Log("���� ������ ��¸���");
    }
}
