using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
  [Range(1,10)]
  public float speed = 3;

  public List<Vector3> targets;
  private Vector3 target;
  private Vector3 dir;

  // Use this for initialization
  void Start()
  {
    target = transform.position;
    dir = Vector3.zero;
  }

  void WalkThroughTargets()
  {
    if (targets.Count > 0)
    {
      target = targets[0];
      dir = target - transform.position;
      float dist = dir.magnitude;

      if (dist < 0.1f)
      {
        targets.RemoveAt(0);
        if (targets.Count != 0)
        {
          target = targets[0];
          Debugger.Log("next target position");
        }
        else
        {
          Debugger.Log("target reached");
          target = transform.position;
          dir = Vector3.zero;
        }
      }
    }
  }

  void Update()
  {
    WalkThroughTargets();

    transform.position += dir.normalized * speed * Time.deltaTime;
  }
}
