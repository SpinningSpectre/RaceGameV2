using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public int winCount = 3;
    [Header("Other")]
    public bool stopAtWall = true;
    public float rngSpeedAccel = 1;
    public float rngSpeedmax = 1;
    public float timeSinceLastCheck = 0;
    public float resetTime = 1;
    [Header("Current Place")]
    public int currentPlace = 0;
    public Text placement;
    public int lapCountUI;
    [SerializeField]
    float checkPointDistance;
    float counter;
    public float counterUI;
    float totalWayPoints;
    public bool isAI;
    public int lessCheckpoints;
    public int extraPointTime;
    public Sprite sprite_name_idfk_ask_mike;
    MoneyManager moneyManager;
    SaveData saveData;
    string activeUpgrade;
    SceneLoader loader;
    IconManager iconManager;
    EndingManager endingManager;
    public bool won = false;
    public float startingMaxSpeed;

    void Start()
    {
        currentCheckpoint = checkPoints[0];
        rngSpeedAccel = Random.Range(-1,1);
        accel = accel + rngSpeedAccel;
        if (rngSpeedAccel > 0)
        {
            rngSpeedmax = Random.Range(-0.5f, 0);
        }
        else if (rngSpeedAccel < 0)
        {
            rngSpeedmax = Random.Range(0, 0.5f);
        }
        else
        {
            rngSpeedmax = Random.Range(-0.5f, 0.5f);
        }
        maxSpeed = maxSpeed + rngSpeedmax;
        loader = FindObjectOfType<SceneLoader>();
        iconManager = FindObjectOfType<IconManager>();
        endingManager = FindObjectOfType<EndingManager>();
        //upgrades
        if (isAI == false)
        {
            saveData = FindObjectOfType<SaveData>();
            moneyManager = FindObjectOfType<MoneyManager>();
            activeUpgrade = saveData.upgrade;
            if (activeUpgrade != "")
            {
                Debug.Log("Has up");
                Debug.Log(activeUpgrade);
                if (activeUpgrade == "test")
                {
                    Debug.Log("Has SPEEEEEEEEEEEEEEED");
                    maxSpeed = maxSpeed + 10;
                    accel = accel + 10;
                }
                else if (activeUpgrade == "Powerup")
                {
                    GetComponentInParent<ItemScript>().RandomUpgrade();
                }
                else if (activeUpgrade == "Speed")
                {
                    maxSpeed = maxSpeed/100*110;
                }
                else if (activeUpgrade == "Accel")
                {
                    accel = accel / 100 * 110;
                }
                else if (activeUpgrade == "Turn")
                {
                    turnSpeed = turnSpeed / 100 * 130;
                } else if (activeUpgrade == "Stop")
                {
                    stoppingSpeed = stoppingSpeed / 100 * 150;
                }
                else if (activeUpgrade == "Balance")
                {
                    maxSpeed = maxSpeed / 100 * 125;
                    accel = accel / 100 * 50;
                }
                else if (activeUpgrade == "Money")
                {
                    moneyManager.moneyMultiplier = 1.2f;
                }
            }
            else
            {
                Debug.Log("No up");
            }
            startingMaxSpeed = maxSpeed;
        }
    }
    void FixedUpdate()
    {
        if (checkPointCounter - 1 >= 0)
        {
            checkPointDistance = Vector3.Distance(checkPoints[checkPointCounter - 1].transform.position, transform.position);
        }
        counter = lapCounter * 10000 + totalWayPoints * 1000 + checkPointDistance;
        if (isAI == false && checkPointCounter >= extraPointTime)
        {
            Debug.Log("AAAAAAAAAAAA");
            counter = counter + lessCheckpoints * 1000;
        }
        counterUI = counter;
        timeSinceLastCheck = timeSinceLastCheck + Time.deltaTime;
        if (gameObject.GetComponent<ItemScript>().isAI == true && timeSinceLastCheck >= resetTime)
        {
            Debug.Log("Grrrrr");
            gameObject.GetComponent<Transform>().position = currentCheckpoint.transform.position;
            timeSinceLastCheck = 0;
        }
        Vector3 velocity = Vector3.forward * speed;
        transform.Translate(velocity * Time.deltaTime, Space.Self);
        if (checkPointCounter > checkPoints.Length)
        {
            checkPointCounter = 0;
        }
        currentCheckpoint = checkPoints[checkPointCounter];
        if (isAI == false && lapCounter >= winCount || endingManager.carsEnded == 3)
        {
            if (iconManager.allCars[1] == iconManager.cars[0] || iconManager.allCars[2] == iconManager.cars[0] || iconManager.allCars[0] == iconManager.cars[0])
            {
                Debug.Log("123");
                moneyManager.GainMoney(100);
            }
            else if(iconManager.allCars[3] == iconManager.cars[0] || iconManager.allCars[4] == iconManager.cars[0] || iconManager.allCars[5] == iconManager.cars[0])
            {
                Debug.Log("456");
                moneyManager.GainMoney(50);
            }
            else
            {
                moneyManager.GainMoney(10);
            }
            loader.LoadScene("Shop");
        }else if (isAI == true && endingManager.carsEnded != 5 && lapCounter == winCount && won == false)
        {
            won = true;
            endingManager.carsEnded++;
        }
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
            totalWayPoints++;
            timeSinceLastCheck = 0;
        }
        if (checkPointCounter >= checkPoints.Length - 1 && other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Oh hell nahh");
            checkPointCounter = 0;
            lapCounter++;
            if (isAI == false)
            {
                moneyManager.GainMoney(10);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spawner"))
        {
            other.GetComponent<PowerupSpawner>().SpawnBox();
        }
    }
}
