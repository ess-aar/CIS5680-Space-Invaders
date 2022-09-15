using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
  public System.Action hitEdge;
  public System.Action hitBottom;

  private void OnTriggerEnter(Collider collider)
  {
    if (collider.gameObject.layer == LayerMask.NameToLayer("Laser")) {
      // this.gameObject.SetActive(false);
      Destroy(this.gameObject);
    }

    if (collider.gameObject.layer == LayerMask.NameToLayer("Collider")) {
      this.hitEdge.Invoke();
      Debug.Log("hit edge!");
    }

    if (collider.gameObject.layer == LayerMask.NameToLayer("BottomCollider")) {
      this.hitBottom.Invoke();
      Debug.Log("hit bottom!");
    }
  }
}
