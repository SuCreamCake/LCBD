using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyinGameUI : MonoBehaviour
{
    public static DontDestroyinGameUI instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }

    private void Update()
    {
        CheckDestroyScene();
    }

    private void CheckDestroyScene() //�� üũ �Լ�
    {
        if (SceneManager.GetActiveScene().name == "Openning") //������� ���۸޴���
        {
            Destroy(this.gameObject); //�ش� ������Ʈ�� �ı�
        }
        if (SceneManager.GetActiveScene().name == "StartMenu") //������� ���۸޴���
        {
            Destroy(this.gameObject); //�ش� ������Ʈ�� �ı�
        }

    }
}
