using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

  bool InitializationError = false;
  // Use this for initialization
  void Start()
  {
    AIMovement mov = GetComponent<AIMovement>();
    if (mov == null)
    {
      Debugger.LogError("AIMovement not found");
      InitializationError = true;
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (InitializationError)
      return;

    AIMovement mov = GetComponent<AIMovement>();

    if (Input.GetMouseButtonDown(1))
    {
      var v3 = Input.mousePosition;
      v3.z = 0;
      v3 = Camera.main.ScreenToWorldPoint(v3);

      mov = GetComponent<AIMovement>();
      mov.targets.Clear();
      mov.targets.Add( v3 - new Vector3(0, 0, Camera.main.transform.position.z));
    }

  }


}
