using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  static AudioSource audio;
  public AudioClip shipFired;
  public AudioClip alienFired;
  public AudioClip shipHit;
  public AudioClip alienHit;
  public AudioClip shieldHit;
  
  void Start ()
  {
    audio = GetComponent<AudioSource>();
  }
  public void ShipFired()
  {
    audio.PlayOneShot(shipFired);
  }
  public void AlienFired()
  {
    audio.PlayOneShot(alienFired);
  }
  public void ShipHit()
  {
    audio.PlayOneShot(shipHit);
  }
  public void AlienHit()
  {
    audio.PlayOneShot(alienHit);
  }
  public void ShieldHit()
  {
    audio.PlayOneShot(shieldHit);
  }
}
