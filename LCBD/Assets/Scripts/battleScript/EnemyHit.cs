using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth;

    private bool isHeat;
    private bool isCrossroadThird;
    
    void Start()
    {
        isHeat = false;
        isCrossroadThird = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        if(!isHeat||isCrossroadThird)
            StartCoroutine(OnDamage(damage));
    }
    IEnumerator OnDamage(int damage)
    {
        isHeat = true;   
        this.GetComponent<SpriteRenderer>().material.color = Color.red;
        Debug.Log("EnemyHit스크립트 28번째 줄 Damage량" + damage);
        maxHealth -= damage;
        yield return new WaitForSeconds(0.01f);
        this.GetComponent<SpriteRenderer>().material.color = Color.white;
        isHeat = false;
        isCrossroadThird = false;
    }
    public void IsCrossroadThird()
    {
        isCrossroadThird = true;
    }
}
