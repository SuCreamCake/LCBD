using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    PlayerTracking PlayerTracking;
    FieldOfView FieldOfView;
    EnemyMove EnemyMove;

    bool once = false;
    bool findPlayer = false;

    public int MonsterID;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerTracking = GetComponent<PlayerTracking>();
        EnemyMove = GetComponent<EnemyMove>();
        FieldOfView = GetComponent<FieldOfView>();
    }

    private void Start()
    {
        PlayerTracking.enabled = false;
    }

    void Update()
    {
        
    }

    public void AppearPlayer()
    {
        EnemyMove.StopThink();
        EnemyMove.enabled = false;
        PlayerTracking.enabled = true;
        once = false;

        findPlayer = true;

    }
    public void DisAppearPlayer()
    {
        if (!once && MonsterID < 1000)
        {
            PlayerTracking.noPlayer = false;
        }
    }

    public void SearchMode()
    {
        EnemyMove.enabled = true;
        PlayerTracking.enabled = false;
        once = true;
        if (findPlayer)
        {
            EnemyMove.SearchPalyer();
        }
    }
}
