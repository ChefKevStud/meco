using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Analytics;

public class Icosahedron : MonoBehaviour
{
    [Header("Resolution")]
    [Range(0, 5)][SerializeField] private int subdivisions = 2;

    [Header("Graphics")]
    [SerializeField] private Shader shader;

    private GameObject sphereObj;
    private Mesh sphereMesh;
    private GameObject[] triangles;

    private IcosahedronGenerator icosahedron;

    private int lastSubdivision = int.MinValue;
    
    [NotNull]
    public Material innerSphereMaterial;
    public Material triangleMaterial;
    
    public float yPositionOffset = 0;

    public void GenerateMesh()
    {
        this.name = "IcoSphere";

        if (this.sphereObj)
            Destroy(this.sphereObj);

        icosahedron = new IcosahedronGenerator();
        icosahedron.Initialize();
        icosahedron.Subdivide(subdivisions);

        this.sphereObj = new GameObject("Sphere Mesh");
        this.sphereObj.transform.parent = this.transform;
        
        this.sphereObj.AddComponent<MeshRenderer>().sharedMaterial = innerSphereMaterial;

        if (sphereMesh) sphereMesh.Clear();
        sphereMesh = new Mesh();

        int vertexCount = icosahedron.Polygons.Count * 3;
        int[] indices = new int[vertexCount];

        Vector3[] vertices = new Vector3[vertexCount];
        Vector3[] normals = new Vector3[vertexCount];

        triangles = new GameObject[icosahedron.Polygons.Count];

        for (int i = 0; i < icosahedron.Polygons.Count; i++)
        {
            var poly = icosahedron.Polygons[i];

            indices[i * 3 + 0] = i * 3 + 0;
            indices[i * 3 + 1] = i * 3 + 1;
            indices[i * 3 + 2] = i * 3 + 2;

            vertices[i * 3 + 0] = icosahedron.Vertices[poly.vertices[0]];
            vertices[i * 3 + 1] = icosahedron.Vertices[poly.vertices[1]];
            vertices[i * 3 + 2] = icosahedron.Vertices[poly.vertices[2]];
            
            createTriangle(i, sphereMesh, icosahedron.Vertices[poly.vertices[0]], icosahedron.Vertices[poly.vertices[1]], icosahedron.Vertices[poly.vertices[2]]);

            normals[i * 3 + 0] = icosahedron.Vertices[poly.vertices[0]];
            normals[i * 3 + 1] = icosahedron.Vertices[poly.vertices[1]];
            normals[i * 3 + 2] = icosahedron.Vertices[poly.vertices[2]];
        }
        sphereMesh.vertices = vertices;
        sphereMesh.normals = normals;
        sphereMesh.SetTriangles(indices, 0);

        MeshFilter terrainFilter = this.sphereObj.AddComponent<MeshFilter>();
        terrainFilter.sharedMesh = sphereMesh;
    }

    private void OnRenderObject()
    {
        innerSphereMaterial = Resources.Load("InnerSphere") as Material;
        triangleMaterial = Resources.Load("TriangleMaterial") as Material;
        
        if (subdivisions != lastSubdivision)
            GenerateMesh();

        lastSubdivision = subdivisions;

        Vector3 startPosition = transform.position;
        transform.position = new Vector3(startPosition.x, yPositionOffset + transform.lossyScale.y, startPosition.z);
    }

    private void createTriangle(int i, Mesh topMesh, Vector3 x, Vector3 y, Vector3 z)
    {
        
        Destroy(triangles[i]);
        
        triangles[i] = new GameObject($"triangle_{i}");
        
        triangles[i].transform.parent = transform;
        triangles[i].AddComponent<MeshRenderer>().sharedMaterial = triangleMaterial;
        triangles[i].AddComponent<MeshFilter>();
        
        Mesh mesh = triangles[i].GetComponent<MeshFilter>().mesh;

        Vector3[] verticesArray = {x, y, z};
        int[] meshTrianglesArray = new int[3];
        
        meshTrianglesArray[0] = 0;
        meshTrianglesArray[1] = 1;
        meshTrianglesArray[2] = 2;
        
        mesh.vertices = verticesArray;
        mesh.triangles = meshTrianglesArray;
        mesh.RecalculateNormals();
        
    }
    
}