using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tPiece : Pipe_Piece
{
    public override void Start()
    {
        values = new int[4] { 0, 1, 1, 1 };
        speed = 0.3f;
        isGoal = 0;
        isStart = 0;
        notActiveSprite = Resources.Load<Sprite>("Art/Pipe_Art/tGray");
        activeSprite = Resources.Load<Sprite>("Art/Pipe_Art/tWater");
        spriteRenderer = GetComponent<SpriteRenderer>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<Pipe_Game_Manager>();
    }
}
