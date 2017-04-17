using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class PlayerInput : MonoBehaviour
{

  bool InitializationError = false;
  // Use this for initialization
  void Start()
  {
    AIMovement mov = GetComponent<AIMovement>();
    if (mov == null)
    {
      Debugger.LogError("AIMovement not found");
      InitializationError = true;
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (InitializationError)
      return;

    AIMovement mov = GetComponent<AIMovement>();

    if (Input.GetMouseButtonDown(1))
    {
      var v3 = Input.mousePosition;
      v3.z = 0;
      v3 = Camera.main.ScreenToWorldPoint(v3);

      TerrainBoard t = TerrainBoard.instance;

      Location start = t.transformPositionToGrid(transform.position);

      Location goal = t.transformPositionToGrid(v3);



      AStarSearch search = new AStarSearch(TerrainBoard.map, start, goal);
      search.PrintResult();

      Stack<Location> test = search.GetResult() ;
      Location testl = test.Pop();
      testl.Print();
      //Stack<Vector2> result = AStar.Pathfind(TerrainBoard.instance, transform.position, v3);

      Queue<Vector2> targets = new Queue<Vector2>();
      foreach (Location l in search.GetResult())
      {
        targets.Enqueue(t.transformGridToPosition(l.x, l.y));
      }

      mov.ResetTarget(targets);
    }

  }


}
