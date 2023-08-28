using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Transform object1; // ù ��° ������Ʈ
    private Transform object2; // �� ��° ������Ʈ
    private float rotationSpeed = 15.0f; // ȸ�� �ӵ�

    private Vector3 center; // �� ������Ʈ�� ����� �������� ������ �߽���
    private float radius; // �� ������Ʈ ������ �Ÿ��� ���� (������)

    private float currentAngle = 180f;
    private bool isRotating = false;

    public bool size = false;

    int randomValue = 3;

    // ������Ʈ �̸����� ���ڸ� �����ϴ� ���� �޼���
    private int GetNumberFromName(string name)
    {
        string numberStr = name.Substring(name.Length - 1); // ����: �̸��� ������ ���ڰ� ���ڷ� �����ϴ�.
        int number;
        if (int.TryParse(numberStr, out number))
        {
            return number;
        }
        return 0; // �̸��� ������ ���ڰ� ���ڰ� �ƴϰų� �̸��� ����ִ� ��� 0�� ��ȯ�մϴ�.
    }

    // CupManager�κ��� chosenCups �迭�� ���޹޾� �����ϴ� �޼���
    public void ReceiveChosenCups(GameObject[] chosenCups)
    {
        object1 = chosenCups[0].transform;
        object2 = chosenCups[1].transform;
        SoStart();
    }

    public void SoStart()
    {
        // ���� �� ���� (0 �Ǵ� 1), ȸ�� ������ ����
        randomValue = Random.Range(0, 2);

        // �� ������Ʈ ������ �߰� ������ ���
        center = (object1.position + object2.position) / 2f;

        // �� ������Ʈ ������ �Ÿ��� ������ ���������� ����
        radius = Vector3.Distance(object1.position, object2.position) / 2f;

        // ��ũ��Ʈ�� ����� ���ӿ�����Ʈ�� �ν����͸� ���� ������ �� ���� ������Ʈ �̸��� �����ɴϴ�.
        string scriptObjectName = gameObject.name;
        string object1Name = object1.name;
        string object2Name = object2.name;

        // �̸��� ���Ͽ� ����� �α׷� ����� ����մϴ�.
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
            Debug.Log("��ġ���� ����");
            isRotating = false;
        }


    }


    void Update()
    {
        if (isRotating)
        {

            if (!size) // ȸ�� ��ŸƮ�� ����(����, ������)
            {

                if (randomValue == 0)
                {
                    currentAngle += rotationSpeed;

                    if (currentAngle >= 360f)
                    {
                        // 180�� ȸ���� ��ġ�� ȸ���� ����
                        currentAngle = 360f;
                        isRotating = false;
                    }
                }
                else
                {
                    currentAngle -= rotationSpeed;

                    if (currentAngle <= 0f)
                    {
                        //180�� ȸ���� ��ġ�� ȸ���� ����
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
                        // 180�� ȸ���� ��ġ�� ȸ���� ����
                        currentAngle = 180f;
                        isRotating = false;
                    }
                }
                else
                {
                    currentAngle -= rotationSpeed;

                    if (currentAngle <= -180f)
                    {
                        // 180�� ȸ���� ��ġ�� ȸ���� ����
                        currentAngle = -180f;
                        isRotating = false;
                    }
                }
            }
            transform.position = center + new Vector3(Mathf.Cos(Mathf.Deg2Rad * currentAngle), Mathf.Sin(Mathf.Deg2Rad * currentAngle), 0) * radius;
        }
    }
}
