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
    [Header("AI")]
    public bool isAI = true;
    public float aiTimeToUse = 10;
    public float aiTTUMin = 4;
    public float aiTTUMax = 10;
    public GameObject[] AI;
    [Header("Scale")]
    //public int[] normalScale = { 0 , 0 , 0}; Unsure if used.
    public Vector3 defaultScale;
    public Vector3 downScale;
    public Transform downScaleExample;
    [Header("Other")]
    public GameObject sceneManager;
    MoneyManager moneyManager;

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
        if (isAI == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RandomUpgrade();
            }
            if (banana == true && Input.GetKeyDown(KeyCode.Q))
            {
                //Test , basically
                sceneManager.GetComponent<IconManager>().UnEquipUI(1);
                EarnMoney();
                randomUpgrade = 0;
                banana = false;
            }
            if (banned == true && Input.GetKeyDown(KeyCode.Q))
            {
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
                //Scale down , Speed up
                transform.localScale = downScale;
                gameObject.GetComponent<CarController>().maxSpeed = 12;
                Invoke("Scaleup", 10f);
                sceneManager.GetComponent<IconManager>().UnEquipUI(3);
                EarnMoney();
                randomUpgrade = 0;
                scale = false;
            }
            if (jump == true && Input.GetKeyDown(KeyCode.Q))
            {
                //Its a jump.
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
                sceneManager.GetComponent<IconManager>().UnEquipUI(4);
                EarnMoney();
                randomUpgrade = 0;
                jump = false;
            }
        }
        else
        {
            aiTimeToUse -= Time.deltaTime;
            if (aiTimeToUse <= 0)
            {
                Debug.Log("AI Attack");
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
                    Invoke("Scaleup", 10f);
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
            randomUpgrade = Random.Range(1, 5);
            //randomUpgrade = 2;
            Debug.Log(randomUpgrade);
            if (randomUpgrade == 1)
            {
                banana = true;
                if (isAI == false)
                {
                    sceneManager.GetComponent<IconManager>().EquipPowerup(1);
                }
            }
            else if (randomUpgrade == 2)
            {
                banned = true;
                if (isAI == false)
                {
                    sceneManager.GetComponent<IconManager>().EquipPowerup(2);
                }
            }
            else if (randomUpgrade == 3)
            {
                scale = true;
                if (isAI == false)
                {
                    sceneManager.GetComponent<IconManager>().EquipPowerup(3);
                }
            }
            else if (randomUpgrade == 4)
            {
                jump = true;
                if (isAI == false)
                {
                    sceneManager.GetComponent<IconManager>().EquipPowerup(4);
                }
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
        gameObject.GetComponent<CarController>().maxSpeed = 10 + gameObject.GetComponent<CarController>().rngSpeedmax; ;
    }
    void EarnMoney()
    {
        moneyManager.GainMoney(5);
    }
}
