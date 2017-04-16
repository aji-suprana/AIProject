using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Put this script on a Camera
public class TerrainBoard : MonoBehaviour
{
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  // Unity Message Handler
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  private void Start()
  {
    AllocateGrid();
    InitializeGrid();
    instance = this;
    //MapReader.ReadMap(this, "Map2_2LARGEROOMS"); // InitializeGrid is called after map is read


  }

  private void OnPostRender()
  {
    DrawGrid();
    //DrawHelper.DrawCircleFilled(new Vector2(0, 0), 10, Color.red);
    //DrawHelper.DrawQuad(new Vector2(0, 0), 50, Color.red);
  }
  private void OnGUI()
  {
    DrawGrid();
  }


  private void OnValidate()
  {
    //OnValidate run on editor mode only, Thus debugOn = false
    AllocateGrid(false);
    InitializeGrid(false);
    ResetColor();
  }
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  // Implementations
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  public static TerrainBoard instance;

  ////////////PUBLIC VAR
  [Range(4, 50)]
  public int boardWidth = 40;
  public int boardHeight = 40;
  public int test = 1;

  ////////////PRIVATE VAR
  private Grid[,] grids;

  ////////////PUBLIC FUNCTIONS
  /// Allocating gridboard's Grid in memory
  public void AllocateGrid(bool debugOn = true)
  {
    if (debugOn)
      Debugger.Log("Allocate: Gridboard");
    grids = new Grid[boardWidth, boardHeight];
  }

  ////////////PUBLIC FUNCTIONS
  /// Initalizing all grid on the board
  public void InitializeGrid(bool debugOn = true)
  {
    if(debugOn)
      Debugger.Log("Initialized: Gridboard Value");

    float startX = -boardWidth / 2 ;
    float startY = -boardHeight / 2;
    for (int y = 0; y < boardHeight; y++)
    {
      for (int x = 0; x < boardWidth; x++)
      {
        Vector2 realPosition = new Vector2( x, y) + new Vector2(startX, startY);
        grids[x, y] = new Grid(x, y, realPosition);
      }
    }
  }

  /// AGet a copy of a grid to get grid information ( constant renturned value )
  public Grid GetGrid(int x, int y)
  {
    if (x < 0 || y < 0 || x > boardWidth || y > boardHeight)
      return null;
    //Make a copy to prevent original grids value modifications
    Grid returnThis = new Grid(grids[x, y]);
    return returnThis;
  }

  /// AGet a copy of a grid to get grid information ( constant renturned value )
  public Grid GetGrid(Vector3 v)
  {
    float startX = -boardWidth / 2;
    float startY = -boardHeight / 2;
    int x = Mathf.RoundToInt(v.x - startX);
    int y = Mathf.RoundToInt(v.y - startY);

    if (x < 0 || y < 0 || x > boardWidth || y > boardHeight)
      return null;

    //Make a copy to prevent original grids value modifications
    Grid returnThis = new Grid(grids[x, y]);
    return returnThis;
  }

  public bool IsWall(int x, int y)
  {
    return grids[x, y].GetTerrainType() == TerrainType.WALL ;
  }

  /// Set Color of a grid
  public void SetColor(int x, int y, Color c) 
  {
    grids[x, y]._Color(c);
  }

  /// Set a grid to a wall
  public void SetWall(int x, int y)
  {
    grids[x, y].SetType(TerrainType.WALL);
  }

  // Reseting All Colors, used in function before changing map
  public void ResetColor()
  {
    for(int x = 0; x < boardWidth; x++)
      for (int y = 0; y < boardWidth; y++)
        grids[x, y]._Color(Color.white);
  }

  ////////////PRIVATES FUNCTIONS
  /// <summary>
  /// Drawing all grid that has been initialized
  /// </summary>
  private void DrawGrid()
  {
    for (int x = 0; x < boardWidth; x++)
      for (int y = 0; y < boardHeight; y++)
        grids[x, y].Draw();

  }

}