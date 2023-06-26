using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Collider2D platformCollider;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), platformCollider, true);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), platformCollider, true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), platformCollider, false);
    }
}
