using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    public Text timerText;
    public Image timerImage;
    public Text startTimer;
    public Image startTimerImage;
    public float gameTime = 0;
    public float lastRaceTime;
    public float beforeStart = 3;
    public int startSize = 60;
    public int bigSize = 90;
    float sizeTime;
    public bool timerActive = true;
    bool[] doneTime = { false, false, false , false , false};
    SaveData savedata;
    private void Start()
    {
        savedata = FindObjectOfType<SaveData>();
        lastRaceTime = savedata.GetFloat("RaceTime");
        beforeStart = savedata.GetInt("TimeBefore");
    }
    void Update()
    {
        beforeStart -= Time.deltaTime;
        if (beforeStart > -1.1f && timerActive == true)
        {
            sizeTime -= Time.deltaTime;
            if (beforeStart > 4 && beforeStart < 5 && doneTime[4] == false)
            {
                startTimer.color = Color.black;
                startTimer.text = "5";
                startTimer.fontSize = startSize;
                doneTime[4] = true;
            }
            if (beforeStart > 3 && beforeStart < 4 && doneTime[3] == false)
            {
                startTimer.color = Color.magenta;
                startTimer.text = "4";
                startTimer.fontSize = startSize;
                doneTime[3] = true;
            }
            if (beforeStart > 2 && beforeStart < 3 && doneTime[2] == false)
            {
                startTimer.color = Color.red;
                startTimer.text = "3";
                startTimer.fontSize = startSize;
                doneTime[2] = true;
            }
            if (beforeStart > 1 && beforeStart < 2&&doneTime[1] == false)
            {
                startTimer.color = Color.yellow;
                startTimer.text = "2";
                startTimer.fontSize = startSize;
                doneTime[1] = true;
            }
            if (beforeStart > 0 && beforeStart < 1&& doneTime[0] == false)
            {
                startTimer.color = Color.green;
                startTimer.text = "1";
                startTimer.fontSize = startSize;
                doneTime[0] = true;
            }
            if (beforeStart < 0 &&beforeStart > -0.75f)
            {
                startTimer.color = Color.white;
                startTimerImage.color = Color.clear;
                startTimer.text = "GO";
                startTimer.fontSize = bigSize;
                timerImage.color = Color.white;
                doneTime[0] = true;
            }
            if (beforeStart < -0.75f)
            {
                if(startTimer.fontSize < -10)
                {
                    startTimer.fontSize = -10;
                    startTimer.text = "";
                }
                else
                {
                    if (sizeTime < 0)
                    {
                        startTimer.fontSize = startTimer.fontSize - 10;
                        sizeTime = 0.01f;
                    }
                }
            }
            if (sizeTime < 0)
            {
                startTimer.fontSize = startTimer.fontSize - 1;
                sizeTime = 0.02f;
            }
        }
        if (timerActive == true && beforeStart < 0)
        {
            gameTime += Time.deltaTime;
            timerText.text = String.Format("{0:0.00}", gameTime);
        }
    }
    public void SaveTime()
    {
        savedata.SaveFloat(gameTime, "RaceTime");
        savedata.AddRaceTime(gameTime);
        savedata.CheckBestTime(gameTime);
    }
}
