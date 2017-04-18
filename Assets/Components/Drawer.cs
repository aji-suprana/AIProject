using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
  List<LineCircle> toBeRemoved = new List<LineCircle> ();
  private void Awake()
  {
    DrawerStructure.Initialize();
  }
  private void OnPostRender()
  {
    foreach (LineSegment l in DrawerStructure.line)
    {
      DrawHelper.DrawLine(l.p[0], l.p[1],l.color);
    }

    foreach (LineCircle c in DrawerStructure.circle)
    {
      DrawHelper.DrawCircleOutLine(c.center, c.radius, c.color);
      c.timer -= Time.deltaTime;
      if (c.timer < 0) 
      toBeRemoved.Add(c);
    }

    foreach (LineCircle c in toBeRemoved)
    {
      DrawerStructure.circle.Remove(c);
    }
  }

}
