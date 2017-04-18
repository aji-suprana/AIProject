using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
  [Range(1, 10)]
  public float speed = 3;

  public delegate void finishMove(bool b);
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
    }
    else
    {
      if (move)
      {
        //targetReached
      }
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
  public int GetTargetCount()
  {
    return targets.Count;
  }
  void Update()
  {
    if (targets.Count > 0)
    {
      move = true;
      StepSoundEmmiting();

    }


    if (move)
      StartCoroutine(WalkThroughTargets());

    transform.position += dir.normalized * Time.deltaTime * speed;

  }

  float time = 1;
  void StepSoundEmmiting()
  {
    if (name != "Player")
      return;

    time += Time.deltaTime;
    if (time > 0.5f)
    {
      SoundGrid.CreateEmitter(transform.position);
      time = 0;
    }
  }
}
