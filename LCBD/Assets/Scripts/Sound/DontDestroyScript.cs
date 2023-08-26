using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyScript : MonoBehaviour
{
    public static DontDestroyScript instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("인스턴스 삽입");
        }

        else if (instance != this)
        {
            Destroy(gameObject);
            Debug.Log("인스턴스 파괴");
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

}

