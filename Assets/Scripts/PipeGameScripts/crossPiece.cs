using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossPiece : Pipe_Piece
{
    public override void Start()
    {
        values = new int[4] { 1, 1, 1, 1 };
        speed = 0.3f;
        isGoal = 0;
        isStart = 0;
        notActiveSprite = Resources.Load<Sprite>("Art/Pipe_Art/crossGray");
        activeSprite = Resources.Load<Sprite>("Art/Pipe_Art/crossWater");
        spriteRenderer = GetComponent<SpriteRenderer>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<Pipe_Game_Manager>();
    }
}
