using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    
    public void OnClickStartBtn()
    {
        Debug.Log("Click Start");
        SceneManager.LoadScene("Player");
    }

    public void OnClickQuitBtn()
    {
        Application.Quit();
        Debug.Log("Click Quit");
    }

    public void OnclickSkipBtn()
    {
        Debug.Log("Skip!");
        SceneManager.LoadScene("StartMenu");
    }
    
}
