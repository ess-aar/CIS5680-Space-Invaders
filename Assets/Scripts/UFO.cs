using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
  SoundManager sound;
  public System.Action<int> killed;
  public int pointValue;

  // [SerializeField] public Vector3 direction = Vector3.right;
  // [SerializeField] public float speed = 1.0f;
  // [SerializeField] public float speedIncrementFactor = 1.5f;
  // private int stepCounter = 0;

  private void Start()
  {
    GameObject obj = GameObject.Find("SoundManager");
    sound = obj.GetComponent<SoundManager>();
    // InvokeRepeating("Move",  1.0f, 1.0f);
  }

  // private void Update()
  // {
  //   Debug.Log("ufo update");
  // }

  // private void Move()
  // {
  //   if (stepCounter > 60) {
  //     stepCounter = 0;
  //     this.direction.x *= -1.0f;
  //     this.speed *= this.speedIncrementFactor;
  //   }
  //   this.transform.position += this.direction * this.speed * Time.deltaTime;
  //   stepCounter++;
  // }

  private void OnTriggerEnter(Collider collider)
  {
    if (collider.gameObject.layer == LayerMask.NameToLayer("Laser")) {
      this.killed.Invoke(pointValue);
      Destroy(this.gameObject);
      sound.AlienHit();
    }
  }
}
