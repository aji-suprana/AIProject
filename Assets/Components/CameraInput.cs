using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInput : MonoBehaviour
{
  public float speed = 45;
  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKey(KeyCode.A))
    {
      transform.position -= Vector3.right * Time.deltaTime * speed;
    }

    if (Input.GetKey(KeyCode.D))
    {
      transform.position += Vector3.right * Time.deltaTime * speed;
    }

    if (Input.GetKey(KeyCode.W))
    {
      transform.position += Vector3.up * Time.deltaTime * speed;
    }

    if (Input.GetKey(KeyCode.S))
    {
      transform.position -= Vector3.up * Time.deltaTime * speed;
    }

    if (Input.GetKey(KeyCode.X))
    {
      GetComponent<Camera>().orthographicSize += Time.deltaTime * speed;
    }

    if (Input.GetKey(KeyCode.Z))
    {
      GetComponent<Camera>().orthographicSize -= Time.deltaTime * speed;
    }

    var d = Input.GetAxis("Mouse ScrollWheel");
    if (d > 0f)
    {
      GetComponent<Camera>().orthographicSize -= Time.deltaTime * speed;
    }
    else if (d < 0f)
    {
      GetComponent<Camera>().orthographicSize += Time.deltaTime * speed;
    }

  }
}
