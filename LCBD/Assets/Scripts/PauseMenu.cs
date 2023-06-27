using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //다른 스크립트에서도 사용가능하게 static으로 설정
    public static bool GameIsPause = false; //메뉴 상태여부
    public GameObject pauseMenuCanvas;

    void Update()
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


    public void Resume()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }


    public void Pause()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }

}
