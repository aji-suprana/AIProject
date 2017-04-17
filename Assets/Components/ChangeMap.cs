using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ChangeMap : MonoBehaviour {

  public void Change()
  {
    //MapReader.ReadMap(TerrainBoard.instance,GetComponent<Text>().text);
    TerrainBoard.instance.ChangeMap(GetComponent<Text>().text);
  }
}
