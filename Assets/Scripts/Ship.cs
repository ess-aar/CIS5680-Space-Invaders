using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Ammo laser;
    [SerializeField] public float speed = 5.0f;

    private bool canShoot = true;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
          if (this.canShoot)
            Shoot();
        }
    }

    private void Shoot()
    {
        Ammo laser = Instantiate(this.laser, this.transform.position, Quaternion.identity);
        laser.destroyed += LaserDestroyed;
        this.canShoot = false;
    }

    private void LaserDestroyed()
    {
      this.canShoot = true;
    }
}
