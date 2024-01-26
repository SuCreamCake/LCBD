using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetPlayerMoney : MonoBehaviour
{
    private TextMeshProUGUI textMesh; // TextMeshPro 컴포넌트를 저장할 변수
    private GameObject playerObject;
    private Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player"); // 태그가 "Player"인 오브젝트 찾기
        textMesh = GetComponent<TextMeshProUGUI>();
        if (playerObject != null)
        {
            playerScript = playerObject.GetComponent<Player>(); // Player 스크립트 가져오기
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript != null)
        {
            int money = playerScript.GetMoney(); // GetMoney 메소드 실행
            if (textMesh != null)
            {
                // TextMeshPro가 있는 경우
                money = playerScript.GetMoney(); // GetMoney 메소드 호출하여 int 값을 받음
                textMesh.text = money.ToString(); // TextMeshPro 텍스트 변경
            }
        }
    }
}
