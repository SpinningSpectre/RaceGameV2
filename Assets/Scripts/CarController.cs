using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Basic Car variables")]
    public float maxSpeed = 10;
    public float turnSpeed = 50;
    public float accel = 2.5f;
    public float speed = 0;
    public float stoppingSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeSpeed(float throttle)
    {
        if (throttle != 0)
        {
            speed = speed + accel * throttle * Time.deltaTime;
            speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);
            Vector2 velocity = Vector2.up * speed;
            transform.Translate(velocity * Time.deltaTime, Space.Self);
        }
    }
    public void Turn(float direction)
    {
        transform.Rotate(0,0,direction * -turnSpeed * Time.deltaTime);
    }
}
