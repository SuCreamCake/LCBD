using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnOff : MonoBehaviour
{
    public List<GameObject> objectsToToggle; // 여러 개의 오브젝트를 인스펙터에서 지정

    private void OnMouseDown()
    {
        Toggle();
    }

    public void Toggle()
    {
        foreach (GameObject obj in objectsToToggle)
        {
            if (obj != null)
            {
                obj.SetActive(!obj.activeSelf); // 각 오브젝트의 활성화 상태를 반전시킴
            }
        }
    }
}
