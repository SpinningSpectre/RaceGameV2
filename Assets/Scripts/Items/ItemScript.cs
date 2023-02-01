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
    public bool stun = false;
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
    [Header("Stun")]
    public GameObject projectile;
    public Transform firePoint;
    public Transform firePoint2;
    public Transform firePoint3;
    public int firespeed;
    [Header("Other")]
    public GameObject sceneManager;
    MoneyManager moneyManager;
    SaveData saveData;
    float itemCooldown;
    bool itemActive = false;
    int amountOfMoney = 5;
    bool upgraded = false;

    // Start is called before the first frame update
    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        saveData = FindObjectOfType<SaveData>();
        defaultScale = transform.localScale;
        downScale = downScaleExample.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        itemCooldown -= Time.deltaTime;
        if (isAI == false && itemCooldown <= 0 && itemActive == false)
        {
            if (saveData.GetBool("DevMode") == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    RandomUpgrade();
                }
            }
            if (banned == true && Input.GetKeyDown(KeyCode.Q))
            {
                itemCooldown = 1;
                //sets 2 random cars to 0 speed
                int r = Random.Range(0, AI.Length);
                GameObject i = AI[r];
                i.GetComponent<CarController>().speed = 2;
                r = Random.Range(0, AI.Length);
                i = AI[r];
                i.GetComponent<CarController>().speed = 2;
                if (upgraded == true)
                {
                    r = Random.Range(0, AI.Length);
                    i = AI[r];
                    i.GetComponent<CarController>().speed = 2;
                }
                sceneManager.GetComponent<IconManager>().UnEquipUI(2);
                EarnMoney();
                randomUpgrade = 0;
                banned = false;
            }
            if (scale == true && Input.GetKeyDown(KeyCode.Q))
            {
                itemCooldown = 11;
                if (upgraded == true)
                {
                    itemCooldown = 13;
                }
                //Scale down , Speed up
                transform.localScale = downScale;
                if (upgraded == true)
                {
                    gameObject.GetComponent<CarController>().maxSpeed = gameObject.GetComponent<CarController>().startingMaxSpeed + 3;
                    itemActive = true;
                    Invoke("Scaleup", 12f);
                }
                else
                {
                    gameObject.GetComponent<CarController>().maxSpeed = gameObject.GetComponent<CarController>().startingMaxSpeed + 2;
                    itemActive = true;
                    Invoke("Scaleup", 10f);
                }
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
                if (upgraded == true)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 7, rb.velocity.z);
                    gameObject.GetComponent<CarController>().maxSpeed = gameObject.GetComponent<CarController>().startingMaxSpeed + 7.5f;
                    gameObject.GetComponent<CarController>().speed = gameObject.GetComponent<CarController>().speed + 7.5f;
                }
                else
                {
                    rb.velocity = new Vector3(rb.velocity.x, 4, rb.velocity.z);
                    gameObject.GetComponent<CarController>().maxSpeed = gameObject.GetComponent<CarController>().startingMaxSpeed + 5;
                    gameObject.GetComponent<CarController>().speed = gameObject.GetComponent<CarController>().speed + 5;
                }
                Invoke("Scaleup", 1f);
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
                CarController playercar = gameObject.GetComponent<CarController>();
                CarController aicar = AI[i].GetComponent<CarController>();
                if (aicar.checkPointCounter <= aicheckpointsMoreAt)
                {
                    playercar.checkPointCounter = aicar.checkPointCounter;
                }
                else if (AI[i].GetComponent<CarController>().checkPointCounter > aicheckpointsMoreAt)
                {
                    playercar.checkPointCounter = aicar.checkPointCounter - aiHasMoreCheckpoints;
                }
                if (playercar.lapCounter < aicar.lapCounter)
                {
                    playercar.lapCounter++;
                }
                playercar.checkPointCounter = aicar.checkPointCounter - aiHasMoreCheckpoints;
                if (upgraded == true)
                {
                    playercar.speed = playercar.speed / 2;
                }
                else
                {
                    playercar.speed = playercar.speed / 3;
                }
                sceneManager.GetComponent<IconManager>().UnEquipUI(5);
                EarnMoney();
                randomUpgrade = 0;
                reference = false;
            }
            if (stun == true && Input.GetKeyDown(KeyCode.Q))
            {
                itemCooldown = 1;
                if (upgraded == true)
                {
                    firespeed = firespeed + 5;
                }
                GameObject Bullet2 = Instantiate(projectile, firePoint2.position, firePoint2.rotation);
                Bullet2.GetComponent<Rigidbody>().AddForce(-firePoint2.right * firespeed, ForceMode.Impulse);
                if (upgraded == true)
                {
                    GameObject Bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
                    Bullet.GetComponent<Rigidbody>().AddForce(-firePoint.right * firespeed, ForceMode.Impulse);
                    GameObject Bullet3 = Instantiate(projectile, firePoint3.position, firePoint3.rotation);
                    Bullet3.GetComponent<Rigidbody>().AddForce(-firePoint3.right * firespeed, ForceMode.Impulse);
                }
                sceneManager.GetComponent<IconManager>().UnEquipUI(6);
                EarnMoney();
                randomUpgrade = 0;
                stun = false;
            }
        }
        else if (isAI == true && itemActive == false && itemCooldown <= 0)
        {
            aiTimeToUse -= Time.deltaTime;
            if (aiTimeToUse <= 0)
            {
                if (banned == true)
                {
                    itemCooldown = 1;
                    //sets 2 random cars to 0 speed
                    int r = Random.Range(0, AI.Length);
                    GameObject i = AI[r];
                    i.GetComponent<CarController>().speed = 2;
                    r = Random.Range(0, AI.Length);
                    i = AI[r];
                    i.GetComponent<CarController>().speed = 2;
                    if (upgraded == true)
                    {
                        r = Random.Range(0, AI.Length);
                        i = AI[r];
                        i.GetComponent<CarController>().speed = 2;
                    }
                    randomUpgrade = 0;
                    banned = false;
                }
                if (scale == true)
                {
                    itemCooldown = 11;
                    if (upgraded == true)
                    {
                        itemCooldown = 13;
                    }
                    //Scale down , Speed up
                    transform.localScale = downScale;
                    if (upgraded == true)
                    {
                        gameObject.GetComponent<CarController>().maxSpeed = gameObject.GetComponent<CarController>().startingMaxSpeed + 3;
                        itemActive = true;
                        Invoke("Scaleup", 12f);
                    }
                    else
                    {
                        gameObject.GetComponent<CarController>().maxSpeed = gameObject.GetComponent<CarController>().startingMaxSpeed + 2;
                        itemActive = true;
                        Invoke("Scaleup", 10f);
                    }
                    itemActive = false;
                    randomUpgrade = 0;
                    scale = false;
                }
                if (jump == true)
                {
                    itemCooldown = 1;
                    //Its a jump.
                    Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                    if (upgraded == true)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, 7, rb.velocity.z);
                        gameObject.GetComponent<CarController>().maxSpeed = gameObject.GetComponent<CarController>().startingMaxSpeed + 7.5f;
                        gameObject.GetComponent<CarController>().speed = gameObject.GetComponent<CarController>().speed + 7.5f;
                    }
                    else
                    {
                        rb.velocity = new Vector3(rb.velocity.x, 4, rb.velocity.z);
                        gameObject.GetComponent<CarController>().maxSpeed = gameObject.GetComponent<CarController>().startingMaxSpeed + 5;
                        gameObject.GetComponent<CarController>().speed = gameObject.GetComponent<CarController>().speed + 5;
                    }
                    Invoke("Scaleup", 1f);
                    randomUpgrade = 0;
                    jump = false;
                }
                if (reference == true)
                {
                    itemCooldown = 1;
                    //Teleports you behind a random enemy car
                    int i = Random.Range(0, AI.Length);
                    transform.position = AIReference[i].transform.position;
                    transform.rotation = AIReference[i].transform.rotation;
                    CarController playercar = gameObject.GetComponent<CarController>();
                    CarController aicar = AI[i].GetComponent<CarController>();
                    if (aicar.checkPointCounter <= aicheckpointsMoreAt)
                    {
                        playercar.checkPointCounter = aicar.checkPointCounter;
                    }
                    else if (AI[i].GetComponent<CarController>().checkPointCounter > aicheckpointsMoreAt)
                    {
                        playercar.checkPointCounter = aicar.checkPointCounter - aiHasMoreCheckpoints;
                    }
                    if (playercar.lapCounter < aicar.lapCounter)
                    {
                        playercar.lapCounter++;
                    }
                    playercar.checkPointCounter = aicar.checkPointCounter - aiHasMoreCheckpoints;
                    if (upgraded == true)
                    {
                        playercar.speed = playercar.speed / 2;
                    }
                    else
                    {
                        playercar.speed = playercar.speed / 3;
                    }
                    randomUpgrade = 0;
                    reference = false;
                }
                if (stun == true)
                {
                    itemCooldown = 1;
                    if (upgraded == true)
                    {
                        firespeed = firespeed + 5;
                    }
                    GameObject Bullet2 = Instantiate(projectile, firePoint2.position, firePoint2.rotation);
                    Bullet2.GetComponent<Rigidbody>().AddForce(-firePoint2.right * firespeed, ForceMode.Impulse);
                    if (upgraded == true)
                    {
                        GameObject Bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
                        Bullet.GetComponent<Rigidbody>().AddForce(-firePoint.right * firespeed, ForceMode.Impulse);
                        GameObject Bullet3 = Instantiate(projectile, firePoint3.position, firePoint3.rotation);
                        Bullet3.GetComponent<Rigidbody>().AddForce(-firePoint3.right * firespeed, ForceMode.Impulse);
                    }
                    randomUpgrade = 0;
                    stun = false;
                }
            }
        }
    }
    public void RandomUpgrade()
    {
        if (randomUpgrade == 0)
        {
            randomUpgrade = Random.Range(2, 7);
            //randomUpgrade = 5;
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
                case 6:
                    stun = true;
                    if (isAI == false)
                    {
                        sceneManager.GetComponent<IconManager>().EquipPowerup(6);
                    }
                    break;
            }
            if (isAI == true) 
            {
            aiTimeToUse = Random.Range(aiTTUMin, aiTTUMax);
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
        moneyManager.GainMoney(amountOfMoney);
    }
    public void UpgradedUpgrades()
    {
        amountOfMoney = 7;
        upgraded = true;
    }
}
