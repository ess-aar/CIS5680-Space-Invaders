using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
  public int pointValue;
  public System.Action<int> killed;
  public System.Action hitEdge;
  public System.Action hitBottom;
  
  private void OnTriggerEnter(Collider collider)
  {
    if (collider.gameObject.layer == LayerMask.NameToLayer("Laser")) {
      this.killed.Invoke(pointValue);
      Destroy(this.gameObject);
    }

    if (collider.gameObject.layer == LayerMask.NameToLayer("Collider")) {
      if (this.hitEdge != null)
       this.hitEdge.Invoke();
    }

    if (collider.gameObject.layer == LayerMask.NameToLayer("PlayerZone")) {
      if (this.hitBottom != null)
       this.hitBottom.Invoke();
    }
  }
}
