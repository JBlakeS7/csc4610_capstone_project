using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Manager : MonoBehaviour
{
    public GameObject[,] Grid;
    public Vector3[,] Grid_pos;
    public GameObject tile;
    public GameObject wall;
    private int columns, rows;
    private Vector3 starting_point = new Vector3(-15.0f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        columns = 10;
        rows = 5;
        Grid = new GameObject[columns, rows];
        Grid_pos = new Vector3[columns, rows];
        Vector3 current_pos = starting_point;
        for (int i = 0; i < columns; i++)
        {
            current_pos = new Vector3(current_pos.x + 5, current_pos.y, starting_point.z);
            for (int j = 0; j < rows; j++)
            {
                SpawnTile(current_pos, i, j);
                current_pos = new Vector3(current_pos.x, current_pos.y, current_pos.z + 5);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse down");
            Camera camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit))
            {
                Debug.Log(rayHit.collider.tag);
                if (rayHit.collider.tag == "Tile")
                {
                    Transform curr_tile = rayHit.collider.gameObject.GetComponentInParent<Transform>();
                    Debug.Log("Tile at " + curr_tile.position.x + ", " + curr_tile.position.y + ", " + curr_tile.position.z + "was clicked");
                    for (int i = 0; i < columns; i++)
                    {
                        for (int j = 0; j < rows; j++)
                        {
                            if (Grid_pos[i, j].Equals(curr_tile.position))
                            {
                                Destroy(Grid[i, j].gameObject);
                                Grid[i, j] = Instantiate(wall, Grid_pos[i, j], Quaternion.identity);
                            }
                        }
                    }
                }
            }
        }
    }

    void SpawnTile(Vector3 pos, int i, int j)
    {
        Grid_pos[i, j] = pos;
        Grid[i, j] = Instantiate(tile, pos, Quaternion.identity);
    }
}
