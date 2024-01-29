using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class GimmickObjectLocation  // 기믹 오브젝트 위치 좌표 static 클래스
{
    public class Point
    {
        public int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public static Point CommonField_1_BabyBottle_GimmickObject = new Point(20, 16);
    public static Point CommonField_1_Mom_GimmickObject = new Point(12, 32);
    public static Point CommonField_1_Standing_GimmickObject = new Point(25, 24);

    public static Point SpecialField_1_Cradle_Object = new Point(20, 19);
    public static Point SpecialField_1_Stroller_Object = new Point(0, 0);
}

public class GimmickObjectPlaceManager : MonoBehaviour
{
    StageGenerator stageGenerator;
    MapGenerator[,] mapGenerator;

    [field: SerializeField] public GameObject BabyBottle_GimmickObject { get; private set; }
    [field: SerializeField] public GameObject Mom_GimmickObject { get; private set; }
    [field: SerializeField] public GameObject Standing_GimmickObject { get; private set; }

    [field: SerializeField] public GameObject Cradle_Object { get; private set; }
    [field: SerializeField] public GameObject Stroller_Object { get; private set; }

    [field: SerializeField] public Transform GimmickObjectsParent { get; private set; } //부모 오브젝트 트랜스폼

    public void SetGimmickObject()
    {
        stageGenerator = GetComponent<StageGenerator>();
        mapGenerator = stageGenerator.GetMapGenerator();

        FieldType[,] fieldTypes = stageGenerator.GetFieldType();

        if (mapGenerator != null)
        {
            // 기믹 오브젝트 생성
            List<GameObject> parentObjects = new List<GameObject>();  // 부모 오브젝트들 리스트

            for (int i = 0; i < fieldTypes.GetLength(0); i++)
            {
                for (int j = 0; j < fieldTypes.GetLength(1); j++)
                {
                    StringBuilder parentName = new();   // 기믹 오브젝트들을 담을 부모 오브젝트의 이름
                    parentName.Append("Field(").Append((char)(i + '0')).Append(", ").Append((char)(j + '0')).Append(")");

                    GameObject parentObject = null;

                    foreach (var obj in parentObjects)  // 부모 오브젝트 리스트를 돌면서 이미 존재하는 지 확인
                    {
                        if (obj.name.Equals(parentName.ToString())) // 이름이 이미 존재하면,
                        {
                            parentObject = obj; // 찾아서 넣어주고
                            break;
                        }
                    }
                    if (parentObject == null)   // 없으면,
                    {
                        parentObject = new GameObject(parentName.ToString());   // 새로 만들어줌
                        parentObject.transform.parent = GimmickObjectsParent;   // 부모도 필도포탈로 세팅해줌
                        parentObjects.Add(parentObject);    // 만들었으니까 리스트에 추가해줌
                    }

                    if (fieldTypes[i, j] == FieldType.Common)   // 일반 필드 기믹 오브젝트 배치.
                    {
                        switch (stageGenerator.StageLevel)
                        {
                            case 1:
                                SetGimmickObject_Stage1(i, j, parentObject.transform);
                                break;

                            case 2:
                                SetGimmickObject_Stage1(i, j, parentObject.transform);  // 임시로 1스테이지
                                break;
                        }
                    }
                    if (fieldTypes[i, j] == FieldType.Special)  // 특수 필드 오브젝트 배치.
                    {
                        switch (stageGenerator.StageLevel)
                        {
                            case 1:
                                SetSpecialFieldObject_Stage1(i, j, parentObject.transform);
                                break;

                            case 2:
                                SetSpecialFieldObject_Stage1(i, j, parentObject.transform); // 임시로 1스테이지.
                                break;
                        }
                    }
                }
            }
        }

    }


    // 1스테이지 일반 필드 기믹 오브젝트 배치.
    private void SetGimmickObject_Stage1(int i, int j, Transform parentMap)
    {
        int width = mapGenerator[i, j].Fields.Map.GetLength(0);
        int height = mapGenerator[i, j].Fields.Map.GetLength(1);

        int x;
        int y;

        Vector3Int pos;
        GameObject gimmickObjectPrefab;

        switch ((CommonFieldSerial_1)mapGenerator[i, j].Fields.Serial)
        {
            case CommonFieldSerial_1.BabyBottle_g:

                x = GimmickObjectLocation.CommonField_1_BabyBottle_GimmickObject.x;
                y = GimmickObjectLocation.CommonField_1_BabyBottle_GimmickObject.y;

                pos = new(i * (width + 1) + x, j * (height + 1) + y, 0);

                gimmickObjectPrefab = Instantiate(BabyBottle_GimmickObject, pos, new(0, 0, 0, 0), parentMap);
                gimmickObjectPrefab.transform.Translate(0.5f, 0.5f, 0);

                break;

            case CommonFieldSerial_1.Mom_g:

                x = GimmickObjectLocation.CommonField_1_Mom_GimmickObject.x;
                y = GimmickObjectLocation.CommonField_1_Mom_GimmickObject.y;

                pos = new(i * (width + 1) + x, j * (height + 1) + y, 0);

                gimmickObjectPrefab = Instantiate(Mom_GimmickObject, pos, new(0, 0, 0, 0), parentMap);
                gimmickObjectPrefab.transform.Translate(0.5f, 0.5f, 0);

                break;

            case CommonFieldSerial_1.Standing_g:
                x = GimmickObjectLocation.CommonField_1_Standing_GimmickObject.x;
                y = GimmickObjectLocation.CommonField_1_Standing_GimmickObject.y;

                pos = new(i * (width + 1) + x, j * (height + 1) + y, 0);

                gimmickObjectPrefab = Instantiate(Standing_GimmickObject, pos, new(0, 0, 0, 0), parentMap);
                gimmickObjectPrefab.transform.Translate(0.5f, 0.5f, 0);

                break;
        }
    }

    // 1스테이지 특수 필드 오브젝트 배치.
    private void SetSpecialFieldObject_Stage1(int i, int j, Transform parentMap)
    {
        int width = mapGenerator[i, j].Fields.Map.GetLength(0);
        int height = mapGenerator[i, j].Fields.Map.GetLength(1);

        int x;
        int y;

        Vector3Int pos;
        GameObject gimmickObjectPrefab;

        switch ((SpecialFieldSerial_1)mapGenerator[i, j].Fields.Serial)
        {
            case SpecialFieldSerial_1.Cradle:

                x = GimmickObjectLocation.SpecialField_1_Cradle_Object.x;
                y = GimmickObjectLocation.SpecialField_1_Cradle_Object.y;

                pos = new(i * (width + 1) + x, j * (height + 1) + y, 0);

                gimmickObjectPrefab = Instantiate(Cradle_Object, pos, new(0, 0, 0, 0), parentMap);
                gimmickObjectPrefab.transform.Translate(0.5f, 0.5f, 0);

                break;

            case SpecialFieldSerial_1.Stroller:

                x = GimmickObjectLocation.CommonField_1_Mom_GimmickObject.x;
                y = GimmickObjectLocation.CommonField_1_Mom_GimmickObject.y;

                pos = new(i * (width + 1) + x, j * (height + 1) + y, 0);

                gimmickObjectPrefab = Instantiate(Mom_GimmickObject, pos, new(0, 0, 0, 0), parentMap);
                gimmickObjectPrefab.transform.Translate(0.5f, 0.5f, 0);

                break;

        }
    }
}
