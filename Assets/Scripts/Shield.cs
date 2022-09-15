using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
  public int hits = 0;
  public int maxHits = 10;

  private void OnTriggerEnter(Collider collider)
  {
    if (collider.gameObject.layer == LayerMask.NameToLayer("Missile")) {
      hits++;

      Color currColor = this.GetComponent<Renderer>().material.color;
      currColor.a = (float)(maxHits - hits) / (float)maxHits;
      this.GetComponent<Renderer>().material.SetColor("_Color", currColor);

      if(hits >= maxHits) {
        Debug.Log("Shield destroyed!");
        Destroy(this.gameObject);
      }
    }
  }
}
