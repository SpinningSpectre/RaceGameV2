using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [Header("Upgrades")]
    public int randomUpgrade = 0;
    public bool banana = false;
    public bool banned = false;
    public bool scale = false;
    public bool jump = false;
    public bool reference = false;
    [Header("AI")]
    public bool isAI = true;
    public float aiTimeToUse = 10;
    public float aiTTUMin = 4;
    public float aiTTUMax = 10;
    public GameObject[] AI;
    public GameObject[] AIReference;
    [Header("Scale")]
    //public int[] normalScale = { 0 , 0 , 0}; Unsure if used.
    public Vector3 defaultScale;
    public Vector3 downScale;
    public Transform downScaleExample;
    [Header("Reference")]
    public int aiHasMoreCheckpoints;
    public int aicheckpointsMoreAt;
    [Header("Other")]
    public GameObject sceneManager;
    MoneyManager moneyManager;
    float itemCooldown;
    bool itemActive = false;

    // Start is called before the first frame update
    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        defaultScale = transform.localScale;
        downScale = downScaleExample.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        itemCooldown -= Time.deltaTime;
        if (isAI == false && itemCooldown <= 0 && itemActive == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RandomUpgrade();
            }
            if (banana == true && Input.GetKeyDown(KeyCode.Q))
            {
                //Test , basically
                itemCooldown = 1;
                sceneManager.GetComponent<IconManager>().UnEquipUI(1);
                EarnMoney();
                randomUpgrade = 0;
                banana = false;
            }
            if (banned == true && Input.GetKeyDown(KeyCode.Q))
            {
                itemCooldown = 1;
                //sets 2 random cars to 0 speed
                int r = Random.Range(0, AI.Length);
                GameObject i = AI[r];
                i.GetComponent<CarController>().speed = 0;
                Debug.Log("Killed " + i);
                r = Random.Range(0, AI.Length);
                i = AI[r];
                i.GetComponent<CarController>().speed = 0;
                Debug.Log("Killed " + i);
                sceneManager.GetComponent<IconManager>().UnEquipUI(2);
                EarnMoney();
                randomUpgrade = 0;
                banned = false;
            }
            if (scale == true && Input.GetKeyDown(KeyCode.Q))
            {
                itemCooldown = 11;
                //Scale down , Speed up
                transform.localScale = downScale;
                gameObject.GetComponent<CarController>().maxSpeed = gameObject.GetComponent<CarController>().startingMaxSpeed + 2;
                itemActive = true;
                Invoke("Scaleup", 10f);
                itemActive = false;
                sceneManager.GetComponent<IconManager>().UnEquipUI(3);
                EarnMoney();
                randomUpgrade = 0;
                scale = false;
            }
            if (jump == true && Input.GetKeyDown(KeyCode.Q))
            {
                itemCooldown = 1;
                //Its a jump.
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
                sceneManager.GetComponent<IconManager>().UnEquipUI(4);
                EarnMoney();
                randomUpgrade = 0;
                jump = false;
            }
            if (reference == true && Input.GetKeyDown(KeyCode.Q))
            {
                itemCooldown = 1;
                //Teleports you behind a random enemy car
                int i = Random.Range(0, AI.Length);
                transform.position = AIReference[i].transform.position;
                transform.rotation = AIReference[i].transform.rotation;
                if (AI[i].GetComponent<CarController>().checkPointCounter <= aicheckpointsMoreAt)
                {
                    gameObject.GetComponent<CarController>().checkPointCounter = AI[i].GetComponent<CarController>().checkPointCounter;
                }
                else if (AI[i].GetComponent<CarController>().checkPointCounter > aicheckpointsMoreAt)
                {
                    gameObject.GetComponent<CarController>().checkPointCounter = AI[i].GetComponent<CarController>().checkPointCounter - aiHasMoreCheckpoints;
                }
                if (gameObject.GetComponent<CarController>().lapCounter < AI[i].GetComponent<CarController>().lapCounter)
                {
                    gameObject.GetComponent<CarController>().lapCounter++;
                }
                gameObject.GetComponent<CarController>().speed = gameObject.GetComponent<CarController>().speed / 3;
                sceneManager.GetComponent<IconManager>().UnEquipUI(5);
                EarnMoney();
                randomUpgrade = 0;
                reference = false;
            }
        }
        else if (isAI == true && itemActive == false)
        {
            aiTimeToUse -= Time.deltaTime;
            if (aiTimeToUse <= 0)
            {
                if (banana == true)
                {
                    //Test , basically
                    Debug.Log("BANANA");
                    randomUpgrade = 0;
                    banana = false;
                }
                if (banned == true)
                {
                    //sets 2 random cars to 0 speed
                    int r = Random.Range(0, AI.Length);
                    GameObject i = AI[r];
                    i.GetComponent<CarController>().speed = 0;
                    Debug.Log("AI Killed " + i);
                    r = Random.Range(0, AI.Length);
                    i = AI[r];
                    i.GetComponent<CarController>().speed = 0;
                    Debug.Log(" AI Killed " + i);
                    randomUpgrade = 0;
                    banned = false;
                }
                if (scale == true)
                {
                    //Scale down , Speed up
                    transform.localScale = downScale;
                    gameObject.GetComponent<CarController>().maxSpeed = gameObject.GetComponent<CarController>().startingMaxSpeed + 2;
                    itemActive = true;
                    Invoke("Scaleup", 10f);
                    itemActive = false;
                    randomUpgrade = 0;
                    scale = false;
                }
                if (jump == true)
                {
                    //Its a jump.
                    Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                    rb.velocity = new Vector3(rb.velocity.x, 1, rb.velocity.z);
                    randomUpgrade = 0;
                    jump = false;
                }
            }
        }
    }
    public void RandomUpgrade()
    {
        if (randomUpgrade == 0)
        {
            randomUpgrade = Random.Range(1, 6);
            //randomUpgrade = 5;
            Debug.Log(randomUpgrade);
            switch (randomUpgrade)
            {
                case 1:
                    banana = true;
                    if (isAI == false)
                    {
                        sceneManager.GetComponent<IconManager>().EquipPowerup(1);
                    }
                    break;
                case 2:
                    banned = true;
                    if (isAI == false)
                    {
                        sceneManager.GetComponent<IconManager>().EquipPowerup(2);
                    }
                    break;
                case 3:
                    scale = true;
                    if (isAI == false)
                    {
                        sceneManager.GetComponent<IconManager>().EquipPowerup(3);
                    }
                    break;
                case 4:
                    jump = true;
                    if (isAI == false)
                    {
                        sceneManager.GetComponent<IconManager>().EquipPowerup(4);
                    }
                    break;
                case 5:
                    reference = true;
                    if (isAI == false)
                    {
                        sceneManager.GetComponent<IconManager>().EquipPowerup(5);
                    }
                    break;
            }
            if (isAI == true) 
            {
            aiTimeToUse = Random.Range(aiTTUMin, aiTTUMax);
            Debug.Log("AI Powerup");
            }
        }
    }
    void Scaleup()
    {
        transform.localScale = defaultScale;
        gameObject.GetComponent<CarController>().maxSpeed = gameObject.GetComponent<CarController>().startingMaxSpeed;
    }
    void EarnMoney()
    {
        moneyManager.GainMoney(5);
    }
}
