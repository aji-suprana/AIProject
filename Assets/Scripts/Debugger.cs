using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public static class Debugger {

  //Public properties ( hide in Unity inspector
  public static Text DebugLog { get; set; }
  public static Text DebugError { get; set; }

  public static void Initialize()
  {
    //Log("Initializing: Debugger");
    //DebugLog = GameObject.FindGameObjectWithTag("DebugPanelText").GetComponent<Text>();
    //DebugError = GameObject.FindGameObjectWithTag("DebugErrorPanelText").GetComponent<Text>();
    DebugLog = GameObject.Find("DebugLog").GetComponent<Text>();
    DebugError = GameObject.Find("DebugError").GetComponent<Text>();
    Log("Initialized: Debugger");
  }

  public static void Log(object s)
  {
    Debug.Log(s);
    if (DebugLog == null)
      return;
    DebugLog.text += s.ToString() + "\n";
  }

  public static void LogError(object s)
  {
    Debug.LogError(s);
    if (DebugError == null)
      return;
    DebugError.text += s.ToString() + "\n";
  }
}
