using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class test : MonoBehaviour
{
    void Update()
    {

        //if(Input.)
    }

    public void MeshDump(Mesh old){
        Mesh m=new Mesh();

        m.SetVertices(old.vertices.ToList());
        m.SetNormals(old.normals.ToList());
        m.SetTriangles(old.triangles.ToList(),0);

/*
        foreach(Vector3 v in m.vertices){
            Debug.Log(v);
        }
        Debug.Log("NORMAL");
        foreach(Vector3 v in m.normals){
            Debug.Log(v);
        }
        Debug.Log("TRIANGLES");
        foreach(int v in m.triangles){
            Debug.Log(v);
        }
*/
        AssetDatabase.CreateAsset(m,"Assets/shh.asset");

    }
}
