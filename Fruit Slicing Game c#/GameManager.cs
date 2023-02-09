using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    [Header("Scoring Elements")]
    public int _score;
    public int _highScore;
    public Text _scoreText;
    public Text _gameOverPanelScoreText;
    public Text _gameOverPanelHighScoreText;
    public Text _highScoreText;

    [Header("Game Over Elements")]
    public GameObject _gameOverPanel;

    private void Awake()
    {
        Advertisement.Initialize("5154289");
        _gameOverPanel.SetActive(false);
        GetHighScore();
    }

    public void GetHighScore()
    {
        _highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreText.text = "Best Score: " + _highScore;
        
    }
    
    public void IncreaseScore(int _points)
    {
        _score += _points;
        _scoreText.text = _score.ToString();

        if(_score > _highScore)
        {
            PlayerPrefs.SetInt("HighScore", _score);
            _highScoreText.text = "Best Score :" + _highScore.ToString();
        }
    }

    public void OnBombHit()
    {
        Advertisement.Show();
        
        Time.timeScale = 0;

        _gameOverPanelScoreText.text = "Your Score: " + _score.ToString();
        _gameOverPanelHighScoreText.text = "Best Score: " + _highScore.ToString();
        _gameOverPanel.SetActive(true);
        
        Debug.Log("Bomb was hit");
    }

    public void RestartGame()
    {
        _score = 0;
        _scoreText.text = _score.ToString();

        _gameOverPanel.SetActive(false);
        //_gameOverPanelScoreText.text = "Your Score: 0";

            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            Destroy(g);
        }

        Time.timeScale = 1;
    }

}
