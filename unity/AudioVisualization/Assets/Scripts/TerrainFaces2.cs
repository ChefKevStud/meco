using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFaces2{

    Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    private float xMultiplier;
    private float yMultiplier;
    private float zMultiplier;

    public TerrainFaces2(Mesh mesh, int resolution, Vector3 localUp, float xMultiplier, float yMultiplier, float zMultiplier)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;
        this.xMultiplier = xMultiplier;
        this.yMultiplier = yMultiplier;
        this.zMultiplier = zMultiplier;

        axisA = new Vector3(localUp.y * xMultiplier, localUp.z, 1 * zMultiplier);
        //axisA = localUp;
        //Debug.Log(localUp);
        //Debug.Log(axisA);
        Debug.Log(axisA);
        axisB = Vector3.Cross( new Vector3(localUp.x, localUp.y, localUp.z), axisA);
        //axisB = new Vector3(localUp.x, localUp.y, localUp.z);
        //Debug.Log(axisB);
    }

    public void ConstructMesh()
    {
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int triIndex = 0;

        for (int y= 0; y<resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                //vertices[i] = pointOnUnitCube;
                vertices[i] = pointOnUnitSphere;

                if (x != resolution - 1 && y != resolution - 1)
                {
                    triangles[triIndex] = i;
                    triangles[triIndex + 1] = i + resolution + 1;
                    triangles[triIndex + 2] = i + resolution;

                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution + 1;
                    triIndex += 6;
                }
            }
        }
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}


