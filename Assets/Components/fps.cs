using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class fps : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    GetComponent<Text>().text = (1 / Time.deltaTime).ToString();
    //if (1 / Time.deltaTime < 40)
    //  Debugger.Log("Performance Hit");

  }
}
