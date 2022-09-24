using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
  public IEnumerator Shake(float duration, float magnitude)
  {
    Vector3 originalPos = transform.localPosition;
    float elapsed = 0.0f;

    while(elapsed < duration)
    {
      float x = Random.Range(-1.0f, 1.0f) * magnitude;
      float y = Random.Range(-1.0f, 1.0f) * magnitude;
      // Debug.Log(x);

      transform.localPosition = new Vector3(x, y, originalPos.z);
      elapsed += Time.deltaTime;

      yield return null;
    }

    transform.localPosition = originalPos;
  }
}
