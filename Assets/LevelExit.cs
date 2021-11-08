using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public float LevelDelaySeconds = 3f;
    public float LevelRndSlowMo = 0.5f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(GetNextLevel());
    }
    IEnumerator GetNextLevel()
    {
        Time.timeScale = LevelRndSlowMo;
        yield return new WaitForSecondsRealtime(LevelDelaySeconds);
        FindObjectOfType<ScenePersist>().DestoryScenePersist();
        Time.timeScale = 1f;
        var CurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentScene + 1);
        
    }
    public void LoadNextScene()
    {
        var CurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentScene + 1);
    }
    public void LoadPlayAgain()
    {
        Time.timeScale = 1f;
        FindObjectOfType<GameSession>().DestoryGameSession();
        SceneManager.LoadScene(0);
    }

}
