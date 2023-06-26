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
    
}
