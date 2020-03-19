using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endPiece : Pipe_Piece
{
    public override void Start()
    {
        values = new int[4] { 0, 0, 1, 0 };
        speed = 0.3f;
        isGoal = 1;
        isStart = 0;
        notActiveSprite = Resources.Load<Sprite>("Art/Pipe_Art/endGray");
        activeSprite = Resources.Load<Sprite>("Art/Pipe_Art/endWater");
        spriteRenderer = GetComponent<SpriteRenderer>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<Pipe_Game_Manager>();
    }
}
