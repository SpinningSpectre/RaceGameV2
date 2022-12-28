using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    public int upgradeCost;
    MoneyManager moneyManager;
    SaveData saveData;
    // Start is called before the first frame update
    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        saveData = FindObjectOfType<SaveData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Cost(int cost)
    {
        upgradeCost = cost;
    }
    public void BuySomething(string upgrade)
    {
        if (moneyManager.money < upgradeCost)
        {
            Debug.Log("HA , Poor!");
        }
        else
        {
            Debug.Log("Bought " + upgrade);
            saveData.SetUpgrade(upgrade);
            moneyManager.money -= upgradeCost;
        }
    }
}
