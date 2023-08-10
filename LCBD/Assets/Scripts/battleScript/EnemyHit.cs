using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    // Start is called before the first frame update
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
    public void TakeDamage()
    {
        if(!isHeat||isCrossroadThird)
            StartCoroutine(OnDamage());
    }
    IEnumerator OnDamage()
    {
        isHeat = true;   
        this.GetComponent<SpriteRenderer>().material.color = Color.red;
        Debug.Log("EnemyHit��ũ��Ʈ 28��° ��");
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
