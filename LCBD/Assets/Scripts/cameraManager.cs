using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public GameObject player;
    Transform playerPosition;
    void Start()
    {
        playerPosition = player.transform;
    }
    void LateUpdate()
    {   
        //ī�޶� �÷��̾� ���󰡱�
        transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, transform.position.z);
    }

}
