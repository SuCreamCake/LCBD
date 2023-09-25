using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject PausePanel;
    public static bool GameIsPause = false;

    private SettingMenu SettingMenu; //설정창

    void Start()
    {
        SettingMenu = FindObjectOfType<SettingMenu>(); //설정창 오브젝트 가져오기
        PausePanel.SetActive(false);
        GameIsPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!SettingMenu.GameIsPause) //정지상태가 아닐때 누르면 정지시킨다.
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
        Debug.Log("정지");
        Time.timeScale = 0.0f;
        SettingMenu.GameIsPause = true;
        PausePanel.SetActive(true);
    }
    private void Resume()
    {
        Debug.Log("계속하기");
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
        Debug.Log("게임 나가기 출력만됨");
    }
}
