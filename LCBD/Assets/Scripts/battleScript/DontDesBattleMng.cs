using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDesBattleMng : MonoBehaviour
{
    private static DontDesBattleMng instance = null;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "StartMenu")
            Destroy(this.gameObject);
    }
}
