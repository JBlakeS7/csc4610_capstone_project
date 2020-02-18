using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObstacle : MonoBehaviour
{
    public float speed = 10.0f;
    public Rigidbody rigidbody;
    private Vector3 _screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
        FindObjectOfType<Game_Driver>().DecreaseLives();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.forward * Time.deltaTime * Game_Driver.currentSpeed * speed;

        if (transform.position.z < _screenBounds.z * 20)
        {
            Destroy(this.gameObject);
            //Debug.Log("Car Destroyed");
        }
    }
}
