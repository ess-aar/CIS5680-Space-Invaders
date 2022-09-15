using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public Vector3 direction;
    [SerializeField] public float speed;
    public System.Action destroyed;
    private void FixedUpdate()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collider)
    {
      if (this.destroyed != null)
        this.destroyed.Invoke();
      
      Destroy(this.gameObject);
    }
}
