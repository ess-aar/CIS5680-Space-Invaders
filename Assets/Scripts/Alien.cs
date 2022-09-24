using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
  public int pointValue;
  public System.Action<int> killed;
  public System.Action hitEdge;
  public System.Action hitBottom;
  SoundManager sound;
  public GameObject explosion;

  public bool canKill = true;

  void Start()
  {
    GameObject obj = GameObject.Find("SoundManager");
    sound = obj.GetComponent<SoundManager>();
  }
  
  private void OnTriggerEnter(Collider collider)
  {
    if (collider.gameObject.layer == LayerMask.NameToLayer("Collider")) {
      if (this.hitEdge != null)
       this.hitEdge.Invoke();
    }

    if (collider.gameObject.layer == LayerMask.NameToLayer("PlayerZone") && this.transform.parent != null) {
      if (this.hitBottom != null)
       this.hitBottom.Invoke();
    }
  }

  private void OnCollisionEnter(Collision collision)
  {
    Collider collider = collision.collider;
    if (collider.gameObject.layer == LayerMask.NameToLayer("Laser") && collider.gameObject.GetComponent<Ammo>().canKill) {
      this.killed.Invoke(pointValue);
      var exp = Instantiate(explosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));
      
      // Destroy(this.gameObject);
      Rigidbody rb = GetComponent<Rigidbody>();
      rb.isKinematic = false;
      
      this.transform.parent = null;
      sound.AlienHit();
      this.canKill = false;
    }
  }
}
