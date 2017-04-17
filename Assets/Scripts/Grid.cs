using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum TerrainType
//{
//  EMPTY = 0,
//  WALL = 1
//}

public class Grid
{
  //Constructor
  public Grid()
  {
    x = 0;
    y = 0;
    realPosition = Vector2.zero;
  }
  public Grid(int _x, int _y, Vector2 _realPos)
  {
    x = _x;
    y = _y;
    realPosition = _realPos;
  }
  public Grid(int _x, int _y, Vector2 _realPos, Color _color)
  {
    x = _x;
    y = _y;
    realPosition = _realPos;
    color = _color;
  }
  public Grid(Grid g)
  {
    x = g.x;
    y = g.y;
    color = g.color;
    realPosition = g.realPosition;
  }


  //Get color
  public Color GetColor()
  {
    return color;
  }
  //Set color
  public void SetColor(Color c)
  {
    color = c;
  }

  //Public Var
  public Vector2 realPosition; // real gridPos in world coordinate

  //Private Var
  Material Mat;
  public int x = 0; //gridCoordinate
  public int y = 0; //gridCoordinate
  private Color color = new Color(0.7f,0.7f,0.7f); // drawing color on screen
  //private TerrainType terrainType;

  public void Draw()
  {
    Vector3 position = new Vector3(realPosition.x, realPosition.y, 3);
    DrawHelper.DrawQuad(realPosition, 0.9f, color);
  }
}
