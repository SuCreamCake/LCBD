using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //�ٸ� ��ũ��Ʈ������ ��밡���ϰ� static���� ����
    public static bool GameIsPause = false; //�޴� ���¿���
    public GameObject pauseMenuCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause == true) //������ ���������϶� ������ ���� ����ǰ�
            {
                Resume(); //����ϱ�
            }
            else
            {
                Pause(); //�����ϱ�
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
