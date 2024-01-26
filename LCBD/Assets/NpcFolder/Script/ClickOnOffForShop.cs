using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ClickOnOffForShop : MonoBehaviour
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
                ShopManager shopManager = obj.GetComponent<ShopManager>();
                if(shopManager != null)
                {
                    // 스테이지 이름에서 숫자 부분을 추출하여 현재 스테이지 번호를 확인
                    String stageString = SceneManager.GetActiveScene().name;
                    int stageNum;

                    if (stageString.StartsWith("stage"))
                    {
                        string numberPart = stageString.Substring(5);
                        int.TryParse(numberPart, out stageNum);
                    }
                    else
                        stageNum = 0;
                    shopManager.FindShopList(gameObject.name, stageNum);
                }
            }
        }
    }
}
