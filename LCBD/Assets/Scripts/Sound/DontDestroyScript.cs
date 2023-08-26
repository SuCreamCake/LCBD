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
            Debug.Log("�ν��Ͻ� ����");
        }

        else if (instance != this)
        {
            Destroy(gameObject);
            Debug.Log("�ν��Ͻ� �ı�");
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

}

