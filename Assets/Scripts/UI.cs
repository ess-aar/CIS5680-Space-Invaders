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

  UI uiObj;
  Text scoreText;
  Text hiScoreText;
  Text livesText;
  Text outcomeText;
  public System.Action gameover;

  void Start()
  {
    hiScore = HiScore.hiScore;

    scoreText = GameObject.Find("Canvas").transform.Find("Score").GetComponent<Text>();
    hiScoreText = GameObject.Find("Canvas").transform.Find("HiScore").GetComponent<Text>();
    hiScoreText.text = "HiScore " + this.hiScore.ToString();
    livesText = GameObject.Find("Canvas").transform.Find("Lives").GetComponent<Text>();
    outcomeText = GameObject.Find("Canvas").transform.Find("Outcome").GetComponent<Text>();
  }

  void FixedUpdate()
  {
    if (playing && lives == 0) {
      Debug.Log("No lives left!");
      ShowGameOverMessage();
      this.gameover.Invoke();
      playing = false;
    }
  }
  
  void Update()
  {
    if (playing) {
      scoreText.text = "Score " + this.score.ToString();
      livesText.text = "Lives " + this.lives.ToString();

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