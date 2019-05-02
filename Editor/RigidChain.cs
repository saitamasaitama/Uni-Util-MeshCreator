using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;

[CustomEditor(typeof(ChainControll))]
public class RigidChain : Editor
{
  delegate void Curry<T>(T t);
  delegate void WalkCallback(Transform t);
  delegate bool executeCondition(Transform t);

  bool toggle = false;

  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();
    toggle = GUILayout.Toggle(toggle, "AAA");

    ChainControll r = this.target as ChainControll;

    if (!r.GetComponent<Rigidbody>())
    {
      r.gameObject.AddComponent<Rigidbody>();
    }

    if (GUILayout.Button("CreateChain"))
    {
      /*
        子供たち全員にrigidbodyをつける
       */
      this.RecursiveChildren(r.transform,
      t =>
      {
        Debug.Log($"BEGIN {t.name}");
        Rigidbody rigid = t.gameObject.GetComponent<Rigidbody>();
        if (!rigid)
        {
          rigid = t.gameObject.AddComponent<Rigidbody>();
        }
        if (0 < t.childCount && !t.gameObject.GetComponent<HingeJoint>())
        {
          //子rigidbodyにつなぐ
          for (int i = 0; i < t.childCount; i++)
          {
            HingeJoint hinge = t.gameObject.AddComponent<HingeJoint>();
            Rigidbody childRigid = t.GetChild(i).GetComponent<Rigidbody>();
            hinge.connectedBody = childRigid;
            hinge.useLimits = true;

            JointLimits limit = new JointLimits();
            limit.max = r.angleLimit;
            hinge.limits = limit;
          }
        }
        /*
          sphereコライダーをつける
         */
        SphereCollider sphere = t.gameObject.AddComponent<SphereCollider>();
        sphere.radius = 0.03f;

      },
      t => true
      );
    }
    if (GUILayout.Button("RemoveChain"))
    {
      Debug.Log("Delete");
      this.RecursiveChildren(r.transform, t =>
      {

        foreach (Collider c in t.gameObject.GetComponents<Collider>())
        {
          Object.DestroyImmediate(c);
        }

        foreach (Joint j in t.gameObject.GetComponents<Joint>())
        {
          Object.DestroyImmediate(j);
        }

        if (t.gameObject.GetComponent<Rigidbody>())
        {
          Object.DestroyImmediate(t.gameObject.GetComponent<Rigidbody>());
        }


      }, t => true);
    }
  }

  private bool ForceTrue(Transform t)
  {
    return true;
  }

  private void RecursiveChildren(Transform t,
    WalkCallback walk,
    executeCondition condition
    )
  {
    for (int i = 0; i < t.childCount; i++)
    {
      RecursiveChildren(t.GetChild(i), walk, condition);
    }
    if (condition(t))
    {
      walk(t);
    }
  }
}
