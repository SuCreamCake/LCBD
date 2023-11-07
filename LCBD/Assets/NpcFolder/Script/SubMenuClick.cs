using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuClick : MonoBehaviour
{
    public GameObject objectToToggle; // ����� ������Ʈ�� �ν����Ϳ��� ����

    private void OnMouseDown()
    {
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(!objectToToggle.activeSelf); // Ȱ��ȭ ���¸� ������Ŵ
        }
    }
}
