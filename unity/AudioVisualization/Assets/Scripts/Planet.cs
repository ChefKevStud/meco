using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    [Range(2, 256)]
    public int resolution = 12;
    
    public float xMultiplier = 1;
    public float yMultiplier = 1;
    public float zMultiplier = 0;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    //TerrainFaces[] terrainFaces;
    TerrainFaces2[] terrainFaces;

    private int numberOfSegments = 12;

    /*public Planet(float xMultiplier, float yMultiplier, float zMultiplier)
    {
        this.xMultiplier = xMultiplier;
        this.yMultiplier = yMultiplier;
        this.zMultiplier = zMultiplier;
    }*/

    private void OnValidate()
    {
        Initialize();
        GenerateMesh();
    }

    void Initialize()
    {
        if (meshFilters == null|| meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[numberOfSegments];
        }

        terrainFaces = new TerrainFaces2[numberOfSegments];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

        for (int i = 0; i < numberOfSegments; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject($"mesh_{i}");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            terrainFaces[i] = new TerrainFaces2(meshFilters[i].sharedMesh, resolution, directions[0], xMultiplier, yMultiplier, zMultiplier);
        }
    }

    void GenerateMesh()
    {
        foreach (TerrainFaces2 face in terrainFaces)
        {
            face.ConstructMesh();
        }
    }
}
