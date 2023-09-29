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

    private void CheckDestroyScene() //씬 체크 함수
    {
        if (SceneManager.GetActiveScene().name == "Openning") //현재씬이 시작메뉴면
        {
            Destroy(this.gameObject); //해당 오브젝트를 파괴
        }
        if (SceneManager.GetActiveScene().name == "StartMenu") //현재씬이 시작메뉴면
        {
            Destroy(this.gameObject); //해당 오브젝트를 파괴
        }

    }
}
