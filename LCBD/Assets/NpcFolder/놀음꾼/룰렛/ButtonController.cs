using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    public RouletteController roulettes; // 룰렛 배열


    void Start()
    {
        // 버튼 클릭 시 ResumeSpinning 메서드 호출
        Button button = GetComponent<Button>();
        button.onClick.AddListener(EnterRoulette);
    }

    // 버튼 클릭 시 해당 룰렛 멈추도록 설정
    void EnterRoulette()
    {
        roulettes.StopSpinning();
    }
}
