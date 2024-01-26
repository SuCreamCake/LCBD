using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    //�� �湮 ����
    StageGenerator stageGenerator;
    MapGenerator[,] mapGenerator;

    //�̴ϸ�
    public Image[] map;

    //�̴ϸ� �湮������ ���� ��¥��ǥ
    public int x, y;

    //�÷��̾� ��ġ
    public GameObject player;
    Vector2 playerPosition;

    //��¥ ��ǥ
    Vector2 start, end;

    //�̴ϸ� �湮 ���� �Ǵ�
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

        //������ �̴ϸ�
        if (visited[x, y])
            map[x * 6 + y].color = new Color(128 / 255f, 128 / 255f, 128 / 255f);

        //�湮�ߴ��� �Ǵ�
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
            //��¥ ��ǥ
            y = 0;
            x++;

            //��¥ ��ǥ
            start.x = 0.5f;
            end.x = 50.5f;
            start.y -= 50;
            end.y -= 50;

        }
        if (x > 6)
        {
            //��¥ ��ǥ
            x = 0;

            //��¥ ��ǥ
            start.y = 304.5f;
            end.y = 254.5f;
        }


    }

}
