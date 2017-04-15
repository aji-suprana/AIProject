using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class MapReader
{
  public static string parentPath = Application.dataPath;
  public static void ReadMap(TerrainBoard t, string fileName)
  {
    string prevLine = "";
    string line;
    int ySize = -1; // start on -1 for board size data on the first line
    int xSize = 0;

    Debugger.Log("Reading map");

    string path = parentPath+"/StreamingAssets/Maps/" + fileName + ".terrain";
    Debugger.Log("Testing map path: " + path);

    if (!File.Exists(path))
    {
      Debugger.LogError("Map file not found!");
      return;
    }

      System.IO.StreamReader file = new System.IO.StreamReader(path);


    while ((line = file.ReadLine()) != null)
    {
      //skip first + second line for row length checking (first line storing board size data)
      if (ySize > 0)
      {
        //Check if all the length are even, 
        if (prevLine.Length != line.Length)
        {
          Debugger.LogError("Error: Invalid map, uneven rowsize, row#: " + ySize);
          return;
        }
      }

      //Deserialize datas
      if (ySize == -1) // get board size data
      {
        string[] boardSize = line.Split(' ');

        t.boardWidth = Int32.Parse(boardSize[0]);
        t.boardHeight = Int32.Parse(boardSize[1]);
        t.AllocateGrid();
        t.InitializeGrid();
      }
      else
      {
        string[] terrainType = line.Split(' ');
        int x = 0;
        foreach (string i in terrainType)
        {
          if (Int32.Parse(i) == 1)
            t.SetWall(x, ySize);
          ++x;
        }
      }

      prevLine = line; // to check if the map is rectangular, prevline length != line length when board is not rectangle
      ySize++; // move to next line
      xSize = (line.Length + 1) / 2; //get the last line length as map width (+1 /2 to remove spaces counts)
    }

    file.Close();
    Debugger.Log("x size" + xSize);
    Debugger.Log("y count" + ySize);
    Debugger.Log("Map file read success");


  }
}
