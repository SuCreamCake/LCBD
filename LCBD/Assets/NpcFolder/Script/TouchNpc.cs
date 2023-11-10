using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchNpc : MonoBehaviour
{
    private GameObject npcSubMenu;
    private bool isSubMenuActive = false;
    private bool CheckPalyer = false;

    void Start()
    {
        CheckPalyer = false;
        // 자식 오브젝트인 NpcSubMenu를 찾아 변수에 할당
        npcSubMenu = transform.Find("NpcSubMenu").gameObject;
        npcSubMenu.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트가 플레이어 태그를 가지고 있다면
        if (other.CompareTag("Player"))
        {
            CheckPalyer = true;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPalyer = false;
            npcSubMenu.SetActive(false);
        }
    }

    void Update()
    {
         if(CheckPalyer && Input.GetKeyDown(KeyCode.G))
        {
            ToggleSubMenu();
        }
    }

    void ToggleSubMenu()
    {
        isSubMenuActive = !isSubMenuActive;

        npcSubMenu.SetActive(isSubMenuActive);
    }
}
