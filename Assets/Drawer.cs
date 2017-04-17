using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
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
  }

}
