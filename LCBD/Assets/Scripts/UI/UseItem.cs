using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public List<Item> item;

    private void Update()
    {
        useItem1();
    }

    private void useItem1()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            item[0].print();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            item[1].print();
        }
    }
}
