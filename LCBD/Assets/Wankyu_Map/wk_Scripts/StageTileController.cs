using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageTileController : MonoBehaviour
{
    [Header ("Tilemap")]
    [SerializeField] private Tilemap brickTilemap;
    [SerializeField] private Tilemap ladderTilemap;
    [SerializeField] private Tilemap platformTilemap;
    [SerializeField] private Tilemap decoTilemap;

    [Header ("Tile")]
    [SerializeField] private TileBase brickTileBase;
    [SerializeField] private TileBase ladderTile;
    [SerializeField] private TileBase platformTile;
    [SerializeField] private TileBase[] newBrickTileBases;

    private StageGenerator stageGenerator;
    private MapGenerator[,] mapGenerator;

    public GameObject StartPos { get; private set; }    //스테이지 시작 지점 오브젝트
    public int StartFieldX { get; private set; }
    public int StartFieldY { get; private set; }
    public int StartMapX { get; private set; }
    public int StartMapY { get; private set; }

    public void FillPlaceTile()
    {
        brickTilemap.ClearAllTiles();

        stageGenerator = GetComponent<StageGenerator>();

        mapGenerator = stageGenerator.GetMapGenerator();

        if (mapGenerator != null)
        {
            for (int i = 0; i < mapGenerator.GetLength(0); i++)
            {
                for (int j = 0; j < mapGenerator.GetLength(1); j++)
                {
                    int[,] map = mapGenerator[i, j].Fields.Map;

                    for (int x = 0; x < map.GetLength(0); x++)
                    {
                        for (int y = 0; y < map.GetLength(1); y++)
                        {
                            Vector3Int pos = new(i * (map.GetLength(0) + 1) + x, j * (map.GetLength(1) + 1) + y, 0);

                            if (map[x, y] == 1) //(일반) 벽 블록
                            {
                                brickTilemap.SetTile(pos, brickTileBase);
                                //tilemap.SetTile(pos, newBrickTileBases[UnityEngine.Random.Range(0, newBrickTileBases.Length)]);
                            }
                            else if (map[x, y] == 2) //플랫폼 블록
                            {
                                platformTilemap.SetTile(pos, platformTile);
                            }
                            else if (map[x, y] == 3) //사다리
                            {
                                ladderTilemap.SetTile(pos, ladderTile);

                                if (y < map.GetLength(1) - 1 && map[x, y + 1] != 3)
                                {
                                    platformTilemap.SetTile(pos, platformTile);
                                }
                            }
                            else if (map[x, y] == 99)    //스테이지 시작 지점
                            {
                                StartFieldX = i;
                                StartFieldY = j;
                                StartMapX = x;
                                StartMapY = y;

                                StartPos = new GameObject("StartPos");
                                StartPos.transform.position = pos;
                                StartPos.transform.Translate(0.5f, 0.5f, 0);
                            }
                        }
                    }
                }
            }
        }
    }
}
