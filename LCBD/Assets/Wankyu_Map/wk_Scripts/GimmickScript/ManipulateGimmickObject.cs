using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulateGimmickObject : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        IControlGimmickObject controlGimmickObject = collision.GetComponent<IControlGimmickObject>();

        if (controlGimmickObject != null && Input.GetKeyDown(KeySetting.keys[KeyInput.TouchNPC]))
        {
            controlGimmickObject.ControlGimmickObject();
        }
    }
}
