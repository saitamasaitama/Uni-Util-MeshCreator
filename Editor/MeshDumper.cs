using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;

[CustomEditor(typeof(MeshFilter))]
public class MeshDumper : Editor
{
  delegate void Curry(string s);
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    if (GUILayout.Button("Dump"))
    {
      MeshFilter mf = this.target as MeshFilter;
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
    if (Input.GetMouseButtonDown(0))
    {
      Debug.Log(123);
    }  //if(Input.)
  }

  public void MeshDump(Mesh old)
  {
    Mesh m = new Mesh();

    m.SetVertices(old.vertices.ToList());
    m.SetNormals(old.normals.ToList());
    m.SetTriangles(old.triangles.ToList(), 0);

    string path = EditorUtility.SaveFilePanel("save mesh", "Assets/", "mesh.obj", "obj");

    if (!string.IsNullOrEmpty(path))
    {
      //mesh2Objを呼び出す
      using (StreamWriter writer = new StreamWriter(path, false, System.Text.Encoding.UTF8))
      {
        WriteObj(writer, old);
      }
    }
  }

  private void WriteObj(TextWriter o, Mesh m)
  {
    /*
    g グループ名
usemtl マテリアル名
v x成分値 y成分値 z成分値
v x成分値 y成分値 z成分値
v x成分値 y成分値 z成分値
…（省略）…
vt x成分値 y成分値
vt x成分値 y成分値
vt x成分値 y成分値
…（省略）…
vn x成分値 y成分値 z成分値
vn x成分値 y成分値 z成分値
vn x成分値 y成分値 z成分値
…（省略）…
f 頂点座標値番号/テクスチャ座標値番号/頂点法線ベクトル番号 (多角形の頂点の数だけ続く）
f 頂点座標値番号/テクスチャ座標値番号/頂点法線ベクトル番号 (多角形の頂点の数だけ続く）
f 頂点座標値番号/テクスチャ座標値番号/頂点法線ベクトル番号 (多角形の頂点の数だけ続く
*/
    o.WriteLine($"g mesh");

    m.vertices.ToArray().ToList().ForEach(v =>
    {
      o.WriteLine($"v {v.x:0.0000000} {v.y:0.0000000} {v.z:0.0000000}");
    });

    m.normals.ToArray().ToList().ForEach(v =>
    {
      o.WriteLine($"vn {v.x:0.0000000} {v.y:0.0000000} {v.z:0.0000000}");
    });

    for (int i = 0; i < m.triangles.Length / 3; i++)
    {
      o.WriteLine($"f {m.triangles[i * 3] + 1}//{i + 1} {m.triangles[i * 3 + 1] + 1}//{i + 1} {m.triangles[i * 3 + 2] + 1}//{i + 1}");
    }
  }

}
