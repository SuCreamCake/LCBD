using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalOnOff : MonoBehaviour
{
    GameObject Stage;
    public GameObject Bose;
    BossManager boseMng;

    void Start()
    {
        Stage = GameObject.Find("Stage");
        boseMng = Bose.GetComponent<BossManager>();
    }


    void Update()
    {
        if (boseMng.getAlive())
            Stage.SetActive(false);
        else
            Stage.SetActive(true);
    }
}
