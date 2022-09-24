using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Ammo laser;
    [SerializeField] public float speed = 5.0f;
    [SerializeField] public float deceleration = 4.0f;
    private bool canShoot = true;
    public bool canJump = false;

    SoundManager sound;
    public CameraEffect camera;

    void Start()
    {
      GameObject obj = GameObject.Find("SoundManager");
      sound = obj.GetComponent<SoundManager>();
    }

    private void FixedUpdate()
    {
      if (this.transform.position.y > 20.0)
        this.transform.position -= Vector3.up * Time.deltaTime * deceleration;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        } else if (canJump && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))) {
            this.transform.position += Vector3.up * this.speed * Time.deltaTime * 1.0f;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
          if (this.canShoot)
            Shoot();
        }

        if (this.transform.position.y > 520.0f)
        {
          GameObject.Find("AlienGrid").GetComponent<AlienGrid>().ShipEscaped();
          GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
      Collider collider = collision.collider;
      if ((collider.gameObject.layer == LayerMask.NameToLayer("Alien") && collider.gameObject.GetComponent<Alien>().canKill) ||
          collider.gameObject.layer == LayerMask.NameToLayer("Missile")) {
        GameObject obj = GameObject.Find("GlobalUIObject");
        UI g = obj.GetComponent<UI>();
        if (g.lives > 0)
          g.lives--;
        sound.ShipHit();
        StartCoroutine(camera.Shake(0.5f, 3.0f));
      }
    }

    private void Shoot()
    {
        Ammo laser = Instantiate(this.laser, this.transform.position + new Vector3(0.0f, 25.0f, 0.0f), Quaternion.identity);
        laser.destroyed += LaserDestroyed;
        this.canShoot = false;
        sound.ShipFired();
    }

    private void LaserDestroyed()
    {
      this.canShoot = true;
    }

    public void PowerupsCollected()
    {
      canJump = true;
    }
}
