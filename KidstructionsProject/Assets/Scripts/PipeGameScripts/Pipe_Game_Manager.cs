using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class Pipe_Game_Manager : MonoBehaviour
{

    public GameObject canvas;
    public int gameOver;
    string jsonPath;
    string sceneName;


    [System.Serializable]
    public class Puzzle
    {

        public int width;
        public int height;

        public Pipe_Piece[,] pieces;
    }

    public Puzzle puzzle;
    public infoList informationList;// = new infoList();
    public Pipe_Piece pipe;
    Queue<Pipe_Piece> pipesToBeChecked = new Queue<Pipe_Piece>();


    // Start is called before the first frame update
    void Start()
    {
        spawnPuzzle();

        gameOver = 0;
        canvas.SetActive(false);
        Vector2 dimensions = CheckDimensions();

        puzzle.width = (int)dimensions.x;
        puzzle.height = (int)dimensions.y;

        puzzle.pieces = new Pipe_Piece[puzzle.width, puzzle.height];

        int counter = 1;

        foreach (var piece in GameObject.FindGameObjectsWithTag("Piece"))
        {
            puzzle.pieces[(int)piece.transform.position.x, (int)piece.transform.position.y] = piece.GetComponent<Pipe_Piece>();
            puzzle.pieces[(int)piece.transform.position.x, (int)piece.transform.position.y].uniqueID += counter;
            counter++;

        }

        //shuffle();
        reStart();
    }


    public void spawnPuzzle()
    {
        Scene currScene = SceneManager.GetActiveScene();
        sceneName = currScene.name;
        // int x;
        //int y;

        if (sceneName == "Pipe_test")
        {
            //jsonPath = Application.streamingAssetsPath + "/json/Pipe_test.json";
            // json = File.ReadAllText(jsonPath);
            TextAsset asset = Resources.Load("json/Pipe_test") as TextAsset;
            if (asset != null)
            {
                informationList = JsonUtility.FromJson<infoList>(asset.text);
                for (int i = 0; i < informationList.info.Count; i++)
                {
                    Debug.Log(informationList.info[i].type);
                    Debug.Log(informationList.info[i].xPos);
                    Debug.Log(informationList.info[i].yPos);
                    Debug.Log(informationList.info[i].numRotates);
                }
            }
            else
            {
                Debug.Log("json not loaded");
            }

        }
        foreach (info i in informationList.info)
        {
            int x = i.xPos;
            int y = i.yPos;
            int rotate = i.numRotates;
            if (i.type == "angle")
            {
                pipe = Instantiate(Resources.Load("PipeGamePrefabs/angle"), new Vector3(x, y, 0), Quaternion.Euler(0, 0, 0)) as Pipe_Piece;
            }
            if (i.type == "straight")
            {
                pipe = Instantiate(Resources.Load("PipeGamePrefabs/straight"), new Vector3(x, y, 0), Quaternion.Euler(0, 0, 0)) as Pipe_Piece;
            }
            if (i.type == "cross")
            {
                pipe = Instantiate(Resources.Load("PipeGamePrefabs/cross"), new Vector3(x, y, 0), Quaternion.Euler(0, 0, 0)) as Pipe_Piece;
            }
            if (i.type == "t")
            {
                pipe = Instantiate(Resources.Load("PipeGamePrefabs/t"), new Vector3(x, y, 0), Quaternion.Euler(0, 0, 0)) as Pipe_Piece;
            }
            if (i.type == "end")
            {
                pipe = Instantiate(Resources.Load("PipeGamePrefabs/end"), new Vector3(x, y, 0), Quaternion.Euler(0, 0, 0)) as Pipe_Piece;
            }
            if (i.type == "start")
            {
                pipe = Instantiate(Resources.Load("PipeGamePrefabs/start"), new Vector3(x, y, 0), Quaternion.Euler(0, 0, 0)) as Pipe_Piece;
            }

            Debug.Log("spawning at");
            Debug.Log(i.xPos);
            Debug.Log(i.yPos);
            Debug.Log(i.type);
            Debug.Log(i.numRotates);   

            for (int q = 0; q < rotate; q++)
            {
               // pipe.RotatePiece();
            }
        }
    }




    public void checkPipes()
    {
        Pipe_Piece currPiece;
        while (pipesToBeChecked.Count > 0)
        {
            currPiece = pipesToBeChecked.Dequeue();
            currPiece.isActive = 1;

            currPiece.changeSkin();
            if (currPiece.hasBeenChecked == 0)
            {
                if (currPiece.isGoal == 0)
                {
                    for (int w = 0; w < puzzle.width; w++)
                    {
                        for (int h = 0; h < puzzle.height; h++)
                        {
                            if (puzzle.pieces[w, h].uniqueID == currPiece.uniqueID)
                            {
                                //up
                                if (h != puzzle.height - 1)
                                {
                                    if (puzzle.pieces[w, h].values[0] == 1 && puzzle.pieces[w, h + 1].values[2] == 1)
                                    {
                                        if (puzzle.pieces[w, h + 1].hasBeenChecked == 0)
                                        {
                                            pipesToBeChecked.Enqueue(puzzle.pieces[w, h + 1]);
                                        }
                                    }
                                }

                                //right
                                if (w != puzzle.width - 1)
                                {
                                    if (puzzle.pieces[w, h].values[1] == 1 && puzzle.pieces[w + 1, h].values[3] == 1)
                                    {
                                        if (puzzle.pieces[w + 1, h].hasBeenChecked == 0)
                                        {
                                            pipesToBeChecked.Enqueue(puzzle.pieces[w + 1, h]);
                                        }
                                    }
                                }

                                //down
                                if (h != 0)
                                {
                                    if (puzzle.pieces[w, h].values[2] == 1 && puzzle.pieces[w, h - 1].values[0] == 1)
                                    {
                                        if (puzzle.pieces[w, h - 1].hasBeenChecked == 0)
                                        {
                                            pipesToBeChecked.Enqueue(puzzle.pieces[w, h - 1]);
                                        }
                                    }
                                }

                                //left
                                if (w != 0)
                                {
                                    if (puzzle.pieces[w, h].values[3] == 1 && puzzle.pieces[w - 1, h].values[1] == 1)
                                    {
                                        if (puzzle.pieces[w - 1, h].hasBeenChecked == 0)
                                        {
                                            pipesToBeChecked.Enqueue(puzzle.pieces[w - 1, h]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    youWin();
                }
            }
            currPiece.hasBeenChecked = 1;
        }

    }

    public void reStart()
    {
        foreach (var piece in puzzle.pieces)
        {
            if (piece.isStart == 0)
            {
                piece.isActive = 0;
            }
            else
            {
                piece.isActive = 1;
                pipesToBeChecked.Enqueue(piece);
            }
            piece.hasBeenChecked = 0;
            piece.changeSkin();
        }
        checkPipes();
    }

   /* public void shuffle()
    {
        Debug.Log("shuffle");
        foreach (var piece in puzzle.pieces)
        {
            int k = Random.Range(0, 4);

            for (int i = 0; i < k; i++)
            {
                piece.RotatePiece();
                piece.changeSkin();
            }
        }
    }*/

    Vector2 CheckDimensions()
    {
        Vector2 aux = Vector2.zero;

        GameObject[] pieces = GameObject.FindGameObjectsWithTag("Piece");

        foreach (var p in pieces)
        {
            if (p.transform.position.x > aux.x)
            {
                aux.x = p.transform.position.x;
            }
            if (p.transform.position.y > aux.y)
            {
                aux.y = p.transform.position.y;
            }
        }
        aux.x++;
        aux.y++;

        return aux;
    }

    public void youWin()
    {
        gameOver = 1;
        Debug.Log("you win");
        canvas.SetActive(true);
    }

    public void NextLevel(string nextLevel)
    {
        SceneManager.LoadScene(nextLevel);
    }


    // Update is called once per frame
    void Update()
    {

    }

}
