using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransform;

    private Vector2 offset;
    private float limitMinX, limitMaxX, limitMinY, limitMaxY;
    private float cameraHalfWidth, cameraHalfHeight;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.transform;

        offset = new Vector2(0, 1);

        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
    }

    private void Update()
    {
        // 지금은 Update에 있지만, 포탈을 탈 때로 바꾸면 좋을 수도. 
        int x = (int)(playerTransform.position.x / (50 + 1));
        int y = (int)(playerTransform.position.y / (50 + 1));

        limitMinX = x * (50 + 1);
        limitMaxX = limitMinX + 50;

        limitMinY = y * (50 + 1);
        limitMaxY = limitMinY + 50;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(playerTransform.position.x + offset.x, limitMinX + cameraHalfWidth, limitMaxX - cameraHalfWidth),   // X
            Mathf.Clamp(playerTransform.position.y + offset.y, limitMinY + cameraHalfHeight, limitMaxY - cameraHalfHeight), // Y
            -10);                                                                                                           // Z

        transform.position = desiredPosition;
    }
}
