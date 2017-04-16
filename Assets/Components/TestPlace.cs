using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlace : MonoBehaviour
{
  Vector3 playerPos;
  public Vector3 targetPos;
  List<Vector2> result = new List<Vector2>();
  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    TerrainBoard.instance.ResetColor();
    playerPos = GameObject.Find("Player").transform.position;
    result = AStar.Pathfind(TerrainBoard.instance, playerPos, targetPos);
    
  }

  private void OnPostRender()
  {

    if (result.Count > 0)

      TerrainBoard.instance.SetColor(playerPos, Color.grey);
    TerrainBoard.instance.SetColor(targetPos, Color.blue);

    for (int i = 1; i < result.Count; ++i)
    {
      Vector3 prevPoint = result[i - 1];
      Vector3 curPoint = result[i];
      prevPoint.z = 2;
      curPoint.z = 2;
      DrawHelper.DrawLine(prevPoint, curPoint, Color.red);
    }
  }
}
