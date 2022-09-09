using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcosahedronGenerator
{
    private List<Polygon> polygons;
    private List<Vector3> vertices;

    public List<Polygon> Polygons { get => polygons; private set => polygons = value; }
    public List<Vector3> Vertices { get => vertices; private set => vertices = value; }

    public void Initialize()
    {
        polygons = new List<Polygon>();
        vertices = new List<Vector3>();

        float t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;

        vertices.Add(new Vector3(-1, t, 0).normalized);
        vertices.Add(new Vector3(1, t, 0).normalized);
        vertices.Add(new Vector3(-1, -t, 0).normalized);
        vertices.Add(new Vector3(1, -t, 0).normalized);
        vertices.Add(new Vector3(0, -1, t).normalized);
        vertices.Add(new Vector3(0, 1, t).normalized);
        vertices.Add(new Vector3(0, -1, -t).normalized);
        vertices.Add(new Vector3(0, 1, -t).normalized);
        vertices.Add(new Vector3(t, 0, -1).normalized);
        vertices.Add(new Vector3(t, 0, 1).normalized);
        vertices.Add(new Vector3(-t, 0, -1).normalized);
        vertices.Add(new Vector3(-t, 0, 1).normalized);
        
        polygons.Add(new Polygon(0, 11, 5));
        polygons.Add(new Polygon(0, 5, 1));
        polygons.Add(new Polygon(0, 1, 7));
        polygons.Add(new Polygon(0, 7, 10));
        polygons.Add(new Polygon(0, 10, 11));
        polygons.Add(new Polygon(1, 5, 9));
        polygons.Add(new Polygon(5, 11, 4));
        polygons.Add(new Polygon(11, 10, 2));
        polygons.Add(new Polygon(10, 7, 6));
        polygons.Add(new Polygon(7, 1, 8));
        polygons.Add(new Polygon(3, 9, 4));
        polygons.Add(new Polygon(3, 4, 2));
        polygons.Add(new Polygon(3, 2, 6));
        polygons.Add(new Polygon(3, 6, 8));
        polygons.Add(new Polygon(3, 8, 9));
        polygons.Add(new Polygon(4, 9, 5));
        polygons.Add(new Polygon(2, 4, 11));
        polygons.Add(new Polygon(6, 2, 10));
        polygons.Add(new Polygon(8, 6, 7));
        polygons.Add(new Polygon(9, 8, 1));
    }

    public void Subdivide(int recursions)
    {
        var midPointCache = new Dictionary<int, int>();

        for (int i = 0; i < recursions; i++)
        {
            var newPolys = new List<Polygon>();
            foreach (var poly in polygons)
            {
                int a = poly.vertices[0];
                int b = poly.vertices[1];
                int c = poly.vertices[2];
                
                int ab = GetMidPointIndex(midPointCache, a, b);
                int bc = GetMidPointIndex(midPointCache, b, c);
                int ca = GetMidPointIndex(midPointCache, c, a);
                
                newPolys.Add(new Polygon(a, ab, ca));
                newPolys.Add(new Polygon(b, bc, ab));
                newPolys.Add(new Polygon(c, ca, bc));
                newPolys.Add(new Polygon(ab, bc, ca));
            }
            polygons = newPolys;
        }
    }

    public int GetMidPointIndex(Dictionary<int, int> cache, int indexA, int indexB)
    {
        int smallerIndex = Mathf.Min(indexA, indexB);
        int greaterIndex = Mathf.Max(indexA, indexB);
        int key = (smallerIndex << 16) + greaterIndex;
        
        int ret;
        if (cache.TryGetValue(key, out ret))
            return ret;
        
        Vector3 p1 = vertices[indexA];
        Vector3 p2 = vertices[indexB];
        Vector3 middle = Vector3.Lerp(p1, p2, 0.5f).normalized;

        ret = vertices.Count;
        vertices.Add(middle);

        cache.Add(key, ret);
        return ret;
    }

}