using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class SaveData : MonoBehaviour
{
    string moneyKey = "Money";
    string upgradeKey = "Upgrade";
    string carSkinKey = "CarSkin";
    public bool[] hasUpgrade = {false , false , false , false , false , false , false};
    public bool[] hasAchievement = { false, false };
    public float money { get; set; }
    public float totalMoney { get; set; }
    public int wins { get; set; }
    public string upgrade { get; set; }
    public string carSkin { get; set; }
    public bool bool1 = false;
    MoneyManager moneyManager;
    public bool freeDefault = false;
    private void Awake()
    {
        money = PlayerPrefs.GetFloat(moneyKey);
        totalMoney = PlayerPrefs.GetFloat("TotalMoney");
        wins = PlayerPrefs.GetInt("Wins");
        upgrade = PlayerPrefs.GetString(upgradeKey);
        carSkin = PlayerPrefs.GetString(carSkinKey);
        bool1 = PlayerPrefs.GetInt("1Up") == 0;
    }
    private void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        //for (int i)
        if (PlayerPrefs.GetInt("1Up") == 1)
        {
            hasUpgrade[0] = true;
        }
        if (PlayerPrefs.GetInt("2Up") == 1)
        {
            hasUpgrade[1] = true;
        }
        if (PlayerPrefs.GetInt("3Up") == 1)
        {
            hasUpgrade[2] = true;
        }
        if (PlayerPrefs.GetInt("4Up") == 1)
        {
            hasUpgrade[3] = true;
        }
        if (PlayerPrefs.GetInt("5Up") == 1)
        {
            hasUpgrade[4] = true;
        }
        if (PlayerPrefs.GetInt("6Up") == 1)
        {
            hasUpgrade[5] = true;
        }
        if (PlayerPrefs.GetInt("7Up") == 1)
        {
            hasUpgrade[6] = true;
        }
        CheckUpgradeAchievement();
        if (freeDefault == true)
        {
            PlayerPrefs.SetInt("hasOld", true ? 1 : 0);
        }
    }
    private void Update()
    {
        if (GetBool("DevMode") == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerPrefs.DeleteAll();
                SetPlayerName("Enter Player Name");
                SaveFloat(1, "MMult");
                SaveFloat(9999999, "BestRoundTime");
                SaveInt("TimeBefore", 3);
                SaveInt("Laps", 3);
                Debug.Log("Reset Shit");
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                if (PlayerPrefs.GetInt("1Up") == 0)
                {
                    //false
                    Debug.Log("is 0");
                }
                else if (PlayerPrefs.GetInt("1Up") == 1)
                {
                    //true
                    Debug.Log("is 1");
                }
                PlayerPrefs.SetInt("1Up", true ? 1 : 0);
            }
        }
    }
    public void SetMoney(float money)
    {
        moneyManager.money = money;
        PlayerPrefs.SetFloat(moneyKey, money);
    }
    public void SetTotalMoney(float money)
    {
        totalMoney = PlayerPrefs.GetFloat("TotalMoney");
        PlayerPrefs.SetFloat("TotalMoney", totalMoney + money);
        if (PlayerPrefs.GetFloat("TotalMoney") >= 3000)
        {
            AchieveSomething(3);
        }
        Debug.Log(PlayerPrefs.GetFloat("TotalMoney") + " Money Total");
    }
    public void SetUpgrade(string Upgrade)
    {
        upgrade = Upgrade;
        PlayerPrefs.SetString(upgradeKey, Upgrade);
        CheckUpgradeAchievement();
    }
    public bool CheckUpgrade(int upgrade)
    {
        bool hasUpgrade = false;
        if (PlayerPrefs.GetInt(upgrade + "Up") == 1)
        {
            hasUpgrade = true;
        }
        else if(PlayerPrefs.GetInt(upgrade + "Up") == 0)
        {
            hasUpgrade = false;
        }
        return hasUpgrade;
    }
    public void SetUpgrade(int upgrade)
    {
        if (upgrade != 0)
        {
            hasUpgrade[upgrade - 1] = true;
            PlayerPrefs.SetInt(upgrade + "Up", true ? 1 : 0);
        }
    }
    public void AchieveSomething(int achievement)
    {
        PlayerPrefs.SetInt(achievement + "Ach", true ? 1 : 0);
    }
    public bool CheckAchievement(int achievement)
    {
        bool hasAchievement = false;
        if (PlayerPrefs.GetInt(achievement + "Ach") == 1)
        {
            hasAchievement = true;
        }
        else if (PlayerPrefs.GetInt(achievement + "Ach") == 0)
        {
            hasAchievement = false;
        }
        return hasAchievement;
    }
    public void CheckUpgradeAchievement()
    {
        if (hasUpgrade[0] == true && hasUpgrade[1] == true && hasUpgrade[2] == true && hasUpgrade[3] == true && hasUpgrade[4] == true && hasUpgrade[5] == true && hasUpgrade[6] == true)
        {
            AchieveSomething(1);
        }
    }
    public void SetCarSkin(string skin)
    {
        PlayerPrefs.SetString(carSkinKey, skin);
    }
    public void Wins(int amount)
    {
        PlayerPrefs.SetInt("Wins", PlayerPrefs.GetInt("Wins") + amount);
        if (PlayerPrefs.GetInt("Wins") == 25)
        {
            AchieveSomething(2);
        }
        Debug.Log(PlayerPrefs.GetInt("Wins") + "Wins");
    }
    public void SaveFloat(float number , string name)
    {
        PlayerPrefs.SetFloat(name, number);
    }
    public void SaveString(string word, string name)
    {
        PlayerPrefs.SetString(name, word);
    }
    public float GetFloat(string name)
    {
        return PlayerPrefs.GetFloat(name);
    }
    public int GetInt(string name)
    {
        return PlayerPrefs.GetInt(name);
    }
    public string GetString(string name)
    {
        return PlayerPrefs.GetString(name);
    }
    public void AddRaceTime(float time)
    {
        PlayerPrefs.SetFloat("TotalRaceTime", time + PlayerPrefs.GetFloat("TotalRaceTime"));
    }
    public void CheckBestTime(float time)
    {
        if (time < PlayerPrefs.GetFloat("BestRoundTime"))
        {
            PlayerPrefs.SetFloat("BestRoundTime" , time);
        }
    }
    public void SetPlayerName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
    }
    public bool hasOld()
    {
        bool bool2 = false;
        if (PlayerPrefs.GetInt("hasOld") == 1)
        {
            bool2 = true;
        }
        return bool2;
    }
    public bool GetBool(string name)
    {
        bool bool2 = false;
        if (PlayerPrefs.GetInt(name) == 1)
        {
            bool2 = true;
        }
        return bool2;
    }
    public void SetBoolTrue(string name)
    {
        PlayerPrefs.SetInt(name, true ? 1 : 0);
    }
    public void SetBoolFalse(string name)
    {
        PlayerPrefs.SetInt(name, false ? 1 : 0);
    }
    public void SaveInt(string name , int amount)
    {
        PlayerPrefs.SetInt(name, amount);
    }
}
