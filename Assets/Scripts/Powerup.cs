using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    Vector3 direction = Vector3.up;
    public System.Action collected;

    private void FixedUpdate()
    {
      this.transform.position += direction * 10.0f * Time.deltaTime;
      direction *= -1.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
      Collider collider = collision.collider;
      if (collider.gameObject.CompareTag("Ship")) {
        Debug.Log("Collided with ship");
        if (this.collected != null)
          this.collected.Invoke();
        
        Destroy(this.gameObject);
      }
    }
}
