using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGrid : MonoBehaviour
{
  public Alien[] aliens;

  public int rows = 5;
  public int cols = 11;

  [SerializeField] public float xOffset = 1.0f;
  [SerializeField] public float yOffset = 1.0f;

  [SerializeField] public Vector3 direction = Vector3.right;
  [SerializeField] public float speed = 0.001f;

  private int counter = 0;
  private bool move = true;

  private void Awake()
  {
    float height = yOffset * (rows - 1);
    float width = xOffset * (cols - 1);

    for (int row = 0; row < this.rows; row++) {
      for (int col = 0; col < this.cols; col++) {
        Alien alien = Instantiate(this.aliens[row], this.transform);
        alien.hitEdge += HitEdge;
        // alien.hitEdge += HitBottom;

        Vector3 position = new Vector3(col * this.xOffset, row * this.yOffset, 0.0f);
        position -= new Vector3(width / 2, height / 2, 0);

        alien.transform.localPosition = position;
      }
    }
  }

  private void Update()
  {
    counter++;
    // make movement choppy ???
    if (move)
      this.transform.position += new Vector3(this.direction.x * this.speed, 0.0f, 0.0f); //this.direction * this.speed * Time.deltaTime;
  }

  private void HitEdge()
  {
    if(counter > 5) {
      counter = 0;
      this.direction.x *= -1.0f;
      this.transform.position -= new Vector3(0.0f, this.yOffset, 0.0f);
      Debug.Log(this.transform.position);
    }
  }

  private void HitBottom()
  {
    Debug.Log("Game Over!");
    move = false;
  }
}
