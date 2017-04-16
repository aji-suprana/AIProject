using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class AStar
{

  public class Node
  {
    public Node() { }
    public Node(Node _parent, Grid g) { parent = _parent; grid = g; }
    public bool Equals(Node n)
    {
      bool isEqual = grid.x == n.grid.x && grid.y == n.grid.y;
      return isEqual;
    }
    public Node parent;
    public Grid grid;
    public float g = 0;
    public float h = 0;
  }

  private static List<Node> OpenList;
  private static List<Node> CloseList;
  private static float searchTime = 0;
  public static List<Vector2> Pathfind(TerrainBoard t, Vector2 g_start, Vector2 g_end)
  {
    searchTime = 0;
    List<Vector2> result = new List<Vector2>();
    OpenList = new List<Node>();
    CloseList = new List<Node>();
    Node node_start = new Node(null, t.GetGrid(g_start));
    Node node_end = new Node(null, t.GetGrid(g_end));
    if (node_start == null || node_end == null)
      return null;
    //put node_start in open list

    //while open list is not empty
    ////Take node_current as a ndoe with the lowest f ( f = g + h)
    ////if node_current == goalnode, solution found, return
    ////generate neighbouring child of node_current as node_neighbours
    ////for each child in node_neighbour
    //////compute cost f = g + h
    //////if child isn't on open/close list, put it on openlist
    //////if child nodeis on open/closelist and child is cheaper, then take the old expensive one off both list and put this new cheaper one on the open list
    //place node_current to the close list
    //if open list empty return no path

    //put node_start in open list
    OpenList.Add(node_start);
    //while open list is not empty
    while (OpenList.Count != 0 && searchTime < 8000)
    {
      searchTime += 1;
      ////Take node_current as a node with the lowest f ( f = g + h) in open list
      Node node_current = FindCheapest(OpenList);

      ////if node_current == node_end, solution found, return
      bool equals = node_current.Equals(node_end);
      if (equals)
      {
        Node n = node_current;
        while (n != null)
        {
          result.Add(n.grid.realPosition);
          n = n.parent;
        }
        break;
      }

      //////generate neighbouring child of node_current as node_neighbours
      List<Node> node_neighbours = new List<Node>();
      for (int x = -1; x <= 1; x++)
        for (int y = -1; y <= 1; y++)
        {
          if (x == 0 && y == 0)
            continue;
          int xId = node_current.grid.x + x;
          int yId = node_current.grid.y + y;
          Grid curNeighbour = t.GetGrid(xId, yId);
          if (curNeighbour == null)
            continue;

          if (t.IsWall(curNeighbour.x, curNeighbour.y))
            continue;

          Node currentNeighbour = new Node(node_current, curNeighbour);
          //on diagonal Calculate g
          if (Mathf.Abs(x) == Mathf.Abs(y))
          {
            currentNeighbour.g = node_current.g + 1.4142135f;
          }
          else
          {
            currentNeighbour.g = node_current.g + 1;
          }

          //Calculate h
          currentNeighbour.h = (g_end - currentNeighbour.grid.realPosition).magnitude;
          node_neighbours.Add(currentNeighbour);
        }

      ////for each child in node_neighbour
      foreach (Node child in node_neighbours)
      {
        //////compute cost f = g + h
        float cost = child.g + child.h;
        Node existed = NodeExisted(OpenList, child);
        if (existed == null)
          existed = NodeExisted(CloseList, child);

        //////if child isn't on open/close list, put it on openlist
        if (existed == null)
        {
          OpenList.Add(child);
        }
        //////if child node is on open/closelist and child is cheaper, then take the old expensive one off both list and put this new cheaper one on the open list
        else
        {
          if (cost < existed.h + existed.g)
          {
            OpenList.Remove(existed);
            CloseList.Remove(existed);
            OpenList.Add(child);
          }
        }
      }

      ////place node_current to the close list
      //CloseList.Add(node_current);

    }


    return result;

  }

  private static Node FindCheapest(List<Node> l)
  {
    Node returnThis = null;
    float cheapest = Mathf.Infinity;

    foreach (Node n in l)
    {
      float current_cost = n.g + n.h;
      if (cheapest > current_cost)
      {
        cheapest = current_cost;
        returnThis = n;
      }
    }

    if (returnThis != null)
      l.Remove(returnThis);

    return returnThis;
  }

  private static Node NodeExisted(List<Node> l, Node check)
  {
    foreach (Node n in l)
    {
      if (n.grid.x == check.grid.x && n.grid.y == check.grid.y)
        return n;
    }
    return null;
  }
}
