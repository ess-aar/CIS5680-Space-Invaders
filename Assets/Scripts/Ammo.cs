using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public Vector3 direction;
    [SerializeField] public float speed;
    [SerializeField] public float speedIncrementFactor = 1.1f;

    public int hits = 3;
    public bool canKill = true;
    public System.Action destroyed;

    private void FixedUpdate()
    {
      this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collider)
    {
      if (collider.gameObject.layer == LayerMask.NameToLayer("Ship")) {
        return;
      }
      this.speed = 50.0f;

      if (this.destroyed != null)
        this.destroyed.Invoke();
      
      Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
      Collider collider = collision.collider;
      if (collider.gameObject.layer == LayerMask.NameToLayer("Alien")) {
        this.hits--;
      }

      if (this.gameObject.layer == LayerMask.NameToLayer("Laser")) {
        this.speed = 50.0f;

        if (hits == 0)
          this.canKill = false;
      }

      if (this.destroyed != null)
        this.destroyed.Invoke();
    }
}
