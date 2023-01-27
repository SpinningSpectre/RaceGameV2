using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    SaveData savedata;
    public Text money;
    public Text totalMoney;
    public Text mMultiplierText;
    public Text winsText;
    public Text bestRound;
    public Text lastRound;
    public Text bestLap;
    public Text raceTime;
    public Text playerNameText;
    public float wins;
    public float currentMoney;
    public float totalMoneyNumber;
    public float moneyMultiplier;
    public float lastRoundTime;
    public float bestRoundTime;
    public float bestLapTime;
    public float totalPlayTime;
    public string playerName;
    public float moneyK;
    public float moneyM;
    // Start is called before the first frame update
    void Start()
    {
        savedata = FindObjectOfType<SaveData>();
        stats();
    }
    void stats()
    {
        CurrentMoney();
        TotalMoney();
        MoneyMultiplier();
        Wins();
        LastRound();
        BestRound();
        RaceTime();
        PlayerName();
    }
    void CurrentMoney()
    {
        currentMoney = savedata.GetFloat("Money");
        if (currentMoney < 1000 && currentMoney >= 0)
        {
            money.text = String.Format("{0:0}", currentMoney);
        }
        else if (currentMoney >= 1000 && currentMoney < 1000000)
        {
            moneyK = currentMoney / 1000;
            money.text = String.Format("{0:0.0}", moneyK) + "K";
        }
        else if (currentMoney < 0)
        {
            money.text = money.ToString() + " MSG Me!";
        }
        else if (currentMoney == 1000000)
        {
            moneyK = currentMoney / 1000000;
            money.text = "1M";
        }
        else
        {
            money.text = "I cant find out :(";
        }
        if (currentMoney > 1000000)
        {
            currentMoney = 1000000;
            money.text = "1M";
        }
    }
    void TotalMoney()
    {
        totalMoneyNumber = savedata.GetFloat("TotalMoney");
        if (totalMoneyNumber < 1000 && totalMoneyNumber >= 0)
        {
            totalMoney.text = String.Format("{0:0}", totalMoneyNumber);
        }
        else if (totalMoneyNumber >= 1000 && totalMoneyNumber < 1000000)
        {
            moneyK = totalMoneyNumber / 1000;
            totalMoney.text = String.Format("{0:0.0}", moneyK) + "K";
        }
        else if (totalMoneyNumber < 0)
        {
            totalMoney.text = money.ToString() + " MSG Me!";
        }
        else if (totalMoneyNumber >= 1000000 && totalMoneyNumber < 100000000)
        {
            moneyM = totalMoneyNumber / 1000000;
            totalMoney.text = String.Format("{0:0.0}", moneyM) + "M";
        } else if(totalMoneyNumber == 100000000)
        {
            totalMoney.text = "100M";
        }else if(totalMoneyNumber > 100000000)
        {
            totalMoney.text = "100M+";
        }
        else
        {
            totalMoney.text = "I cant find out :(";
        }
        if (totalMoneyNumber > 1000000000)
        {
            totalMoneyNumber = 1000000000;
        }
    }
    void MoneyMultiplier()
    {
        moneyMultiplier = savedata.GetFloat("MMult");
        mMultiplierText.text = "x" + moneyMultiplier.ToString();
    }
    void Wins()
    {
        wins = savedata.GetInt("Wins");
        winsText.text = wins.ToString();
    }
    void LastRound()
    {
        lastRoundTime = savedata.GetFloat("RaceTime");
        lastRound.text = String.Format("{0:0.00}", lastRoundTime) + "s";
    }
    void BestRound()
    {
        bestRoundTime = savedata.GetFloat("BestRoundTime");
        bestRound.text = String.Format("{0:0.00}", bestRoundTime) + "s";
    }
    void RaceTime()
    {
        totalPlayTime = savedata.GetFloat("TotalRaceTime");
        raceTime.text = String.Format("{0:0.00}", totalPlayTime) + "s";//totalPlayTime.ToString();
    }
    void PlayerName()
    {
        playerName = savedata.GetString("PlayerName");
        playerNameText.text = playerName.ToString();
    }
}
