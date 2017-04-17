using System.Collections;
using System.Collections.Generic;
using UnityEngine;


static class DrawHelper
{
  static Shader boardShader = Shader.Find("Unlit/BoardShader");
  public static Material lineMat = new Material(boardShader);


  public static void DrawCircleFilled(Vector2 center, float radius, Color c)
  {
    float division = 30;
    float angleStep = Mathf.PI * 2 / division;

    Vector3 prevPos;
    Vector3 curPos = new Vector3(center.x, center.y, 0) + (new Vector3(Mathf.Cos(0), Mathf.Sin(0), 0) * radius);
    for (int i = 1; i <= division; ++i)
    {
      prevPos = curPos;
      curPos = new Vector3(center.x, center.y, 0) + new Vector3(Mathf.Cos(angleStep * i), Mathf.Sin(angleStep * i), 0) * radius;
      lineMat.SetPass(0);
      GL.Begin(GL.TRIANGLES);
      GL.Color(Color.red);
      GL.Vertex3(center.x, center.y,0);
      GL.Vertex3(curPos.x, curPos.y, 0);
      GL.Vertex3(prevPos.x, prevPos.y, 0);
      GL.End();
    }

  }

  public static void DrawCircleOutLine(Vector2 center, float radius, Color c)
  {
    float division = 30;
    float angleStep = Mathf.PI * 2 / division;

    Vector2 prevPos;
    Vector2 curPos = center + (new Vector2(Mathf.Cos(0), Mathf.Sin(0)) * radius);
    for (int i = 1; i <= division; ++i)
    {
      prevPos = curPos;
      curPos = center + (new Vector2(Mathf.Cos(angleStep * i), Mathf.Sin(angleStep * i)) * radius);
      DrawLine(prevPos, curPos, c);
    }
  }
  
  public static void DrawQuad(Vector3 position, float size, Color color)
  {

    if (Event.current.type != EventType.Repaint)
      return;
    Vector2 min = new Vector2(position.x - size / 2, position.y - size / 2);
    Vector2 max = new Vector2(position.x + size / 2, position.y + size / 2);

    lineMat.SetPass(0);
    GL.Begin(GL.QUADS);
    GL.Color(color);
    GL.Vertex3(min.x, min.y, position.z);
    GL.Vertex3(min.x, max.y, position.z);
    GL.Vertex3(max.x, max.y, position.z);
    GL.Vertex3(max.x, min.y, position.z);

    GL.End();
  }

  public static void DrawQuadOutLine(Vector2 position, float size, Color c)
  {
    Vector2 min = new Vector2(position.y - size / 2, position.y - size / 2);
    Vector2 max = new Vector2(position.x + size / 2, position.y + size / 2);

    DrawLine(new Vector2(min.x, min.y), new Vector2(min.x, max.y), c);
    DrawLine(new Vector2(min.x, max.y), new Vector2(max.x, max.y), c);
    DrawLine(new Vector2(max.x, max.y), new Vector2(max.x, min.y), c);
    DrawLine(new Vector2(max.x, min.y), new Vector2(min.x, min.y), c);

  }


  public static void DrawLine(Vector2 begin, Vector2 end, Color c)
  {
    lineMat.SetPass(0);
    GL.Begin(GL.LINES);
    GL.Color(c);
    GL.Vertex3(begin.x, begin.y, 0);
    GL.Vertex3(end.x, end.y, 0);
    GL.End();
  }

}