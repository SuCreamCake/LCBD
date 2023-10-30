using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MonsterSpawnerManager : MonoBehaviour
{
    StageGenerator stageGenerator;
    MapGenerator[,] mapGenerator;

    [SerializeField] private List<GameObject> monsterList;  //몬스터 리스트  // 인스펙터에서 출현할 몬스터 집어넣으면 됨 / 몬스터 없어서 승훈이가 만든 임시 몬스터 사용

    [field: SerializeField] public GameObject MonsterSpawnerObject { get; private set; }    //몬스터 스포너 오브젝트

    [SerializeField] private Transform monsterSpawnerParent;   //몬스터 스포너 필드 별로 정리할 부모 오브젝트 트랜스폼

    public void SetSpawner()
    {
        stageGenerator = GetComponent<StageGenerator>();
        mapGenerator = stageGenerator.GetMapGenerator();

        SetMonsterSpawners();
    }

    private void SetMonsterSpawners()
    {
        if (mapGenerator != null)
        {
            // 몬스터 스포너 오브젝트 생성
            List<GameObject> parentObjects = new List<GameObject>();  //몬스터 스포너들을 필드 별로 분리하여 담을 부모 오브젝트들 리스트
            Transform playerTransform = GetComponent<StageManager>().GetPlayer().transform;

            for (int i = 0; i < mapGenerator.GetLength(0); i++)
            {
                for (int j = 0; j < mapGenerator.GetLength(1); j++)
                {
                    int[,] map = mapGenerator[i, j].Fields.Map;

                    for (int x = 0; x < map.GetLength(0); x++)
                    {
                        for (int y = 0; y < map.GetLength(1); y++)
                        {
                            if (map[x, y] == 49)   //몬스터 스폰위치 위치
                            {
                                StringBuilder parentName = new();   //몬스터 스포너들을 담을 부모 오브젝트의 이름
                                parentName.Append("Field(").Append((char)(i + '0')).Append(", ").Append((char)(j + '0')).Append(")");

                                GameObject parentObject = null;

                                foreach (var obj in parentObjects)  //부모 오브젝트 리스트를 돌면서 이미 존재하는 지 확인
                                {
                                    if (obj.name.Equals(parentName.ToString())) //이름이 이미 존재하면,
                                    {
                                        parentObject = obj; //찾아서 넣어주고
                                        break;
                                    }
                                }
                                if (parentObject == null)   //없으면,
                                {
                                    parentObject = new GameObject(parentName.ToString());   //새로 만들어줌
                                    parentObject.transform.parent = monsterSpawnerParent;   //부모도 필도포탈로 세팅해줌
                                    parentObjects.Add(parentObject);    //만들었으니까 리스트에 추가해줌
                                }

                                Vector3Int pos = new(i * (map.GetLength(0) + 1) + x, j * (map.GetLength(1) + 1) + y, 0);

                                GameObject monsterSpawnerPrefab = Instantiate(MonsterSpawnerObject, pos, new(0,0,0,0), parentObject.transform);   //필드에 맞는 부모에게 몬스터 스포너 생성
                                monsterSpawnerPrefab.transform.Translate(0.5f, 0.5f, 0);

                                //생성된 스포너 설정
                                int count = UnityEngine.Random.Range(0, 3); //스폰되는 몬스터 수 설정     / 0~2마리 스폰
                                monsterSpawnerPrefab.GetComponent<MonsterSpawner>().SetCount(count);

                                bool isWalk = (UnityEngine.Random.Range(0, 100) < 50) ? true : false;   //스폰되는 몬스터의 행동패턴이 배회인지 설정    / 50%
                                monsterSpawnerPrefab.GetComponent<MonsterSpawner>().SetPattern(isWalk);
                                
                                int selectedIndex = UnityEngine.Random.Range(0, monsterList.Count);     //몬스터리스트에서 몬스터 선택
                                monsterSpawnerPrefab.GetComponent<MonsterSpawner>().SetMonsters(monsterList[selectedIndex]);    //스폰되는 몬스터의 종류 설정   /몬스터도 없어서, 승훈이가 임시로 만들어 놓은 하나만 그냥 갖다 씀.

                                monsterSpawnerPrefab.GetComponent<MonsterSpawner>().SetTrackingTarget(playerTransform);         //스폰되는 몬스터의 타겟(플레이어로) 지정

                                monsterSpawnerPrefab.GetComponent<MonsterSpawner>().SetPos(new ObjectPoint(i, j, x, y), map.GetLength(0), map.GetLength(1));    //스포너 위치 설정
                                monsterSpawnerPrefab.GetComponent<MonsterSpawner>().Spawn();    //몬스터 스폰
                            }
                        }
                    }
                }
            }
        }
    }
}