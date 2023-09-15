using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : MonoBehaviour
{
    private Rigidbody2D bulletRigidbody2D;
    private float bulletSpeed = 10f;
    private float distanceTime;
    private bool isDistanceOver = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody2D = GetComponent<Rigidbody2D>();
        bulletRigidbody2D.velocity = bulletSpeed * transform.right;
        if(gameObject != null)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        distanceTime += Time.deltaTime;
        if((GameObject.Find("Player").GetComponent<Player>().crossroads < distanceTime * bulletSpeed) && !isDistanceOver)
        {
            this.bulletRigidbody2D.gravityScale = 1f;
            isDistanceOver = true;
        }
    }
}
