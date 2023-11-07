using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuClick : MonoBehaviour
{
    public GameObject objectToToggle; // 토글할 오브젝트를 인스펙터에서 지정

    private void OnMouseDown()
    {
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(!objectToToggle.activeSelf); // 활성화 상태를 반전시킴
        }
    }
}
