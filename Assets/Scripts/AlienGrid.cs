using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGrid : MonoBehaviour
{
  public int rows = 5;
  public int cols = 11;

  public Alien[] aliens;
  private int alienCount;

  [SerializeField] public float xOffset = 1.0f;
  [SerializeField] public float yOffset = 1.0f;

  [SerializeField] public Vector3 direction = Vector3.right;
  [SerializeField] public float speed = 1.0f;
  [SerializeField] public float speedIncrementFactor = 1.5f;

  [SerializeField] public float missileRate = 2.0f;
  public Ammo missile;

  private int stepCounter = 0;
  public bool move = true;
  private UI ui;

  private void Awake()
  {
    float height = this.yOffset * (this.rows - 1);
    float width = this.xOffset * (this.cols - 1);
    alienCount = this.rows * this.cols;

    for (int row = 0; row < this.rows; row++) {
      for (int col = 0; col < this.cols; col++) {
        Alien alien = Instantiate(this.aliens[row], this.transform);
        alien.killed += AlienKilled;
        alien.hitEdge += MoveVertically;
        alien.hitBottom += ReachedBottom;

        Vector3 position = new Vector3(col * this.xOffset, row * this.yOffset, 0.0f);
        position -= new Vector3(width / 2, height / 2, 0);

        alien.transform.localPosition = position;
      }
    }

    GameObject obj = GameObject.Find("GlobalObject");
    ui = obj.GetComponent<UI>();
    ui.gameover += StopMoving;
  }
  
  private void Start()
  {
    InvokeRepeating("ShootMissile",  1.0f, 1.0f / missileRate);
  }

  private void Update()
  {
    if (move && stepCounter > 60) {
      // this.transform.position += this.direction * this.speed;
      this.transform.position += this.direction * this.speed * Time.deltaTime;
    }
    stepCounter++;
  }

  private void MoveVertically()
  {
    if(stepCounter > 5) {
      stepCounter = 0;
      this.direction.x *= -1.0f;
      this.speed *= this.speedIncrementFactor;
      this.transform.position -= new Vector3(0.0f, this.yOffset / 2.0f, 0.0f);
    }
  }
  private void ShootMissile()
  {
    foreach (Transform alien in this.transform) {
      if (Random.value < (1.0f / (float)this.alienCount)) {
        Instantiate(this.missile, alien.position, Quaternion.identity);
        break;
      }
    }
  }

  private void StopMoving()
  {
    if (move) {
      move = false;
      CancelInvoke();
    }
  }

  private void ReachedBottom()
  {
    Debug.Log("Game Over!");
    StopMoving();
    ui.ShowGameOverMessage();
  }

  private void AlienKilled(int pointValue)
  {
    alienCount--;
    ui.score += pointValue;

    if (alienCount == 0) {
      Debug.Log("You won!");
      ui.ShowYouWonMessage();
    }
  }
}
