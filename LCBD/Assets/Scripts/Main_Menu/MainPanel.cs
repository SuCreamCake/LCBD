using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : MonoBehaviour
{
    private GameObject mainPanel;

    private SettingMenu setPanel;


    private void Start()
    {
        setPanel = FindObjectOfType<SettingMenu>(); //설정창 오브젝트 가져오기
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (setPanel.SettingPanel.activeSelf)
            {
                mainPanel.SetActive(true);
            }
        }
    }
}
