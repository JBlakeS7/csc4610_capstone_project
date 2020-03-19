using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Road : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject road;

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.forward * Time.deltaTime * Game_Driver.currentSpeed * speed;


        if (transform.position.z <= 436.625f)
        {
            Instantiate(road, new Vector3(64.625f, -326.875f, 520.625f), Quaternion.identity);
            Destroy(transform.gameObject);
            //Debug.Log("Destroyed");
        }
    }
}
