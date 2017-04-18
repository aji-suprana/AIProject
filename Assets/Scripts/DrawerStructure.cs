using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSegment
{
  public LineSegment()
  {
    p =new Vector2[2] { Vector2.zero, Vector2.zero };
  }

  public LineSegment(Vector2 p0, Vector2 p1)
  {
    p = new Vector2[2] { p1, p0};
  }

  public Vector2[] p;
  public Color color;
}

public class LineCircle
{
  public LineCircle()
  {
    center = Vector2.zero;
    radius = 0.1f;
  }

  public LineCircle(Vector2 center, float radius)
  {
    this.center = center;
    this.radius = radius;
    this.color = Color.black;
  }

  public LineCircle(Vector2 center, float radius, Color c)
  {
    this.center = center;
    this.radius = radius;
    this.color = c;
  }

  public Vector2 center;
  public float radius;
  public Color color;
  public float timer = 1;
}
public static class DrawerStructure
{
  public static void Initialize()
  {
    line = new List<LineSegment>();
    circle = new List<LineCircle>();
  }

  public static int CreateLine(Vector2 p0, Vector2 p1)
  {
    int currentID = line.Count;
    LineSegment l = new LineSegment(p0, p1);
    if (line.Contains(l))
    {
      Debugger.LogError("Line existed: ");
      return -1;
    }
    line.Add(l);
    return currentID;
  }

  public static void RemoveLine(int l)
  {
    line.RemoveAt(l);
  }

  public static int CreateCircle(Vector2 center, float radius)
  {
    int currentID = circle.Count;
    LineCircle l = new LineCircle(center, radius);
    if (circle.Contains(l))
    {
      Debugger.LogError("Line existed: ");
      return -1;
    }
    circle.Add(l);
    return currentID;
  }

  public static int CreateCircle(Vector2 center, float radius,Color c)
  {
    int currentID = circle.Count;
    LineCircle l = new LineCircle(center, radius,c);
    if (circle.Contains(l))
    {
      Debugger.LogError("Line existed: ");
      return -1;
    }
    circle.Add(l);
    return currentID;
  }

  public static void RemoveCircle(int l)
  {
    if(l < circle.Count)
    circle.RemoveAt(l);
  }

  public static List<LineSegment> line;
  public static List<LineCircle> circle;
}
