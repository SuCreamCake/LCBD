using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{

    [SerializeField]
    private StageGenerator stageGenerator;
    [SerializeField]
    private StageTileController stageTileController;
    [SerializeField]
    private PortalManager portalManager;
    [SerializeField]
    private MonsterSpawnerManager monsterSpawnerManager;
    [SerializeField]
    private GimmickObjectPlaceManager gimmickObjectManager;
    [SerializeField]
    private BossSpawnerManeger bossSpawnManager;

    private Player player;
    public Player GetPlayer() { return player; }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>(); //FindObject    플레이어 오브젝트 가져오기

        stageGenerator = GetComponent<StageGenerator>(); //스테이지를 만들어줌
        stageGenerator.GenerateStage();

        portalManager = GetComponent<PortalManager>(); //포탈을 만들어줌
        portalManager.SetPortal();

        monsterSpawnerManager = GetComponent<MonsterSpawnerManager>();  //몬스터 스포너를 만들어서 몬스터 스포너를 통해 몬스터를 만들어줌
        monsterSpawnerManager.SetSpawner();

        gimmickObjectManager = GetComponent<GimmickObjectPlaceManager>(); //기믹 오브젝트 배치
        gimmickObjectManager.SetGimmickObject();

        stageTileController = GetComponent<StageTileController>(); //타일 만들어줌
        stageTileController.FillPlaceTile();

        bossSpawnManager = GetComponent<BossSpawnerManeger>();  // 보스 만들어줌. 
        bossSpawnManager.SpawnBoss();

        GameObject startPos = stageTileController.StartPos; //시작 위치설정

        player.transform.position= startPos.transform.position; //플레이어 오브젝트를 시작 지점을 넣어준다.
    }
}
