using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundWave : MonoBehaviour
{
    private float time;


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 1)
            Destroy(this.gameObject);
    }
}
