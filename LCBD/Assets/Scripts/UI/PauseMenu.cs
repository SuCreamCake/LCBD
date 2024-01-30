using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject PausePanel;
    private bool GameIsPause = false;
    private SoundsPlayer SFXPlayer;

    private SettingMenu SettingMenu; //설정창


    private void Awake()
    {
        SettingMenu = FindObjectOfType<SettingMenu>(); //설정창 오브젝트 가져오기
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
        Debug.Log("정지");
        Time.timeScale = 0.0f;
        if(SFXPlayer != null)
            SFXPlayer.SFX_Mute(true);
        PausePanel.SetActive(true);
        GameIsPause = true;
    }
    private void Resume()
    { //퍼스판넬을 끈다 / 타이머ㅓ를 1.0으로 정상으로 돌려놓는다. GameIsPause를 false로
        Debug.Log("계속하기");
        PausePanel.SetActive(false);
        if (SFXPlayer != null)
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
        Debug.Log("게임 나가기 출력만됨");
    }
}