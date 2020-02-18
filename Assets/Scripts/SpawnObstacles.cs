using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    //Car Obsticles
    public GameObject CarObstacle;
    //public GameObject Car2;

    public float respawnTime = 1.0f;
    public int minSpawnLanes;
    public int maxSpawnLanes;
   
    public GameObject[] carPrefabs;

    private Vector3 _screenBounds;
    private float[] _lane = { -2.8f, 1.0f, 4.8f, 8.85f };
    private float _carY = -0.75f;

    // Start is called before the first frame update
    void Start()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(SpawnEnum());
    }

    private void SpawnObstacle()
    {
        float[] spawnPositions = GetSpawnPositions();
        int randomCar;
        foreach(float x in spawnPositions)
        {
            if(x!=0)
            {
                randomCar = Random.Range(0, 6);
                GameObject car = Instantiate(carPrefabs[randomCar]) as GameObject;
                car.transform.position = new Vector3(x, _carY, _screenBounds.z * -20);
                //Debug.Log("Car Spawned");
            }
        }
            
    }

    private float[] GetSpawnPositions()
    {
        int numSpawnLanes = Random.Range(minSpawnLanes, maxSpawnLanes);
        //Debug.Log(numSpawnLanes);
        float[] spawnPosition = { 0, 0, 0};
        float laneVal = 0;
        bool spawnable = false;

        for (int i = 0; i < numSpawnLanes; i++)
        {
            spawnable = false;
            while (!spawnable)
            {
                laneVal = _lane[Random.Range(0, 4)];
                //Debug.Log(laneVal);
                if (spawnPosition[0] != laneVal && spawnPosition[1] != laneVal && spawnPosition[2] != laneVal)
                    spawnable = true;
            }
            spawnPosition[i] = laneVal;
        }

        return spawnPosition;
    }

    IEnumerator SpawnEnum()
    {
        while (true) //Change true to be some bool condition later
        {
            yield return new WaitForSeconds(respawnTime); 
            SpawnObstacle();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
