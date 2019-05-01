using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(MeshFilter))]
public class MeshDumper : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        if(GUILayout.Button("Dump")){
            MeshFilter mf=this.target as MeshFilter;
            Debug.Log("Dump!");
            MeshDump(mf.sharedMesh);
        }
    }
    /*
    public override void OnDrawGizmos() {

    }
    */
    void Update()
    {
      if(Input.GetMouseButtonDown(0)){
          Debug.Log(123);
      }  //if(Input.)
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
