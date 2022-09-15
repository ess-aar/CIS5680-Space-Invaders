using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
  public GameObject Cam1;
  public GameObject Cam2;
  private bool flip;

  void Awake()
  {
    Cam1.SetActive(true);
    Cam2.SetActive(false);
  }
  void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
          if (flip) {
            Cam1.SetActive(true);
            Cam2.SetActive(false);
            flip = false;
          } else {
            Cam1.SetActive(false);
            Cam2.SetActive(true);
            flip = true;
          }
        }
    }
}
