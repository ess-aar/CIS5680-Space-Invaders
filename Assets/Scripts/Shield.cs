using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
  public int hits = 0;
  public int maxHits = 10;

  SoundManager sound;

  void Start()
  {
    GameObject obj = GameObject.Find("SoundManager");
    sound = obj.GetComponent<SoundManager>();
  }

  private void OnTriggerEnter(Collider collider)
  {
    if (collider.gameObject.layer == LayerMask.NameToLayer("Missile") ||
        collider.gameObject.layer == LayerMask.NameToLayer("Laser") ||
        collider.gameObject.layer == LayerMask.NameToLayer("Alien")) {
      hits++;
      sound.ShieldHit();

      Color currColor = this.GetComponent<Renderer>().material.color;
      currColor.a = (float)(maxHits - hits) / (float)maxHits;
      this.GetComponent<Renderer>().material.SetColor("_Color", currColor);

      Vector3 scale = this.transform.localScale;
      scale.x *= (float)(maxHits - hits) / (float)maxHits;
      this.transform.localScale = scale;

      if(hits >= maxHits) {
        Debug.Log("Shield destroyed!");
        Destroy(this.gameObject);
      }
    }
  }
}
