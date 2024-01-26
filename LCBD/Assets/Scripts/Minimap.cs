using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    //맵 방문 유무
    StageGenerator stageGenerator;
    MapGenerator[,] mapGenerator;

    //미니맵
    public Image[] map;

    //미니맵 방문유무를 위한 가짜좌표
    public int x, y;

    //플레이어 위치
    public GameObject player;
    Vector2 playerPosition;

    //진짜 좌표
    Vector2 start, end;

    //미니맵 방문 유무 판단
    bool[,] visited;

    private void Awake()
    {
        player = GameObject.Find("Player");

        stageGenerator = FindObjectOfType<StageGenerator>();
        mapGenerator = stageGenerator.GetMapGenerator();
        x = 0;
        y = 0;

        start.x = 0.5f;
        start.y = 304.5f;
        end.x = 50.5f;
        end.y = 254.5f;

        visited = new bool[12, 12];
        for(int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
                visited[i, j] = false;
        }
    }

    private void Update()
    {
        playerPosition = player.transform.position;

        //지나간 미니맵
        if (visited[x, y])
            map[x * 6 + y].color = new Color(128 / 255f, 128 / 255f, 128 / 255f);

        //방문했는지 판단
        if (start.x < playerPosition.x && playerPosition.x < end.x 
            && start.y > playerPosition.y && playerPosition.y > end.y)
        {
            map[x * 6 + y].color = new Color(255, 255, 255);
            visited[x, y] = true;
        }




        y++;
        start.x += 50;
        end.x += 50;
        if (y > 6)
        {
            //가짜 좌표
            y = 0;
            x++;

            //진짜 좌표
            start.x = 0.5f;
            end.x = 50.5f;
            start.y -= 50;
            end.y -= 50;

        }
        if (x > 6)
        {
            //가짜 좌표
            x = 0;

            //진짜 좌표
            start.y = 304.5f;
            end.y = 254.5f;
        }


    }

}
