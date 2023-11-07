using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammer1 : MonoBehaviour
{
    public GameObject player;
    Transform playerPosition;
    int key;
    Player playerScript;
    BattleManager battleManager;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        playerPosition = player.transform;
        battleManager = GameObject.Find("battleManager").GetComponent<BattleManager>();
    }

    void LateUpdate()
    {
        
        if (Input.GetKey(KeySetting.keys[KeyInput.LEFT]))
            key = -1;
        if (Input.GetKey(KeySetting.keys[KeyInput.RIGHT]))
            key = 1;


        playerPosition = player.transform;
        //카메라가 플레이어 따라가기


        if (key < 0)
        {
            spriteRenderer.flipX = false;
            transform.position = new Vector3(playerPosition.position.x - 0.3f, playerPosition.position.y - 0.3f, transform.position.z);
        }
        if (key > 0)
        {
            spriteRenderer.flipX = true;
            transform.position = new Vector3(playerPosition.position.x + 0.3f, playerPosition.position.y - 0.3f, transform.position.z);
        }

        if (!playerScript.ani.GetCurrentAnimatorStateInfo(0).IsName("childhoodStay"))
            battleManager.hammer1bool = true;
    }
}
