using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathEvent : MonoBehaviour
{
    Player player;
    GameObject deathEvent;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        deathEvent = GameObject.Find("deathEvent");

    }

    private void Update()
    {
        if (player.health > 0)
            deathEvent.SetActive(false);
        else
            deathEvent.SetActive(true);
    }
}
