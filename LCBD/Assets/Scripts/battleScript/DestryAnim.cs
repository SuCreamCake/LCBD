using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestryAnim : MonoBehaviour
{
    // Start is called before the first frame update

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
