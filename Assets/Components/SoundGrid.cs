using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SoundEmitter
{
  public SoundEmitter(Location l)
  {
    location = l;
    radius = 0.3f;
  }
  public float radius;
  public float intensity;
  public readonly Location location;
  public void Update()
  {
    intensity = 10.0f / (2 * Mathf.PI * radius * radius);
    if (intensity < 0.01)
      intensity = 0;

    if (intensity > 1)
      intensity = 1;

    radius += Time.deltaTime * 10;
  }
}

public class SoundGrid : MonoBehaviour
{
  static Dictionary<Location, SoundEmitter> Emitters = new Dictionary<Location,SoundEmitter>();
  List<Location> toBeRemoved = new List<Location>();
  static float[,] IntensityGrid;
  static int width;
  static int height;
  int line;
  // Use this for initialization
  void Start()
  {
    Debugger.Log("SoundGrid");
    IntensityGrid = new float[TerrainBoard.map.width, TerrainBoard.map.height];
    width = TerrainBoard.map.width;
    height = TerrainBoard.map.height;
    //line = DrawerStructure.CreateLine(new Vector2(0, 0), new Vector2(10, 10));
  }

  public static void ChangeMap()
  {
    IntensityGrid = new float[TerrainBoard.map.width, TerrainBoard.map.height];
    width = TerrainBoard.map.width;
    height = TerrainBoard.map.height;
  }

  float Cross(Vector2 a, Vector2 b)
  {
    return a.x * b.y - a.y * b.x;
  }

  bool LineSegmentIntersect(Vector2 p, Vector2 p1, Vector2 q, Vector2 q1)
  {
    Vector2 r = p1 - p;
    Vector2 s = q1 - q;
    float rsCross = Cross(r, s);
    float srCross = Cross(s, r);
    Vector2 qp = q - p;

    float t = Cross(qp, s) / rsCross;
    float u = Cross(qp,r) / rsCross;
    //Colinear
    if (rsCross == 0 && Cross(qp,r) == 0)
    {
      return true;
    }
    ////Parrarel
    //if (Cross(r, s) == 0 && Cross(qp, r) != 0)
    //{
    //  return false;
    //}
    //
    if (Cross(r, s) != 0 && t <=1 && t >=0 && u <= 1 && u >= 0)
    {
      return true;
    }

    return false;
  }

  bool LineAABBIntersect(Vector2 aabbCenter, Vector2 rayStart, Vector2 rayEnd)
  {
    Vector2 tl = aabbCenter + new Vector2(-0.5f,0.5f);
    Vector2 tr = aabbCenter + new Vector2(0.5f, 0.5f);
    Vector2 bl = aabbCenter + new Vector2(-0.5f, -0.5f);
    Vector2 br = aabbCenter + new Vector2(0.5f, -0.5f);

    bool Left = LineSegmentIntersect(rayStart, rayEnd, tl, bl);
    bool Right = LineSegmentIntersect(rayStart, rayEnd, tr, br);
    bool Top = LineSegmentIntersect(rayStart, rayEnd, tl, tr);
    bool Bot = LineSegmentIntersect(rayStart, rayEnd, bl, br);

    return Left || Right || Top || Bot;
  }

  // Update is called once per frame
  void Update()
  {

    if (Input.GetMouseButtonDown(0))
    {
      Vector3 v3;
      v3 = Input.mousePosition;

      v3.z = 0;
      v3 = Camera.main.ScreenToWorldPoint(v3);
      CreateEmitter(v3);
    }

    foreach (Location l in toBeRemoved)
    {
      Emitters.Remove(l);
    }
    toBeRemoved.Clear();

    foreach (SoundEmitter e in Emitters.Values)
    {
      e.Update();
      if (e.intensity < 0.04f)
        toBeRemoved.Add(e.location);
      UpdateSoundParticle(e.location, e.radius, e.intensity);
    }


    //Sound fade out over time
    for (int x = 0; x < width; ++x)
    {
      for (int y = 0; y < height; ++y)
      {
        float currentIntensity = IntensityGrid[x, y];

        currentIntensity -= Time.deltaTime * 5 * currentIntensity;
        if (currentIntensity < 0)
          currentIntensity = 0;
        IntensityGrid[x, y] = currentIntensity;
      }
    }

    DrawSoundParticle();

    //DrawerStructure.line[0].p[0] = GameObject.Find("Player").transform.position;
    //DrawerStructure.line[0].p[1] = v3;
    //for (int x = 0; x < width; x++)
    //{
    //  for (int y = 0; y < height; y++)
    //  {
    //    Vector2 realPos = TerrainBoard.transformGridToPosition(x, y);
    //    if (LineAABBIntersect(realPos, GameObject.Find("Player").transform.position, v3))
    //    {
    //      TerrainBoard.SetColor(x, y, Color.blue);
    //    }
    //  }
    //}
  }

  public static void CreateEmitter(Vector3 pos)
  {
    Location l = TerrainBoard.transformPositionToGrid(pos);
    SoundEmitter e = new SoundEmitter(l);
    Emitters[l] = e;
  }

  private void UpdateSoundParticle(Location l, float radius, float intensity)
  {
    int Rangex = l.x + (int)radius;
    int Rangey = l.y + (int)radius;
    List<Location> walls = new List<Location>();

    //Find wall
    for (int x = l.x - (int)radius - 1; x < Rangex; x++)
    {

      for (int y = l.y - (int)radius; y < Rangey; y++)
      {
        if (TerrainBoard.map.IsWall(x, y))
        {
          walls.Add(new Location(x, y));
        }
      }

    }

    for (int x = 0; x < Rangex; x++)
    {
      if (x < 0 || x >= TerrainBoard.map.width) continue;
      for (int y = 0; y < Rangey; y++)
      {
        if (y < 0 || y >= TerrainBoard.map.height) continue;

        Vector2 dir = new Vector2(x - l.x, y - l.y);

        //Check if collided with wall
        bool intersected = false;
        foreach (Location i in walls)
        {
          Vector2 rayStart =new Vector2(x,y);
          Vector2 rayEnd = new Vector2(l.x, l.y);
          if (LineAABBIntersect(new Vector2(i.x, i.y), rayStart, rayEnd))
          {
            intersected = true;
          }
        }

        if (intersected)
          continue;

        if (dir.magnitude < radius)
        {
          float currentIntensity = IntensityGrid[x, y] + intensity;
          if (currentIntensity > 1)
            currentIntensity = 1;
          IntensityGrid[x, y] = currentIntensity;

        }
      }
    }
  }

  void DrawSoundParticle()
  {
    for (int x = 0; x < TerrainBoard.map.width; ++x)
      for (int y = 0; y < TerrainBoard.map.height; ++y)
      {
        TerrainBoard.SetColor(x, y, new Color(IntensityGrid[x,y], 0, 0) + new Color(1 - IntensityGrid[x, y], 1 - IntensityGrid[x, y], 1 - IntensityGrid[x, y]));
      }
  }
}
