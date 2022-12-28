using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Money and Upgrades
public class MoneyManager : MonoBehaviour
{
    public int money;
    SaveData saveData;
    private void Start()
    {
        saveData = FindObjectOfType<SaveData>();
        money = saveData.money;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GainMoney(100);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GainMoney(0);
            Debug.Log("Reset Shit");
        }
    }
    public void GainMoney(int amount)
    {
        Debug.Log(money);
        if (amount == 0)
        {
            Debug.Log("is 0");
            saveData.SetMoney(0);
        }
        else
        {
            money = money + amount;
            saveData.SetMoney(money);
        }
    }
}
