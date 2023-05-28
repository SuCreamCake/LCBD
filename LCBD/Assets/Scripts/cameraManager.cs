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
        //카메라가 플레이어 따라가기
        transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, transform.position.z);
    }

}
