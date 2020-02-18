using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesCounter : MonoBehaviour
{
    private static int lives;
    private bool gameOver;
    TextMeshProUGUI Lives;

    // Start is called before the first frame update
    void Start()
    {
 
        Lives = GetComponent<TextMeshProUGUI> ();
        
    }

    // Update is called once per frame
    void Update()
    {
        gameOver = FindObjectOfType<Game_Driver>().ReturnGameOver();
        lives = FindObjectOfType<Game_Driver>().ReturnNumLives();
        if (gameOver == false)
        {
            Check(lives);
        }
        if (gameOver)
        {
            Lives.text = "Lives: 0";
        }
    }

    void Check(int lives)
    {

        Lives.text = "Lives: " + lives;
    }
}
