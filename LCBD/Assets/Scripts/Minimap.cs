using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    StageGenerator stageGenerator;
    MapGenerator[,] mapGenerator;

    public Image[] map;

    public int x, y;

    public GameObject player;
    Transform playerPosition;

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerPosition = player.transform;
        stageGenerator = FindObjectOfType<StageGenerator>();
        mapGenerator = stageGenerator.GetMapGenerator();
        x = 1;
        y = 5;
        
    }

    private void Update()
    {
        playerPosition = player.transform;



        map[x*6+y].color = new Color(0, 255, 0);

    }

}
