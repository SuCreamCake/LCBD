using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammer2 : MonoBehaviour
{
    public GameObject player;
    Transform playerPosition;


    private void Start()
    {
        player = GameObject.Find("Player");
        playerPosition = player.transform;
    }

    void LateUpdate()
    {
        playerPosition = player.transform;
        //ī�޶� �÷��̾� ���󰡱�
        transform.position = new Vector3(playerPosition.position.x - 0.2f, playerPosition.position.y -0.2f, transform.position.z);
    }
}
