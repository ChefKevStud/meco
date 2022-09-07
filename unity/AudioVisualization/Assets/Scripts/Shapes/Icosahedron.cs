﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icosahedron : MonoBehaviour
{
    [Header("Resolution")]
    [Range(0, 5)][SerializeField] private int subdivisions = 2;

    [Header("Graphics")]
    [SerializeField] private Shader shader;

    private GameObject sphereMesh;

    private IcosahedronGenerator icosahedron;

    private int lastSubdivision = int.MinValue;

    public void GenerateMesh()
    {
        this.name = "IcoSphere";

        if (this.sphereMesh)
            StartCoroutine(Destroy(this.sphereMesh));

        icosahedron = new IcosahedronGenerator();
        icosahedron.Initialize();
        icosahedron.Subdivide(subdivisions);

        this.sphereMesh = new GameObject("Sphere Mesh");
        this.sphereMesh.transform.parent = this.transform;

        //MeshRenderer surfaceRenderer = this.sphereMesh.AddComponent<MeshRenderer>();
        this.sphereMesh.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
        //surfaceRenderer.sharedMaterial = new Material(shader);

        Mesh sphereMesh = new Mesh();

        int vertexCount = icosahedron.Polygons.Count * 3;
        int[] indices = new int[vertexCount];

        Vector3[] vertices = new Vector3[vertexCount];
        Vector3[] normals = new Vector3[vertexCount];

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

        MeshFilter terrainFilter = this.sphereMesh.AddComponent<MeshFilter>();
        terrainFilter.sharedMesh = sphereMesh;
    }

    IEnumerator Destroy(GameObject go)
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(go);
    }

    private void Start()
    {
        /*lastSubdivision = subdivisions;
        GenerateMesh();*/
    }

    private void OnValidate()
    {
        if (subdivisions != lastSubdivision)
            GenerateMesh();

        lastSubdivision = subdivisions;
    }

    private void Update()
    {
        /*if (subdivisions != lastSubdivision)
            GenerateMesh();

        lastSubdivision = subdivisions;*/
    }

    private void createTriangle(int i, Mesh topMesh, Vector3 x, Vector3 y, Vector3 z)
    {
        GameObject meshObj = new GameObject($"triangle_{i}");
        meshObj.transform.parent = transform;

        MeshFilter meshFilter = new MeshFilter();

        meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
        
        meshFilter = meshObj.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = new Mesh();
        
        Vector3[] VerteicesArray = {x, y, z};
        int[] trianglesArray = new int[3];
        
        trianglesArray[0] = 0;
        trianglesArray[1] = 1;
        trianglesArray[2] = 2;
        
        meshFilter.sharedMesh.vertices = VerteicesArray;
        meshFilter.sharedMesh.triangles = trianglesArray;
        
    }
    
}