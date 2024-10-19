using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammer1 : MonoBehaviour
{
    //캐릭터가 망치를 들고 다니는 애니메이션을 위한 코드
    public GameObject player;
    Transform playerPosition;
    int key;
    Player playerScript;
    Battle battle;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        playerPosition = player.transform;
        battle = GameObject.Find("BattleManager").GetComponent<Battle>();
    }

    void Update()
    {
        
        if (Input.GetKey(KeySetting.keys[KeyInput.LEFT]))
            key = -1;
        if (Input.GetKey(KeySetting.keys[KeyInput.RIGHT]))
            key = 1;


        playerPosition = player.transform;



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

        if (!playerScript.ani.GetCurrentAnimatorStateInfo(0).IsName("childhoodStay") ||
            playerScript.stage != 2)
        {
            battle.hammer1bool = true;
        }
    }
}
