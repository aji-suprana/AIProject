using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIBrain : MonoBehaviour
{
  public AStarSearch SearchAlgorithm;

  public Queue<Vector2> Search(Vector2 a, Vector2 b)
  {
    Location start = TerrainBoard.transformPositionToGrid(a);

    Location goal = TerrainBoard.transformPositionToGrid(b);

    SearchAlgorithm.Search(start, goal);

    Queue<Vector2> targets = new Queue<Vector2>();
    foreach (Location l in SearchAlgorithm.GetResult())
    {
      targets.Enqueue(TerrainBoard.transformGridToPosition(l.x, l.y));
    }
    return targets;

    //TerrainBoard.ResetColor();
    //foreach (Location i in TerrainBoard.AStar.GetExploredList())
    //{
    //  TerrainBoard.SetColor(i.x,i.y,Color.red);
    //}
  }
}

public class AIInput : AIBrain
{
  enum AIState
  {
    Idle,
    Wander,
    Chase
  }
  public delegate void fn();

  float maxAngle = Mathf.PI;
  float maxDist = 10;



  AIMovement mov;
  AIState state = AIState.Wander;
  fn SelectTarget = null;
  fn Behavior = null;

  public bool targetFound = false;
  public bool chasing = false;
  Vector2 target;

  void Start()
  {
    mov = GetComponent<AIMovement>();
    SelectTarget = WanderSelect;
    Behavior = Wander;
  }

  // Update is called once per frame
  void Update()
  {
    switch (state)
    {
      case AIState.Idle:
        chasing = false;

        break;
      case AIState.Wander:
        Behavior = Wander;
        SelectTarget = WanderSelect;
        break;
      case AIState.Chase:
        chasing = true;

        break;
    }

    Behavior();
  }

  float IdleTimer = 0;
  void Idle()
  {
    IdleTimer += Time.deltaTime;
    if (IdleTimer > 1)
      state = AIState.Wander;
  }

  float curAngle = 0;
  Vector2 pos;
  Vector2 dir;
  Vector2 goal;
  Location goalGrid;


  void WanderSelect()
  {
    System.Random random = new System.Random();
    float angle = (random.Next(-50, 50) / 100.0f) * maxAngle;
    curAngle += angle;
    pos = transform.position;
    dir = new Vector2(Mathf.Cos(curAngle), Mathf.Sin(curAngle)) * maxDist;

    goal = pos + dir;
  }

  void Wander()
  {
    if (mov.GetTargetCount() == 0)
    {
      TerrainBoard.SetColor(goalGrid.x, goalGrid.y, Color.white);
      SelectTarget();
      goalGrid = TerrainBoard.transformPositionToGrid(pos + dir);
      if (!TerrainBoard.map.InBound(goalGrid))
        return;

      TerrainBoard.SetColor(goalGrid.x, goalGrid.y, Color.red);
      mov.ResetTarget(Search(pos, pos + dir));
    }


  }


}
