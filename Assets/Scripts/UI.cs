using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
  private bool playing = true;

  public int score = 0;
  public int hiScore = 0;
  public int lives = 3;
  public int powerups = 0;
  public int powerupsNeeded = 5;

  UI uiObj;
  Text scoreText;
  Text hiScoreText;
  Text livesText;
  Text outcomeText;
  Text powerupText;
  public System.Action gameover;

  void Start()
  {
    hiScore = HiScore.hiScore;

    scoreText = GameObject.Find("Canvas").transform.Find("Score").GetComponent<Text>();
    hiScoreText = GameObject.Find("Canvas").transform.Find("HiScore").GetComponent<Text>();
    hiScoreText.text = "HiScore " + this.hiScore.ToString();
    livesText = GameObject.Find("Canvas").transform.Find("Lives").GetComponent<Text>();
    outcomeText = GameObject.Find("Canvas").transform.Find("Outcome").GetComponent<Text>();
    powerupText = GameObject.Find("Canvas").transform.Find("Powerups").GetComponent<Text>();
  }

  void FixedUpdate()
  {
    if (playing && lives == 0) {
      Debug.Log("No lives left!");
      ShowGameOverMessage();
      this.gameover.Invoke();
      playing = false;
    }

    if (playing && this.powerups >= this.powerupsNeeded)
    {
      Debug.Log("Enough Power ups collected!");
      GameObject.Find("Ship").GetComponent<Ship>().PowerupsCollected();
    }
  }
  
  void Update()
  {
    if (playing) {
      scoreText.text = "Score " + this.score.ToString();
      livesText.text = "Lives " + this.lives.ToString();
      powerupText.text = "Powerup " + this.powerups.ToString();

      if (hiScore < score) {
        hiScore = score;
        hiScoreText.text = "HiScore " + this.hiScore.ToString();
      }
    }
  }

  public void ShowGameOverMessage()
  {
    outcomeText.text = "You Lost!\nGame Over.";
  }

  public void ShowYouWonMessage()
  {
    outcomeText.text = "You Won!";
  }

  public void ShowYouEscapedMessage()
  {
    outcomeText.text = "You Escaped!";
  }

  public void NewGame()
  {
    HiScore.hiScore = this.hiScore;
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}

public static class HiScore 
{
    public static int hiScore { get; set; }
}