﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Unity.UI;

public class piece : MonoBehaviour
{
    public int[] values;
    public float speed;
    public int uniqueID;

    public int hasBeenChecked;

    float realRotation;

    public int isActive;
    public int isGoal;
    public int isStart;

    public Sprite notActiveSprite;
    public Sprite activeSprite;

    private SpriteRenderer spriteRenderer;


    public Pipe_Game_Manager gm;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<Pipe_Game_Manager>();
        /*  if (isStart != 1)
          {
              isActive = 0;
          }
          else
          {
              isActive = 1;
          }
          hasBeenChecked = 0;
          changeSkin();
          */
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.root.eulerAngles.z != realRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, realRotation), speed);

        }
        if (isGoal == 1 && isActive == 1)
        {
            gm.youWin();
        }
    }


    public void changeSkin()
    {
        if (isActive == 1)
        {
            spriteRenderer.sprite = activeSprite;
        }
        else
        {
            spriteRenderer.sprite = notActiveSprite; ;
        }
    }

    private void OnMouseDown()
    {
        if (gm.gameOver == 0)
        {
            //gm.startTurn();
            RotatePiece();
            gm.reStart();
            //isActive = 0;
            // gm.Check((int)transform.position.x, (int)transform.position.y);
            // gm.fullSweep();
            // gm.endTurn();
        }
    }

    public void RotatePiece()
    {

        if (isGoal != 1 && isStart != 1)
        {
            realRotation += 90;

            if (realRotation == 360)
            {
                realRotation = 0;
            }

            RotateValues();

        }
    }

    public void RotateValues()
    {
        int aux = values[0];

        for (int i = 0; i < values.Length - 1; i++)
        {
            values[i] = values[i + 1];
        }
        values[3] = aux;
    }
}
