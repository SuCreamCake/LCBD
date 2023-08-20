using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningCamera : MonoBehaviour
{
    public GameObject player;
    Transform playerPosition;

    void Awake()
    {
        //playerPosition = Transform<>();
    }

    private void Start()
    {
        Debug.Log("asdddddda");
        player = GameObject.Find("Player");
        playerPosition = player.transform;
    }

    void LateUpdate()
    {
        playerPosition = player.transform;
        //카메라가 플레이어 따라가기
        transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, transform.position.z);
    }

}
