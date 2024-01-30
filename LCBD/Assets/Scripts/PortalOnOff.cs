using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalOnOff : MonoBehaviour
{
    GameObject Stage;
    public GameObject Boss;
    BossManager boseMng;

    void Start()
    {
        Stage = GameObject.Find("Stage");
    }


    void Update()
    {
        if(boseMng == null)
        {
            boseMng = Boss.GetComponent<BossManager>();
        }
        else
        {
            if (boseMng.getAlive())
                Stage.SetActive(false);
            else
                Stage.SetActive(true);
        }
            



    }
}
