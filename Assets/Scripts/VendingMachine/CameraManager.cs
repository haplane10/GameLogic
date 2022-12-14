using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0.0f, 0.0f, speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(0.0f, 0.0f, speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(speed, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position -= new Vector3(0f, speed, 0.0f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.position += new Vector3(0f, speed, 0.0f);
        }

    }
}
