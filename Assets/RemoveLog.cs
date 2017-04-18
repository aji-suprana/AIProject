using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RemoveLog : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    if (GetComponent<Text>().text.Length > 2000)
    {
      GetComponent<Text>().text = "";
    }
	}
}
