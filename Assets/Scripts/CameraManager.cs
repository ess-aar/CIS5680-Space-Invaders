using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
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
        Cam1.GetComponent<AudioListener>().enabled = true;

        Cam2.SetActive(false);
        Cam2.GetComponent<AudioListener>().enabled = false;

        flip = false;
    } else {
        Cam1.SetActive(false);
        Cam1.GetComponent<AudioListener>().enabled = false;

        Cam2.SetActive(true);
        Cam2.GetComponent<AudioListener>().enabled = true;

        flip = true;
      }
    }
  }

  public Camera getActiveCamera()
  {
    if(flip)
      return Cam2.GetComponent<Camera>();
    else
      return Cam1.GetComponent<Camera>();
  }
}
