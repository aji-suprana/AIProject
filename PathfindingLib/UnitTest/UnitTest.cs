using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pathfinding;

namespace UnitTest
{
  class UnitTest
  {
    string mapDirectory = "..\\..\\..\\..\\Assets\\StreamingAssets\\Maps\\";
    GridMap map;
    public void Initialize(string path)
    {
      map = new GridMap(mapDirectory + path + ".terrain");
      
    }
    public void TestNeighbor()
    {

    }

    public void TestPathFinding()
    {
      Initialize("Small");
      AStarSearch search = new AStarSearch(map, new Location(1, 1), new Location(1, 2));
      search.PrintResult ();
    }
  }
}
