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

public static class DrawerStructure
{
  public static void Initialize()
  {
    line = new HashSet<LineSegment>();
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

  public static void RemoveLine(LineSegment l)
  {
    line.Remove(l);
  }

  public static HashSet<LineSegment> line;
}
