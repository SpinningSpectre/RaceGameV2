using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Money and Upgrades
public class MoneyManager : MonoBehaviour
{
    public float money;
    SaveData saveData;
    public Text moneyText;
    public float moneyK;
    public float moneyM;
    public float moneyMultiplier = 1;
    public bool showsMoney = true;
    private void Start()
    {
        saveData = FindObjectOfType<SaveData>();
        money = saveData.money;
        moneyMultiplier = saveData.GetFloat("MMult");
    }
    private void Update()
    {
        if (saveData.GetBool("DevMode") == true)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                GainMoney(100);
            }
            if (Input.GetKey(KeyCode.N))
            {
                GainMoney(100);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                GainMoney(-1);
            }
        }
        if (showsMoney == true)
        {
            if (money < 1000 && money >= 0)
            {
                moneyText.text = String.Format("{0:0}", money);
            }
            else if (money >= 1000 && money < 1000000)
            {
                moneyK = money / 1000;
                moneyText.text = String.Format("{0:0.0}", moneyK) + "K";
            }
            else if (money < 0)
            {
                moneyText.text = money.ToString() + " MSG Me!";
            }
            else if (money == 1000000)
            {
                moneyK = money / 1000000;
                moneyText.text = "1M";
            }
            else
            {
                moneyText.text = "I cant find out :(";
            }
            if (money > 1000000)
            {
                money = 1000000;
                moneyText.text = "1M";
            }
        }
        saveData.SaveFloat(moneyMultiplier, "MMult");
    }
    public void GainMoney(float amount)
    {
        Debug.Log(money);
        if (amount == 0)
        {
            saveData.SetMoney(money);
        }
        else if (amount == -1)
        {
            saveData.SetMoney(0);
        }
        else
        {
            if (amount > 0)
            {
                amount = amount * moneyMultiplier;
                saveData.SetTotalMoney(amount);
            }
            money = money + amount;
            saveData.SetMoney(money);
        }
    }
}
