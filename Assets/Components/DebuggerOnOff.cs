using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggerOnOff : MonoBehaviour {

  //// Use this for initialization
  void Start () {
    Toggle();
  }

  //// Update is called once per frame
  //void Update () {

  //}
  private bool DebugPanelOn = true;
  public void Toggle()
  {
    //toggle on to off, or off to on
    DebugPanelOn = !DebugPanelOn;

    if (DebugPanelOn)
    {
      GetComponent<RectTransform>().anchoredPosition = new Vector2(-80 , 15);
    }
    else
    {
      //Hide panel out of the screen
      GetComponent<RectTransform>().anchoredPosition = new Vector2(-80, 260);

    }
  }
}
