using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    public RouletteController roulettes; // �귿 �迭


    void Start()
    {
        // ��ư Ŭ�� �� ResumeSpinning �޼��� ȣ��
        Button button = GetComponent<Button>();
        button.onClick.AddListener(EnterRoulette);
    }

    // ��ư Ŭ�� �� �ش� �귿 ���ߵ��� ����
    void EnterRoulette()
    {
        roulettes.StopSpinning();
    }
}
