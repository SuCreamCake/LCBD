using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Transform object1; // 첫 번째 오브젝트
    private Transform object2; // 두 번째 오브젝트
    private float rotationSpeed = 3.75f; // 회전 속도

    private Vector3 center; // 두 오브젝트의 중심점을 기준으로 회전하는 원의 중심 좌표
    private float radius; // 두 오브젝트 사이의 거리를 반지름으로 하는 원의 반지름 (반경)

    private float currentAngle = 180f;
    private bool isRotating = false;

    public bool size = false;

    int randomValue = 3;

    public void ChangerotationSpeed(float speed)
    {
        rotationSpeed = speed;
    }

    // 오브젝트 이름에서 숫자를 추출하는 메소드
    private int GetNumberFromName(string name)
    {
        string numberStr = name.Substring(name.Length - 1); // 주의: 이름의 마지막 글자를 추출합니다.
        int number;
        if (int.TryParse(numberStr, out number))
        {
            return number;
        }
        return 0; // 이름의 마지막 글자가 숫자가 아니거나 이름이 없을 경우 0을 반환합니다.
    }

    // CupManager에서 chosenCups 배열을 전달받는 메소드
    public void ReceiveChosenCups(GameObject[] chosenCups)
    {
        object1 = chosenCups[0].transform;
        object2 = chosenCups[1].transform;
        SoStart();
    }

    public void SoStart()
    {
        // 회전 방향 (0 또는 1), 회전 크기를 랜덤으로 결정
        randomValue = Random.Range(0, 2);

        // 두 오브젝트의 중심점을 계산하여 추가
        center = (object1.position + object2.position) / 2f;

        // 두 오브젝트 사이의 거리를 반지름으로 하는 원의 반지름 계산
        radius = Vector3.Distance(object1.position, object2.position) / 2f;

        // 스크립트 오브젝트의 이름과 두 오브젝트의 이름을 비교하여 회전 여부를 결정합니다.
        string scriptObjectName = gameObject.name;
        string object1Name = object1.name;
        string object2Name = object2.name;

        // 이름을 비교하여 회전을 시작합니다.
        if (scriptObjectName == object1Name || scriptObjectName == object2Name)
        {
            int scriptObjectNumber = GetNumberFromName(scriptObjectName);
            int object1Number = GetNumberFromName(object1Name);
            int object2Number = GetNumberFromName(object2Name);

            if (scriptObjectNumber < object1Number || scriptObjectNumber < object2Number)
            {
                size = false;
                currentAngle = 180f;
                isRotating = true;
            }
            else
            {
                size = true;
                currentAngle = 0f;
                isRotating = true;
            }
        }
        else
        {
            Debug.Log("위치가 일치하지 않습니다.");
            isRotating = false;
        }
    }

    void Update()
    {
        if (isRotating)
        {
            if (!size) // 회전 크기가 작은 경우 (작아지는 경우, 커지는 경우)
            {
                if (randomValue == 0)
                {
                    currentAngle += rotationSpeed;

                    if (currentAngle >= 360f)
                    {
                        // 180도 회전으로 위치가 회전한 경우
                        currentAngle = 360f;
                        isRotating = false;
                    }
                }
                else
                {
                    currentAngle -= rotationSpeed;

                    if (currentAngle <= 0f)
                    {
                        // 180도 회전으로 위치가 회전한 경우
                        currentAngle = 0f;
                        isRotating = false;
                    }
                }
            }
            else if (size)
            {
                if (randomValue == 0)
                {
                    currentAngle += rotationSpeed;

                    if (currentAngle >= 180f)
                    {
                        // 180도 회전으로 위치가 회전한 경우
                        currentAngle = 180f;
                        isRotating = false;
                    }
                }
                else
                {
                    currentAngle -= rotationSpeed;

                    if (currentAngle <= -180f)
                    {
                        // 180도 회전으로 위치가 회전한 경우
                        currentAngle = -180f;
                        isRotating = false;
                    }
                }
            }
            transform.position = center + new Vector3(Mathf.Cos(Mathf.Deg2Rad * currentAngle), Mathf.Sin(Mathf.Deg2Rad * currentAngle), 0) * radius;
        }
    }
}
