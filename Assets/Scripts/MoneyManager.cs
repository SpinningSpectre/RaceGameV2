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
        if (money < 1000 && money >= 0)
        {
            moneyText.text = money.ToString();
        } else if (money >= 1000 && money < 1000000)
        {
            moneyK = money / 1000;
            moneyText.text = moneyK.ToString() + "K";
        } else if (money < 0)
        {
            moneyText.text = money.ToString() + " MSG Me!";
        } else if (money == 1000000)
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
        }
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
            Debug.Log("is 0");
            saveData.SetMoney(0);
        }
        else
        {
            if (amount > 0)
            {
                amount = amount * moneyMultiplier;
            }
            money = money + amount;
            saveData.SetMoney(money);
        }
    }
}
