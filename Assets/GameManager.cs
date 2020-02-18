using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int lives = 0;
    public bool gameOver = false;

    public float restartDelay = 1f;

    private void Start()
    {
        Time.timeScale = 1;
        lives = 3;
    }

    void EndGame()
    {
        if(gameOver == false)
        {
            gameOver = true; 
            Debug.Log("Game Over");
            //Invoke("Restart", restartDelay);
            Time.timeScale = 0;
        }
        
    }
    public void DecreaseLives()
    {
        lives -= 1;
        if(lives == 0)
        {
            EndGame();
        }
    }

    public bool ReturnGameOver()
    {
        return gameOver;
    }

    public int ReturnNumLives()
    {
        return lives;
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Start();
    }
}
