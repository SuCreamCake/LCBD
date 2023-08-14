using UnityEngine;
using System.Collections;

public class RouletteController : MonoBehaviour {

	float rotSpeed = 0.1f; // 회전속도
    bool isSpinning = true; // 회전 중인지 여부


    void Start() {
	}

	void Update() {

        if (isSpinning)
        {
            // 회전 속도만큼 룰렛을 회전 시킨다
            transform.Rotate(0, 0, this.rotSpeed);
        }
    }

    // 룰렛 회전을 멈추는 메서드
    public void StopSpinning()
    {
        this.rotSpeed = 0;
        isSpinning = false;

        // Rotation의 Z값이 0에서 51.5 사이인지 체크하여 디버그 출력
        float currentRotationZ = transform.eulerAngles.z;
        if (currentRotationZ >= 0 && currentRotationZ <= 51.5f)
        {
            Debug.Log("성공");
        }
        else
        {
            Debug.Log("실패");
        }
    }

    // 룰렛 회전을 재개하는 메서드
    public void ResumeSpinning()
    {
        isSpinning = true;
        this.rotSpeed = 0.1f;
    }

    public void EnterButton()
    {
        if (isSpinning)
        {
            StopSpinning();
        }
        else
        {
            ResumeSpinning();
        }
    }
}