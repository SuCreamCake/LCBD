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
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(false);

        stageGenerator = GetComponent<StageGenerator>();
        stageGenerator.GenerateStage();

        portalManager = GetComponent<PortalManager>();
        portalManager.SetPortal();

        stageTileController = GetComponent<StageTileController>();
        stageTileController.FillPlaceTile();

        GameObject startPos = stageTileController.StartPos;

        player.transform.position= startPos.transform.position;
        player.SetActive(true);
    }
}
