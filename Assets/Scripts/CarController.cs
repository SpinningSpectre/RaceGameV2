using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [Header("Basic Car variables")]
    public float maxSpeed = 10;
    public float turnSpeed = 50;
    public float accel = 2.5f;
    public float speed = 0;
    public float stoppingSpeed = 5;
    public float bounceSpeed = 1;
    [Header("Level Stuff")]
    public GameObject[] checkPoints;
    public GameObject currentCheckpoint;
    public int checkPointCounter = 0;
    public int lapCounter = 0;
    [Header("Other")]
    public bool stopAtWall = true;
    public float rngSpeed = 1;
    [Header("Current Place")]
    public int currentPlace = 0;
    public Text placement;
    // Start is called before the first frame update
    void Start()
    {
        rngSpeed = Random.Range(95, 105);
        rngSpeed = rngSpeed / 10;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 velocity = Vector3.forward * speed;
        transform.Translate(velocity * Time.deltaTime, Space.Self);
        if (checkPointCounter >= checkPoints.Length)
        {
            checkPointCounter = 0;
        }
        currentCheckpoint = checkPoints[checkPointCounter];
    }
    public void ChangeSpeed(float throttle)
    {
        if (throttle != 0)
        {
            speed = speed + accel * throttle * Time.deltaTime;
            speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);
        }
        if (throttle == 0)
        {
            if (speed > 0)
            {
                speed = speed - stoppingSpeed * Time.deltaTime;
                speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);
            }
            if (speed < 0)
            {
                speed = speed + stoppingSpeed * Time.deltaTime;
                speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);
            }
        }
    }
    public void Turn(float direction)
    {
        transform.Rotate(0, direction * turnSpeed * Time.deltaTime, 0);
    }
    public void ChangePlacement()
    {
        //placement.text = currentPlace.ToString();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (stopAtWall == true)
        {
            if (speed >= 1 && collision.gameObject.CompareTag("SpeedBoost") != true)
            {
                speed = -bounceSpeed;
            }
            else if (speed <= -1 && collision.gameObject.CompareTag("SpeedBoost") != true)
            {
                speed = bounceSpeed;
            }
            if (speed >= 1 && collision.gameObject.CompareTag("NotBounce") != true)
            {
                speed = -bounceSpeed;
            }
            else if (speed <= -1 && collision.gameObject.CompareTag("NotBounce") != true)
            {
                speed = bounceSpeed;
            }
        }
        if (collision.gameObject.CompareTag("SpeedBoost") == true)
        {
            bounceSpeed = -50;
            maxSpeed = 50;
            speed = 50;
            Debug.Log("WEEEEEEEEEEEE");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("SpeedBoost") == true)
        {
            bounceSpeed = 1;
            maxSpeed = 10;
            speed = 10;
            Debug.Log("no");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == currentCheckpoint)
        {
            checkPointCounter++;
        }
        if (checkPointCounter >= checkPoints.Length - 1 && other.gameObject.CompareTag("Finish"))
        {
            checkPointCounter = 0;
            lapCounter++;
        }
    }
}
