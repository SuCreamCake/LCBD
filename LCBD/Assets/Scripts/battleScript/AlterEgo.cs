using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterEgo : MonoBehaviour
{
    public static int AlterEgoMemeber;
    public float attackPower;
    public float attackSpeed;
    public float croosroads;


    // Start is called before the first frame update
    void Start()
    {
        attackPower = GameObject.Find("Player").GetComponent<Player>().attackPower * 0.8f;
        attackSpeed = GameObject.Find("Player").GetComponent<Player>().attackSpeed * 0.8f;
        croosroads = GameObject.Find("Player").GetComponent<Player>().crossroads * 0.8f;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
