using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Pathfinding;

// Put this script on a Camera
public class TerrainBoard : MonoBehaviour
{
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  // Unity Message Handler
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  private void Start()
  {
    Debugger.Log("TerrainBoard Initialized");

    Pathfinding.Debug.Log = Debugger.Log;
    Pathfinding.Debug.LogError = Debugger.LogError;

    ChangeMap("4");

    instance = this;
    //MapReader.ReadMap(this, "Test"); // InitializeGrid is called after map is read

  }

  private void OnPostRender()
  {
    DrawGrid();
    //DrawHelper.DrawCircleFilled(new Vector2(0, 0), 10, Color.red);
    //DrawHelper.DrawQuad(new Vector2(0, 0), 50, Color.red);
  }

  //private void OnValidate()
  //{
  //  //OnValidate run on editor mode only, Thus debugOn = false
  //  AllocateGrid(false);
  //  InitializeGrid(false);
  //  ResetColor();
  //}

  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  // Implementations
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  public static TerrainBoard instance;
  public static GridMap map;
  private static Grid[,] grids;

  ////////////PUBLIC VAR


  ////////////PRIVATE VAR
  private static float startX;
  private static float startY;

  ////////////PUBLIC FUNCTIONS
  public void ChangeMap(string path)
  {
    string realpath = Application.dataPath + "/StreamingAssets/Maps/" + path + ".terrain";
    map = new GridMap(realpath);
    if (map.width == -1)
      return;

    foreach (AIBrain brain in GameObject.FindObjectsOfType<AIBrain>())
    {
      brain.SearchAlgorithm = new AStarSearch(map);
    }

    InitializeGridFeedback();
  }

  /// Initalizing all grid on the board
  public void InitializeGridFeedback()
  {
    grids = new Grid[map.width, map.height];

    Debugger.Log("Initialized: GridboardFeedback Value");

    startX = -map.width / 2;
    startY = -map.height / 2;

    for (int y = 0; y < map.height; y++)
    {
      for (int x = 0; x < map.width; x++)
      {
        Vector2 realPosition = new Vector2( x, y) + new Vector2(startX, startY);
        grids[x, y] = new Grid(x, y, realPosition);
        if (map.IsWall(x, y))
        {
          grids[x, y].SetColor(Color.black);
        }
      }
    }
  }

  /// AGet a copy of a grid to get grid information ( constant renturned value )
  public Grid GetGrid(int x, int y)
  {
    if (x < 0 || y < 0 || x > map.width-1 || y > map.height-1)
      return null;
    //Make a copy to prevent original grids value modifications
    Grid returnThis = new Grid(grids[x, y]);
    return returnThis;
  }

  /// AGet a copy of a grid to get grid information ( constant renturned value )
  public Grid GetGrid(Vector3 v)
  {
    int x = 0;
    int y = 0;
    transformPositionToGrid(v, ref x, ref y);
    if (x < 0 || y < 0 || x > map.width || y > map.height)
      return null;

    //Make a copy to prevent original grids value modifications
    Grid returnThis = new Grid(grids[x, y]);
    return returnThis;
  }

  ///Transforming Vector3 position into grid coordinate by passing coordinate x y as reference
  public static void transformPositionToGrid(Vector3 position, ref int x, ref int y)
  {
    startX = -map.width / 2;
    startY = -map.height / 2;
    x = Mathf.RoundToInt(position.x - startX);
    y = Mathf.RoundToInt(position.y - startY);

  }

  ///Transforming Vector3 position into grid coordinate by returning Location type
  public static Location transformPositionToGrid(Vector3 position)
  {
    startX = -map.width / 2;
    startY = -map.height / 2;
    int x = Mathf.RoundToInt(position.x - startX);
    int y = Mathf.RoundToInt(position.y - startY);
    return new Location(x, y);
  }

  ///Transforming Grid coordinate into real world position in Vector2
  public static Vector2 transformGridToPosition(int x, int y)
  {
    return grids[x, y].realPosition;
  }

  /// Set Color of a grid
  public static void SetColor(Vector3 v,Color c)
  {
    int x = 0;
    int y = 0;
    transformPositionToGrid(v, ref x, ref y);
    if (map.IsWall(x, y))
      return;

    grids[x, y].SetColor(c);
  }

  /// Set Color of a grid
  public static void SetColor(int x, int y, Color c) 
  {
    if (map.IsWall(x, y))
      return;
    grids[x, y].SetColor(c);
  }

  /// Get Color of a grid
  public static Color GetColor(int x, int y)
  {
    return grids[x, y].GetColor();
  }


  public static void ResetColor()
  {
    for (int x = 0; x < map.width; x++)
      for (int y = 0; y < map.width; y++)
      {
        if (map.IsWall(x,y))
          continue;
        grids[x, y].SetColor(Color.white);

      }
  }

  ////////////PRIVATES FUNCTIONS
  /// <summary>
  /// Drawing all grid that has been initialized
  /// </summary>
  private void DrawGrid()
  {
    for (int x = 0; x < map.width; x++)
      for (int y = 0; y < map.height; y++)
        grids[x, y].Draw();

  }

}