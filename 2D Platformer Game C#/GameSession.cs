using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField]
    int _playerLives = 3;
    [SerializeField]
    int _score = 0;
    [SerializeField]
    TextMeshProUGUI _livesText;
    [SerializeField]
    TextMeshProUGUI _scoreText;

    void Awake()
    {
        int _numOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if(_numOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        _livesText.text = _playerLives.ToString();
        _scoreText.text = _score.ToString();
    }

  
   public void ProcessPlayerDeath()
    {
        if (_playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToScore(int _pointsToAdd)
    {
        _score += _pointsToAdd;
        _scoreText.text = _score.ToString();
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLife()
    {
        _playerLives--;
        int _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(_currentSceneIndex);
        _livesText.text = _playerLives.ToString();
    }
}
