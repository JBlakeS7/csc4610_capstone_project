using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Driver : MonoBehaviour
{
    public GameObject road;
    public GameObject pauseMenu;
    public GameObject scoreCounter;
    public static float currentSpeed = 1;
    public static int lives = 0;
    public bool gameOver = false;

    public float restartDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(road, new Vector3(64.625f, -326.875f, 478.625f), Quaternion.identity);
        Instantiate(road, new Vector3(64.625f, -326.875f, 520.625f), Quaternion.identity);
        lives = 3;
        currentSpeed = 1;
        ScoreCounter.currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreCounter.currentScore % 1000 == 0)
        {
            currentSpeed *= 1.2f;
            //Debug.Log(currentSpeed);
        }

        if (Input.GetKeyDown("p"))
        {
            if (!pauseMenu.activeInHierarchy)
            {
                PauseGame();
            } else if (pauseMenu.activeInHierarchy)
            {
                ResumeGame();
            }
        }
    }
    void EndGame()
    {
        if (gameOver == false)
        {
            gameOver = true;
            //Debug.Log("Game Over");
            //Invoke("Restart", restartDelay);
            Time.timeScale = 0;
        }

    }
    public void DecreaseLives()
    {
        lives -= 1;
        if (lives == 0)
        {
            //EndGame();
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

    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        scoreCounter.GetComponent<ScoreCounter>().enabled = false;
    }

    private void ResumeGame()
    {
        Time.timeScale = currentSpeed;
        pauseMenu.SetActive(false);
        scoreCounter.GetComponent<ScoreCounter>().enabled = true;
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Start();
    }

    public void PauseButton()
    {
        if (!pauseMenu.activeInHierarchy)
        {
            PauseGame();
        }
        else if (pauseMenu.activeInHierarchy)
        {
            ResumeGame();
        }
    }

    public void ResumeButton()
    {
        ResumeGame();
    }

    public void SettingsButton()
    {
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
    }
}
