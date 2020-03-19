using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driving_Player : MonoBehaviour
{
    public Rigidbody rb;

    public float speed = 7.5f;
    int currLane = 2;
    int targetLane;
    static float y = -1.06f;
    static float z = -13.14683f;
    private float minDistanceForSwipe = 20f;
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private bool alreadyMoved = false;

    public Vector3[] lanes = new [] {new Vector3(-2.5f, y, z), new Vector3(1.25f, y, z), new Vector3(5.0f, y, z), new Vector3(9.0f, y, z)};

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Moved && !alreadyMoved)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                alreadyMoved = false;
            }
        }

        if (Input.GetKeyDown("d") && currLane < 3)
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.AddForce(speed * Time.deltaTime * Game_Driver.currentSpeed, 0, 0, ForceMode.VelocityChange);
            currLane += 1;
        } else if (Input.GetKeyDown("a") && currLane > 0)
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.AddForce(-speed * Time.deltaTime * Game_Driver.currentSpeed, 0, 0, ForceMode.VelocityChange);
            currLane -= 1;
        }

        float step = speed * Time.deltaTime * Game_Driver.currentSpeed;
        transform.position = Vector3.MoveTowards(transform.position, lanes[currLane], step);
    }

    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            double direction = fingerDownPosition.x - fingerUpPosition.x;
            if (direction >= 0 && currLane < 3)
            {
                rb.velocity = new Vector3(0f, 0f, 0f);
                rb.AddForce(speed * Time.deltaTime * Game_Driver.currentSpeed, 0, 0, ForceMode.VelocityChange);
                currLane += 1;
            }
            else if (direction < 0 && currLane > 0)
            {
                rb.velocity = new Vector3(0f, 0f, 0f);
                rb.AddForce(-speed * Time.deltaTime * Game_Driver.currentSpeed, 0, 0, ForceMode.VelocityChange);
                currLane -= 1;
            }

            fingerUpPosition = fingerDownPosition;
            alreadyMoved = true;
        }
    }

    private bool SwipeDistanceCheckMet()
    {
        return HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }
}
