using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulateGimmickObject : MonoBehaviour
{
    private Collider2D gimmickObjCol;

    private void Awake()
    {
        gimmickObjCol = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IControlGimmickObject controlGimmickObject = collision.GetComponent<IControlGimmickObject>();
        if (controlGimmickObject != null)
        {
            gimmickObjCol = collision;
        }
    }

    private void Update()
    {
        if (gimmickObjCol != null)
        {
            if (Input.GetKeyDown(KeySetting.keys[KeyInput.TouchNPC]))
            {
                gimmickObjCol.GetComponent<IControlGimmickObject>().ControlGimmickObject();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gimmickObjCol = null;
    }
}
