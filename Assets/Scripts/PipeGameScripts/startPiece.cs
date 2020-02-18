using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startPiece : Pipe_Piece
{
    public override void Start()
    {
        values = new int[4] { 0, 0, 1, 0 };
        speed = 0.3f;
        isGoal = 0;
        isStart = 1;
        notActiveSprite = Resources.Load<Sprite>("Art/Pipe_Art/startGray");
        activeSprite = Resources.Load<Sprite>("Art/Pipe_Art/startWater");
        spriteRenderer = GetComponent<SpriteRenderer>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<Pipe_Game_Manager>();
    }
}
