using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pathfinding;
using System.IO;
namespace UnitTest
{
  class Program
  {
    static void Main(string[] args)
    {
      UnitTest test = new UnitTest();
      test.TestPathFinding();

      HashSet<int> hashTest = new HashSet<int>();
      hashTest.Add(0);
      hashTest.Add(0);
      hashTest.Add(0);

      Console.WriteLine(hashTest.Count);
      hashTest.Remove(0);

      Console.WriteLine(hashTest.Count);
      while (true)
      {

      }
    }
  }
}
