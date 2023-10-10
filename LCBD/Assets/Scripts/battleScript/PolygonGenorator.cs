using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonGenorator : MonoBehaviour
{
    [SerializeField]
    [Range(3, 100)]
    private int polygonPoints = 3;   
    [SerializeField]
    [Min(0.1f)]
    private float outerRadius = 3;   
    [SerializeField]
    [Min(0)]
    private float innerRadius;                 
    [SerializeField]
    [Min(1)]
    private int repeatCount = 1;   

    private Mesh mesh;
    private Vector3[] vertices;       
    private int[] indices;        
    private Vector2[] uv;             

    private EdgeCollider2D edgeCollider2D;

    private void Awake()
    {
        mesh = new Mesh();

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        edgeCollider2D = gameObject.AddComponent<EdgeCollider2D>();
    }

   
    private void Update()
    {
        // innerRadius        outerRadius     Ŭ        
        innerRadius = innerRadius > outerRadius ? outerRadius - 0.1f : innerRadius;

        // innerRadius             
        if (innerRadius == 0)
        {
            DrawFilled(polygonPoints, outerRadius);
        }
        // innerRadius              
        else
        {
            DrawHollow(polygonPoints, outerRadius, innerRadius);
        }
    }

    private void DrawFilled(int sides, float radius)
    {
        //          
        vertices = GetCircumferencePoints(sides, radius);
            
        indices = DrawFilledIndices(vertices);
        
        uv = GetUVPoints(vertices, radius, repeatCount);
      
        GeneratePolygon(vertices, indices, uv);

          
        edgeCollider2D.points = GetEdgePoints(vertices);
    }

    private int[] DrawFilledIndices(Vector3[] vertices)
    {
        int triangleCount = vertices.Length - 2;
        List<int> indices = new List<int>();

        for (int i = 0; i < triangleCount; ++i)
        {
            indices.Add(0);
            indices.Add(i + 2);
            indices.Add(i + 1);
        }

        return indices.ToArray();
    }

    private void DrawHollow(int sides, float outerRadius, float innerRadius)
    {
           
        Vector3[] outerPoints = GetCircumferencePoints(sides, outerRadius);
                
        Vector3[] innerPoints = GetCircumferencePoints(sides, innerRadius);

       
        List<Vector3> points = new List<Vector3>();
        points.AddRange(outerPoints);
        points.AddRange(innerPoints);
      
        vertices = points.ToArray();
                   
        indices = DrawHollowIndices(sides);
         
        uv = GetUVPoints(vertices, outerRadius, repeatCount);
            
        GeneratePolygon(vertices, indices, uv);

                 
        List<Vector2> edgePoints = new List<Vector2>();
        edgePoints.AddRange(GetEdgePoints(outerPoints));      
        edgePoints.AddRange(GetEdgePoints(innerPoints));     
        edgeCollider2D.points = edgePoints.ToArray();
    }

    private int[] DrawHollowIndices(int sides)
    {
        List<int> indices = new List<int>();

        for (int i = 0; i < sides; ++i)
        {
            int outerIndex = i;
            int innerIndex = i + sides;

            indices.Add(outerIndex);
            indices.Add(innerIndex);
            indices.Add((outerIndex + 1) % sides);

            indices.Add(innerIndex);
            indices.Add(sides + ((innerIndex + 1) % sides));
            indices.Add((outerIndex + 1) % sides);
        }

        return indices.ToArray();
    }

    private void GeneratePolygon(Vector3[] vertices, int[] indices, Vector2[] uv)
    {
       
        mesh.Clear();
     
        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.uv = uv;
       
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

  
    private Vector3[] GetCircumferencePoints(int sides, float radius)
    {
        Vector3[] points = new Vector3[sides];
        float anglePerStep = 2 * Mathf.PI * ((float)1 / sides);

        for (int i = 0; i < sides; ++i)
        {
            Vector2 point = Vector2.zero;
            float angle = anglePerStep * i;

            point.x = Mathf.Cos(angle) * radius;
            point.y = Mathf.Sin(angle) * radius;

            points[i] = point;
        }

        return points;
    }

 
    private Vector2[] GetUVPoints(Vector3[] vertices, float outerRadius, int repeatCount)
    {
        Vector2[] points = new Vector2[vertices.Length];

        for (int i = 0; i < vertices.Length; ++i)
        {
            Vector2 point = Vector2.zero;

                   
            point.x = vertices[i].x / outerRadius * 0.5f + 0.5f;
            point.y = vertices[i].y / outerRadius * 0.5f + 0.5f;

                   
            point *= repeatCount;

            points[i] = point;
        }

        return points;
    }

   
    private Vector2[] GetEdgePoints(Vector3[] vertices)
    {
        Vector2[] points = new Vector2[vertices.Length + 1];

        for (int i = 0; i < vertices.Length; ++i)
        {
            points[i] = vertices[i];
        }

        points[points.Length - 1] = vertices[0];

        return points;
    }

}
