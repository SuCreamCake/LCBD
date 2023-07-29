using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageTileController : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tileBase;

    public TileBase[] newBrickTileBases;

    private StageGenerator stageGenerator;
    private MapGenerator[,] mapGenerator;

    [SerializeField]
    private int fieldSquareMatrixRow;

    [SerializeField]
    private int mapWidth, mapHeight;

    void Start()
    {
        stageGenerator = GetComponent<StageGenerator>();
        mapGenerator = stageGenerator.GetMapGenerator();

        fieldSquareMatrixRow = stageGenerator.FieldSquareMatrixRow;
        mapWidth = stageGenerator.MapWidth;
        mapHeight = stageGenerator.MapHeight;

        FillPlaceTile();
    }

    //임시 디버그
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))   // '=' 키
        {
            tilemap.ClearAllTiles();
            FillPlaceTile();
        }
    }


    void FillPlaceTile()
    {
        mapGenerator = stageGenerator.GetMapGenerator();

        if (mapGenerator != null)
        {
            for (int i = 0; i < mapGenerator.GetLength(0); i++)
            {
                for (int j = 0; j < mapGenerator.GetLength(1); j++)
                {
                    int[,] map = mapGenerator[i, j].Fields.Map;

                    for (int x = 0; x < mapGenerator[i, j].Fields.Map.GetLength(0); x++)
                    {
                        for (int y = 0; y < mapGenerator[i, j].Fields.Map.GetLength(1); y++)
                        {
                            if (mapGenerator[i, j].Fields.Map[x, y] == 1)
                            {
                                Vector3Int pos = new Vector3Int(i * (mapGenerator[i, j].Fields.Map.GetLength(0) + 1) + x, j * (mapGenerator[i, j].Fields.Map.GetLength(1) + 1) + y);

                                tilemap.SetTile(pos, tileBase);
                                //tilemap.SetTile(pos, newBrickTileBases[UnityEngine.Random.Range(0, newBrickTileBases.Length)]);
                            }
                        }
                    }
                }
            }
        }
    }
}
