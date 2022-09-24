using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGrid : MonoBehaviour
{
  public int rows = 5;
  public int cols = 11;

  public Alien[] aliens;
  public UFO ufo;
  public Powerup powerup;
  public Powerup powerupPrefab;

  private int alienCount;

  [SerializeField] public float xOffset = 1.0f;
  [SerializeField] public float yOffset = 1.0f;

  [SerializeField] public Vector3 direction = Vector3.right;
  [SerializeField] public float speed = 1.0f;
  [SerializeField] public float speedIncrementFactor = 1.5f;
  [SerializeField] public Vector3 ufoDirection = Vector3.left;
  [SerializeField] public float ufoSpeed = 1.0f;

  [SerializeField] public float missileRate = 2.0f;
  public Ammo missile;

  private int stepCounter = 0;
  private int ufoStepCounter = 0;
  public bool move = true;
  private UI ui;
  SoundManager sound;
  public float timeRemaining = 10.0f;
  private bool spawnPowerup = true;
  private Vector2 spawnTimeRange = new Vector2(5.0f, 10.0f);

  private void Awake()
  {
    float height = this.yOffset * (this.rows - 1);
    float width = this.xOffset * (this.cols - 1);
    alienCount = this.rows * this.cols;

    for (int row = 0; row < this.rows; row++) {
      for (int col = 0; col < this.cols; col++) {
        Alien alien = Instantiate(this.aliens[row], this.transform);
        alien.gameObject.layer = LayerMask.NameToLayer("Alien");
        alien.killed += AlienKilled;
        alien.hitEdge += MoveVertically;
        alien.hitBottom += ReachedBottom;

        Vector3 position = new Vector3(col * this.xOffset, row * this.yOffset, 0.0f);
        position -= new Vector3(width / 2, height / 2, 0);

        alien.transform.localPosition = position;
      }
    }
    ufo = Instantiate(ufo, new Vector3(-100.0f, this.transform.position.y + 100.0f, 0.0f), Quaternion.identity);
    ufo.gameObject.layer = LayerMask.NameToLayer("Alien");
    ufo.killed += UFOKilled;

    GameObject obj = GameObject.Find("GlobalUIObject");
    ui = obj.GetComponent<UI>();
    ui.gameover += StopMoving;

    timeRemaining = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
  }
  
  private void Start()
  {
    GameObject obj = GameObject.Find("SoundManager");
    sound = obj.GetComponent<SoundManager>();
    InvokeRepeating("ShootMissile",  1.0f, 1.0f / missileRate);
  }

  private void FixedUpdate()
  {
    if (timeRemaining > 0)
    {
      timeRemaining -= Time.deltaTime;
    } else if (spawnPowerup)
    {
      powerup = Instantiate(powerupPrefab, new Vector3(Random.Range(100.0f, 1000.0f), 20.0f, 0.0f), Quaternion.identity);
      // powerup.gameObject.layer = LayerMask.NameToLayer("Powerup");
      powerup.collected += CollectedPowerup;
      spawnPowerup = false;
    }

    if (move && stepCounter > 60) {
      // this.transform.position += this.direction * this.speed;
      this.transform.position += this.direction * this.speed * Time.deltaTime;
    }
    stepCounter++;

    if (this.ufo.transform.position.x < Mathf.Infinity)
    {
      this.ufo.transform.position += this.ufoDirection * this.ufoSpeed * Time.deltaTime;
      if (this.ufo.transform.position.x > 1100 || this.ufo.transform.position.x < -700) {
        ufoStepCounter = 0;
        this.ufoDirection.x *= -1.0f;
        this.ufoSpeed *= this.speedIncrementFactor;
      }
      ufoStepCounter++;
    }
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
        sound.AlienFired();
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

  public void ShipEscaped()
  {
    Debug.Log("You escaped!");
    StopMoving();
    ui.ShowYouEscapedMessage();
  }

  private void UFOKilled(int pointValue)
  {
    ui.score += pointValue;
    Debug.Log("You got the Alien UFO!");

    ufo = Instantiate(ufo, new Vector3(-100.0f, this.transform.position.y + 100.0f, 0.0f), Quaternion.identity);
    ufo.gameObject.layer = LayerMask.NameToLayer("Alien");
    ufo.killed += UFOKilled;
  }

  private void CollectedPowerup()
  {
    Debug.Log("powerup++");
    ui.powerups++;
    powerup = null;
    timeRemaining = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
    spawnPowerup = true;
  }
}
