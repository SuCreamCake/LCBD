using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public GameObject player;
    Transform playerPosition;

    private void Start()
    {
        Debug.Log("asdddddda");
        player = GameObject.Find("Player");
        playerPosition = player.transform;
    }

    void LateUpdate()
    {
        playerPosition = player.transform;
        //ī�޶� �÷��̾� ���󰡱�
        transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, transform.position.z);
    }

}
