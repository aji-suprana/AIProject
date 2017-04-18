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
      Initialize("5x5");
      AStarSearch search = new AStarSearch(map);
      search.Search(new Location(0,0), new Location (2,2));
      search.PrintResult ();
    }
  }
}
