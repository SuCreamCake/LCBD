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
            if (SettingMenu.SettingPanel.activeSelf)
            {
                PausePanel.SetActive(true);
            }
            if (!GameIsPause) //정지판넬이 활성화 되어있고 정지상태가 아닐때 누르면 정지시킨다.
            {
                Pause();
            }
            else if(PausePanel.activeSelf && GameIsPause) //정지상태에서 정지메뉴가 활성화 되있으면 실행
            {
                Resume(); //계속하기
            }
        }
    }

    private void Pause()
    {
        Debug.Log("정지");
        Time.timeScale = 0.0f;
        GameIsPause = true;
        PausePanel.SetActive(true);
    }
    private void Resume()
    {
        Debug.Log("계속하기");
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
        Debug.Log("게임 나가기 출력만됨");
    }
}
