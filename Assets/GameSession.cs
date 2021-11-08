using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameSession : MonoBehaviour
{
    public int _playerlives = 3;
    public int _score = 0;
    public Text LivesText;
    public Text ScoreText;


    private void Awake()
    {
        int numbGameSession = FindObjectsOfType<GameSession>().Length;
        if (numbGameSession > 1)
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
        LivesText.text = _playerlives.ToString();
        ScoreText.text = _score.ToString();

    }
    public void AddToScore(int pointsToAdd)
    {
        _score += pointsToAdd;
        ScoreText.text = _score.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if (_playerlives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void TakeLife()
    {
        _playerlives--;
        var currentsceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentsceneIndex);
        LivesText.text = _playerlives.ToString();
    }
    public void DestoryGameSession()

    {
        Destroy(gameObject);
    }
}
