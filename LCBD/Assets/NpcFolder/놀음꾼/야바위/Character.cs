using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Transform object1; // 첫 번째 오브젝트
    private Transform object2; // 두 번째 오브젝트
    private float rotationSpeed = 15.0f; // 회전 속도

    private Vector3 center; // 두 오브젝트의 가운데를 기준으로 설정된 중심점
    private float radius; // 두 오브젝트 사이의 거리의 절반 (반지름)

    private float currentAngle = 180f;
    private bool isRotating = false;

    public bool size = false;

    int randomValue = 3;

    // 오브젝트 이름에서 숫자를 추출하는 보조 메서드
    private int GetNumberFromName(string name)
    {
        string numberStr = name.Substring(name.Length - 1); // 가정: 이름의 마지막 문자가 숫자로 끝납니다.
        int number;
        if (int.TryParse(numberStr, out number))
        {
            return number;
        }
        return 0; // 이름의 마지막 문자가 숫자가 아니거나 이름이 비어있는 경우 0을 반환합니다.
    }

    // CupManager로부터 chosenCups 배열을 전달받아 저장하는 메서드
    public void ReceiveChosenCups(GameObject[] chosenCups)
    {
        object1 = chosenCups[0].transform;
        object2 = chosenCups[1].transform;
        SoStart();
    }

    public void SoStart()
    {
        // 랜덤 값 생성 (0 또는 1), 회전 방향을 설정
        randomValue = Random.Range(0, 2);

        // 두 오브젝트 사이의 중간 지점을 계산
        center = (object1.position + object2.position) / 2f;

        // 두 오브젝트 사이의 거리의 절반을 반지름으로 설정
        radius = Vector3.Distance(object1.position, object2.position) / 2f;

        // 스크립트가 적용된 게임오브젝트와 인스펙터를 통해 선택한 두 개의 오브젝트 이름을 가져옵니다.
        string scriptObjectName = gameObject.name;
        string object1Name = object1.name;
        string object2Name = object2.name;

        // 이름을 비교하여 디버그 로그로 결과를 출력합니다.
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
            Debug.Log("일치하지 않음");
            isRotating = false;
        }


    }


    void Update()
    {
        if (isRotating)
        {

            if (!size) // 회전 스타트를 설정(왼쪽, 오른쪽)
            {

                if (randomValue == 0)
                {
                    currentAngle += rotationSpeed;

                    if (currentAngle >= 360f)
                    {
                        // 180도 회전을 마치면 회전을 멈춤
                        currentAngle = 360f;
                        isRotating = false;
                    }
                }
                else
                {
                    currentAngle -= rotationSpeed;

                    if (currentAngle <= 0f)
                    {
                        //180도 회전을 마치면 회전을 멈춤
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
                        // 180도 회전을 마치면 회전을 멈춤
                        currentAngle = 180f;
                        isRotating = false;
                    }
                }
                else
                {
                    currentAngle -= rotationSpeed;

                    if (currentAngle <= -180f)
                    {
                        // 180도 회전을 마치면 회전을 멈춤
                        currentAngle = -180f;
                        isRotating = false;
                    }
                }
            }
            transform.position = center + new Vector3(Mathf.Cos(Mathf.Deg2Rad * currentAngle), Mathf.Sin(Mathf.Deg2Rad * currentAngle), 0) * radius;
        }
    }
}
