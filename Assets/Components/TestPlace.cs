using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TestPlace : MonoBehaviour
{
  Vector3 playerPos;
  public Vector3 targetPos;
  Stack<Vector2> result = new Stack<Vector2>();
  // Use this for initialization
  void Start()
  {
    
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnPostRender()
  {
    TerrainBoard.instance.SetColor(playerPos, Color.grey);
    TerrainBoard.instance.SetColor(targetPos, Color.blue);

    Vector3 prevPoint = result.Peek();
    Vector3 curPoint = result.Pop();

    foreach(Vector3 t in result)
    {
      prevPoint = curPoint;
      curPoint = t;
      DrawHelper.DrawLine(prevPoint, curPoint, Color.red);
    }
  }
}
