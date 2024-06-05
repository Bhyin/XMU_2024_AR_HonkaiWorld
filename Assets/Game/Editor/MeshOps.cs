using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MeshOps
{
    [MenuItem("Tools/Mesh/Generate Map Block Base")]
    static void GenerateMapBlockBaseMesh()
    {
        //Vector3[] vertices = new Vector3[8];
        //Vector3[] tangents = new Vector3[8];


    }

    [MenuItem("Tools/Mesh/Generate Map Design Mesh")]
    static void GenerateMapBlockDesign()
    {
        Vector3[] vertices =
        {
            new Vector3(-1, 0, -1),
            new Vector3(-1, 0, 1),
            new Vector3(1, 0, 1),
            new Vector3(1, 0, -1)
        };

        Vector4[] tangents =
        {
            new Vector4(1, 0, 0, -1),
            new Vector4(1, 0, 0, -1),
            new Vector4(1, 0, 0, -1),
            new Vector4(1, 0, 0, -1)
        };

        Vector2[] uv =
        {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0)
        };

        int[] triangles =
        {
            0, 1, 3,
            1, 2, 3
        };

        Mesh mesh = new Mesh();
        mesh.name = "MapBlockDesign";
        mesh.vertices = vertices;
        mesh.tangents = tangents;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        MeshUtility.Optimize(mesh);
        AssetDatabase.CreateAsset(mesh, "Assets/" + mesh.name + ".asset");
        AssetDatabase.SaveAssets();
    }


}
