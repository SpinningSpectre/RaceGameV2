using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    string moneyKey = "Money";
    string upgradeKey = "Upgrade";
    public int money { get; set; }
    public string upgrade { get; set; }
    MoneyManager moneyManager;
    private void Awake()
    {
        money = PlayerPrefs.GetInt(moneyKey);
        upgrade = PlayerPrefs.GetString(upgradeKey);
    }
    private void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.SetInt(moneyKey, 0);
            PlayerPrefs.SetString(upgradeKey, "");
            Debug.Log("Reset Shit");
        }
    }
    public void SetMoney(int money)
    {
        Debug.Log(money);
        moneyManager.money = money;
        PlayerPrefs.SetInt(moneyKey, money);
    }
    public void SetUpgrade(string Upgrade)
    {
        upgrade = Upgrade;
        PlayerPrefs.SetString(upgradeKey, Upgrade);
    }
}
