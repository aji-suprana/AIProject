using System.Collections;
using System.Collections.Generic;
using UnityEngine;


static class DrawHelper
{
  static Shader boardShader = Shader.Find("Unlit/BoardShader");
  public static Material boardMat = new Material(boardShader);
  public static Material debugMoveMat = new Material(boardShader);

  public static void DrawQuad(Vector2 position, float size, Color color, Material lineMat)
  {

    if(Event.current.type != EventType.Repaint)
    return;
      Vector2 min = new Vector2(position.x - size / 2, position.y - size / 2);
    Vector2 max = new Vector2(position.x + size / 2, position.y + size / 2);

    lineMat.SetPass(0);
    GL.Begin(GL.QUADS);
    GL.Color(color);
    GL.Vertex3(min.x, min.y, 0);
    GL.Vertex3(min.x, max.y, 0);
    GL.Vertex3(max.x, max.y, 0);
    GL.Vertex3(max.x, min.y, 0);

    GL.End();
  }


  public static void DrawQuadOutLine(Vector2 position, float size, Color color, Material lineMat)
  {
    Vector2 min = new Vector2(position.y - size / 2, position.y - size / 2);
    Vector2 max = new Vector2(position.x + size / 2, position.y + size / 2);

    DrawLine(new Vector2(min.x, min.y), new Vector2(min.x, max.y), lineMat);
    DrawLine(new Vector2(min.x, max.y), new Vector2(max.x, max.y), lineMat);
    DrawLine(new Vector2(max.x, max.y), new Vector2(max.x, min.y), lineMat);
    DrawLine(new Vector2(max.x, min.y), new Vector2(min.x, min.y), lineMat);

  }


  public static void DrawLine(Vector2 begin, Vector2 end, Material lineMat)
  {
    GL.Begin(GL.LINES);
    debugMoveMat.SetPass(0);
    GL.Color(new Color(lineMat.color.r, lineMat.color.g, lineMat.color.b, lineMat.color.a));
    GL.Vertex3(begin.x, begin.y, 0);
    GL.Vertex3(end.x, end.y, 0);
    GL.End();
  }

}