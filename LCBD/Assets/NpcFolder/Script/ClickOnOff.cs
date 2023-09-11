using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnOff : MonoBehaviour
{
    public List<GameObject> objectsToToggle; // ���� ���� ������Ʈ�� �ν����Ϳ��� ����

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
                obj.SetActive(!obj.activeSelf); // �� ������Ʈ�� Ȱ��ȭ ���¸� ������Ŵ
            }
        }
    }
}
