using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeMap : MonoBehaviour {

  public void Change()
  {
    MapReader.ReadMap(TerrainBoard.instance,GetComponent<Text>().text);
  }
}
