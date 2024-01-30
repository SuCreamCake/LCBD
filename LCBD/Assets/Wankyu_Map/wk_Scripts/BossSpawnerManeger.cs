using UnityEngine;

public class BossSpawnerManeger : MonoBehaviour
{
    public static class BossSpawnLocation  // 보스 스폰 위치 좌표 static 클래스
    {
        public class Point
        {
            public int x, y;
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public static Point Boss1 = new Point(25, 25);
    }

    StageGenerator stageGenerator;
    MapGenerator[,] mapGenerator;

    [SerializeField] GameObject portalOnOff_Transform;

    [field: SerializeField] public GameObject BossObject { get; private set; }    //보스 오브젝트

    public void SpawnBoss()
    {
        stageGenerator = GetComponent<StageGenerator>();
        mapGenerator = stageGenerator.GetMapGenerator();

        if (mapGenerator != null)
        {
            for (int i = 0; i < mapGenerator.GetLength(0); i++)
            {
                for (int j = 0; j < mapGenerator.GetLength(1); j++)
                {
                    int[,] map = mapGenerator[i, j].Fields.Map;

                    FieldType FieldFIeldType = mapGenerator[i, j].Fields.FieldFIeldType;
                    if (FieldFIeldType == FieldType.Boss)
                    {
                        Vector3Int bossLocation = GetBossLocation(mapGenerator[i, j].Fields.StageLevel);

                        Vector3Int pos = new(i * (map.GetLength(0) + 1) + bossLocation.x, j * (map.GetLength(1) + 1) + bossLocation.y, 0);

                        GameObject boss = Instantiate(BossObject, BossObject.transform.position, BossObject.transform.rotation);    // 보스 배치.
                        boss.transform.position = pos;
                        boss.transform.Translate(0.5f, 0.5f, 0);

                        portalOnOff_Transform.GetComponent<PortalOnOff>().Boss = boss;    // 스테이지 포탈 onoff를 위해 보스를 넣어줌.
                    }
                }
            }
        }
    }

    private Vector3Int GetBossLocation(int stageLevel)
    {
        Vector3Int pos = Vector3Int.zero;
        switch (stageLevel)
        {
            case 1:
                pos = new Vector3Int(BossSpawnLocation.Boss1.x, BossSpawnLocation.Boss1.y);
                break;

            case 2: // 1스테이지 보스 위치로.
                pos = new Vector3Int(BossSpawnLocation.Boss1.x, BossSpawnLocation.Boss1.y);
                break;

            default:
                break;
        }

        return pos;
    }
}
