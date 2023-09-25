using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingDontDestroy : MonoBehaviour
{
    public static SettingDontDestroy instance = null;

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
}
