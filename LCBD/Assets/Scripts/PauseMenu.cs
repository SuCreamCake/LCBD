using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //다른 스크립트에서도 사용가능하게 static으로 설정
    public static bool GameIsPause = false; //메뉴 상태여부
    public GameObject pauseMenuPanel;

    private void Awake()
    {
        pauseMenuPanel.SetActive(false); //게임 시작시에는 활성화x
    }

    void Update()
    {
        EscPause(); //esc누르면 정지

        
    }

    public void EscPause() //esc누르면 정지시키는 메소드
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause == true) //게임이 정지상태일때 누르면 게임 실행되게
            {
                Resume(); //계속하기
            }
            else
            {
                Pause(); //정지하기
            }
        }
    }


    public void Resume() //계속하기 시키는 메소드
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }


    public void Pause() //정지시키는 메소드
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }


    public void go_Menu() //메인으로
    {
        Debug.Log("메인으로");
        SceneManager.LoadScene("StartMenu");
    }

    public void go_Setting() //설정
    {
        Debug.Log("설정창!");
    }

    public void go_Out() //나가기
    {
        Debug.Log("게임 종료함");
    }







}
