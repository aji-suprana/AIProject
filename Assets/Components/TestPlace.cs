﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlace : MonoBehaviour
{

  // Use this for initialization
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {

    List<Vector3> result = AStar.Pathfind(TerrainBoard.instance, new Vector3(0, 0, 0), new Vector3(3, 3, 0));
    //Debug.Log(result);
  }
}