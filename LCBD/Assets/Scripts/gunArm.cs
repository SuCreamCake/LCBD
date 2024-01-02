using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunArm : MonoBehaviour
{
    public GameObject player;
    Transform playerPosition;
    float angle;
    Vector2  target, mouse;
    SpriteRenderer spriteRenderer;
    BattleManager battleManager;
    Player playerScript;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        playerPosition = player.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }



    void LateUpdate()
    {

        playerPosition = player.transform;
        //ī�޶� �÷��̾� ���󰡱�
        transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, transform.position.z);
        target = transform.position;
        mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));
        angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        float key;
        key = target.x - mouse.x;
        if (key > 0)
            spriteRenderer.flipY = true;
        else
            spriteRenderer.flipY = false;

        if (!playerScript.ani.GetCurrentAnimatorStateInfo(0).IsName("gunAttack"))
            battleManager.gunBool = true;

    }
}
