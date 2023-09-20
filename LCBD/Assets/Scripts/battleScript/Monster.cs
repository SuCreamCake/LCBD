using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private float damage;
    private int attackType;
    //1.근접 2.원거리
    // Start is called before the first frame update
    void Start()
    {
        damage = 100f;
        attackType = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float returnDamage()
    {
        return damage;
    }

    public int returnAttackType()
    {
        return attackType;
    }
    
}
