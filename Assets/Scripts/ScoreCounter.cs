using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public static int currentScore = 0;
    private bool gameOver;
    TextMeshProUGUI Score;
    
    // Start is called before the first frame update
    void Start()
    {
        Score = GetComponent<TextMeshProUGUI> ();
    }

    // Update is called once per frame
    void Update()
    {
        gameOver = FindObjectOfType<Game_Driver>().ReturnGameOver();
        if (gameOver == false)
        {
            Check();
        }

    }
    void Check()
    {
            currentScore += 1;
            Score.text = "Score: " + currentScore;
    }

}
