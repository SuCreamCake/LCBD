using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PolygonGeno : MonoBehaviour
{
    [SerializeField, Range(3, 100)] private int segments = 20; // 부채꼴을 구성할 세그먼트의 개수
    [SerializeField, Range(0.1f, 10f)] private float radius = 3f; // 부채꼴의 반지름
    [SerializeField, Range(0f, 360f)] private float angle = 90f; // 부채꼴의 각도

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    public Transform transformPlayer;
    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }


    private void Update()
    {
        DrawFanShape(segments, radius, angle);
        this.transform.position = transformPlayer.position;
    }

    public float ReturnAngle()
    {
        return this.angle;
    }


    public void DrawFanShape(int segments, float radius, float angle)
    {
        List<Vector3> vertexList = new List<Vector3>();

        // 중심점 추가
        vertexList.Add(Vector3.zero);

        float angleStep = Mathf.Deg2Rad * angle / (segments - 1);

        for (int i = 0; i < segments; i++)
        {
            float theta = i * angleStep;
            float x = Mathf.Cos(theta) * radius;
            float y = Mathf.Sin(theta) * radius;
            vertexList.Add(new Vector3(x, y, 0));
        }

        vertices = vertexList.ToArray();

        List<int> triangleList = new List<int>();

        for (int i = 1; i < segments; i++)
        {
            triangleList.Add(0);
            triangleList.Add(i + 1);
            triangleList.Add(i);
        }

        triangles = triangleList.ToArray();

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

}
