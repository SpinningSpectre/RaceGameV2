using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms;
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
    [Header("Other")]
    public bool stopAtWall = true;
    public float rngSpeedAccel = 1;
    public float rngSpeedmax = 1;
    public float timeSinceLastCheck = 0;
    public float resetTime = 1;
    public MoneyManager moneyManager;
    public SaveData saveData;
    public string activeUpgrade;
    public string[] upgrades = { "Powerup", "Speed", "Accel", "Stop", "Balance" };
    SceneLoader loader;
    IconManager iconManager;
    EndingManager endingManager;
    TimeScript timerCode;
    public bool won = false;
    public float startingMaxSpeed;
    public bool allowedToMove = true;
    bool stunned = false;
    public string playerName;

    void Start()
    {
        currentCheckpoint = checkPoints[0];
        rngSpeedAccel = Random.Range(-0.25f,0.25f);
        accel = accel + rngSpeedAccel;
        if (rngSpeedAccel > 0)
        {
            rngSpeedmax = Random.Range(-0.1f, 0);
        }
        else if (rngSpeedAccel < 0)
        {
            rngSpeedmax = Random.Range(0, 0.1f);
        }
        else
        {
            rngSpeedmax = Random.Range(-0.1f, 0.1f);
        }
        maxSpeed = maxSpeed + rngSpeedmax;
        loader = FindObjectOfType<SceneLoader>();
        iconManager = FindObjectOfType<IconManager>();
        endingManager = FindObjectOfType<EndingManager>();
        timerCode = FindObjectOfType<TimeScript>();
        //upgrades
        saveData = FindObjectOfType<SaveData>();
        activeUpgrade = saveData.upgrade;
        if (isAI == false)
        {
            playerName = saveData.GetString("PlayerName");
        }
        if (playerName == "Rowan Car")
        {
            maxSpeed = 7;
            accel = 2;
        }
        if (activeUpgrade != "")
        {
            if (isAI == true)
            {
                int i = Random.Range(0, upgrades.Length);
                Debug.Log(i);
                activeUpgrade = upgrades[i];
                switch (activeUpgrade)
                {
                    case "Powerup":
                        GetComponentInParent<ItemScript>().RandomUpgrade();
                        break;
                    case "Speed":
                        maxSpeed = maxSpeed / 100 * 105;
                        break;
                    case "Accel":
                        accel = accel / 100 * 110;
                        break;
                    case "Stop":
                        stoppingSpeed = stoppingSpeed / 100 * 150;
                        break;
                    case "Balance":
                        maxSpeed = maxSpeed / 100 * 125;
                        accel = accel / 100 * 75;
                        break;
                }
            } else if (isAI == false)
            {
                moneyManager = FindObjectOfType<MoneyManager>();
                switch (activeUpgrade)
                {
                    case "Powerup":
                        GetComponentInParent<ItemScript>().UpgradedUpgrades();
                        break;
                    case "Speed":
                        maxSpeed = maxSpeed / 100 * 105;
                        break;
                    case "Accel":
                        accel = accel / 100 * 110;
                        break;
                    case "Turn":
                        turnSpeed = turnSpeed / 100 * 130;
                        break;
                    case "Stop":
                        stoppingSpeed = stoppingSpeed / 100 * 150;
                        break;
                    case "Balance":
                        maxSpeed = maxSpeed / 100 * 125;
                        accel = accel / 100 * 75;
                        break;
                    case "Money":
                        moneyManager.moneyMultiplier = 1.5f;
                        break;
                }
                if (activeUpgrade != "Money")
                {
                    moneyManager.moneyMultiplier = 1f;
                }
            }
        }
        startingMaxSpeed = maxSpeed;
        winCount = saveData.GetInt("Laps");
    }
    void FixedUpdate()
    {
        if (timerCode.beforeStart <= 0)
        {
            allowedToMove = true;
        }
        else
        {
            allowedToMove = false;
        }
        if (saveData.GetBool("DevMode") == true)
        {
            if (Input.GetKey(KeyCode.Backspace))
            {
                accel = 50;
                if (checkPointCounter < checkPoints.Length - 1)
                {
                    var rotationVector = transform.rotation.eulerAngles;
                    rotationVector.y = -60;
                    transform.rotation = transform.rotation = Quaternion.Euler(rotationVector);
                    transform.position = currentCheckpoint.transform.position;
                }
            }
        }
        if (checkPointCounter - 1 >= 0)
        {
            checkPointDistance = Vector3.Distance(checkPoints[checkPointCounter - 1].transform.position, transform.position);
        }
        counter = lapCounter * 10000 + totalWayPoints * 1000 + checkPointDistance;
        if (isAI == false && checkPointCounter >= extraPointTime)
        {
            counter = counter + lessCheckpoints * 1000;
        }
        counterUI = counter;
        timeSinceLastCheck = timeSinceLastCheck + Time.deltaTime;
        if (gameObject.GetComponent<ItemScript>().isAI == true && timeSinceLastCheck >= resetTime)
        {
            gameObject.GetComponent<Transform>().position = currentCheckpoint.transform.position;
            timeSinceLastCheck = 0;
        }
        Vector3 velocity = Vector3.forward * speed;
        transform.Translate(velocity * Time.deltaTime, Space.Self);
        if (checkPointCounter > checkPoints.Length - 1)
        {
            checkPointCounter = 0;
        }
        /*        try
                {
                    currentCheckpoint = checkPoints[checkPointCounter];
                } catch
                {

                }*/
        currentCheckpoint = checkPoints[checkPointCounter];

        if (isAI == false && lapCounter >= winCount || endingManager.carsEnded == 3)
        {
            if (iconManager.allCars[1] == iconManager.cars[0] || iconManager.allCars[2] == iconManager.cars[0] || iconManager.allCars[0] == iconManager.cars[0])
            {
                moneyManager.GainMoney(100);
                saveData.Wins(1);
            }
            else if(iconManager.allCars[3] == iconManager.cars[0] || iconManager.allCars[4] == iconManager.cars[0] || iconManager.allCars[5] == iconManager.cars[0])
            {
                moneyManager.GainMoney(50);
                saveData.Wins(1);
            }
            else
            {
                moneyManager.GainMoney(10);
            }
            timerCode.timerActive = false;
            timerCode.SaveTime();
            iconManager.SavePlace();
            loader.LoadScene("EndScreen");
        }
        else if (isAI == true && endingManager.carsEnded != 5 && lapCounter == winCount && won == false)
        {
            won = true;
            Debug.Log(playerName + " Has ended");
            endingManager.carsEnded++;
        }
        if (stunned == true)
        {
            maxSpeed = 0;
        }
    }
    public void ChangeSpeed(float throttle)
    {
        if (allowedToMove == true)
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
    }
    public void Turn(float direction)
    {
        if (allowedToMove == true)
        {
            if (speed == maxSpeed || speed == -maxSpeed)
            {
                transform.Rotate(0, direction * turnSpeed * Time.deltaTime, 0);
            }
            else if (speed < maxSpeed && (speed >= maxSpeed / 2) || speed <= (-maxSpeed / 2) && speed > -maxSpeed)
            {
                transform.Rotate(0, direction * (turnSpeed/1.5f) * Time.deltaTime, 0);
            } else if(speed < (maxSpeed / 2) && speed >= (maxSpeed / 10) || speed <= (-maxSpeed / 10) && speed > (-maxSpeed / 2))
            {
                transform.Rotate(0, direction * (turnSpeed /2) * Time.deltaTime, 0);
            }else if(speed > 1 && speed < -1)
            {
                transform.Rotate(0, direction,0);
            }
        }
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
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("SpeedBoost") == true)
        {
            bounceSpeed = 1;
            maxSpeed = 10;
            speed = 10;
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
            checkPointCounter = 0;
            lapCounter++;
            if (isAI == false)
            {
                moneyManager.GainMoney(10);
            }
        }
        if (other.gameObject.CompareTag("Stun"))
        {
            stunned = true;
            Invoke("UnStun", 1f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spawner"))
        {
            other.GetComponent<PowerupSpawner>().SpawnBox();
        }
    }
    public void UnStun()
    {
        stunned = false;
        maxSpeed = startingMaxSpeed;
    }
}
