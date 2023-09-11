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
    private Player player;

    // Start is called before the first frame update
    void Start()
    {

        stageGenerator = GetComponent<StageGenerator>(); //스테이지를 만들어줌
        stageGenerator.GenerateStage();

        portalManager = GetComponent<PortalManager>(); //포탈을 만들어줌
        portalManager.SetPortal();

        stageTileController = GetComponent<StageTileController>(); //타일 만들어줌
        stageTileController.FillPlaceTile();

        GameObject startPos = stageTileController.StartPos; //시작 위치설정

        player=GameObject.FindObjectOfType<Player>(); //FindObject

        player.transform.position= startPos.transform.position; //플레이어 오브젝트를 시작 지점을 넣어준다.
    }
}
