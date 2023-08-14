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

    private void Awake()
    {
        pauseMenuPanel.SetActive(false); //���� ���۽ÿ��� Ȱ��ȭx
    }

    void Update()
    {
        EscPause(); //esc������ ����

        
    }

    public void EscPause() //esc������ ������Ű�� �޼ҵ�
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


    public void Resume() //����ϱ� ��Ű�� �޼ҵ�
    {
        pauseMenuPanel.SetActive(false);
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
        Debug.Log("��������");
        SceneManager.LoadScene("StartMenu");
    }

    public void go_Setting() //����
    {
        Debug.Log("����â!");
    }

    public void go_Out() //������
    {
        Debug.Log("���� ������");
    }







}
