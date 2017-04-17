using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
  [Range(1, 10)]
  public float speed = 3;

  private Queue<Vector2> targets = new Queue<Vector2>();
  private Vector3 target;
  private Vector3 dir;

  // Use this for initialization
  void Start()
  {
    target = transform.position;
    dir = Vector3.zero;
  }

  float dist = 0;
  bool move = false;
  IEnumerator WalkThroughTargets()
  {
    while (dist > 0.1f)
    {
      dir = target - transform.position;
      dist = dir.magnitude;
      yield return null;
    }

    if (targets.Count != 0)
    {
      target = targets.Dequeue();
      dir = target - transform.position;
      dist = dir.magnitude;
      Debugger.Log("next target position");
    }
    else
    {
      if(move)
        Debugger.Log("target reached");
      move = false;
      dist = 0;
      dir = Vector3.zero;
    }

  }

  public void ResetTarget(Queue<Vector2> _targets)
  {
    targets = _targets;
    move = true;
    dist = 0;
  }

  void Update()
  {
    if (targets.Count > 0)
      move = true;

    if (move)
      StartCoroutine(WalkThroughTargets());
    
    transform.position += dir.normalized * Time.deltaTime * speed;
  }
}
