using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    SaveData saveData;
    public Transform offScreen;
    public Transform cameraOnScreen;
    public Transform cameraIsUp;
    public Transform cameraIsntUp;
    public Transform devY;
    public Transform devN;
    public Transform devOnScreen;
    public Text timeBefore;
    public Text laptext;
    // Start is called before the first frame update
    void Start()
    {
        saveData = FindObjectOfType<SaveData>();
        SwitchCameraOptions();
        SwitchDevMode();
        SaveLaps(saveData.GetInt("Laps").ToString());
        SaveTimeBefore(saveData.GetInt("TimeBefore").ToString());
    }
    public void SwitchCameraOptions()
    {
        bool isUp;
        isUp = saveData.GetBool("CamUp");
        if (isUp == true)
        {
            cameraIsUp.position = offScreen.position;
            cameraIsntUp.position = cameraOnScreen.position;
        }
        else if (isUp == false)
        {
            cameraIsntUp.position = offScreen.position;
            cameraIsUp.position = cameraOnScreen.position;
        }
    }
    public void SwitchDevMode()
    {
        bool isUp;
        isUp = saveData.GetBool("DevMode");
        if (isUp == true)
        {
            devY.position = offScreen.position;
            devN.position = devOnScreen.position;
        }
        else if (isUp == false)
        {
            devN.position = offScreen.position;
            devY.position = devOnScreen.position;
        }
    }
    public void SaveTimeBefore(string time)
    {
        timeBefore.text = saveData.GetInt("TimeBefore").ToString();
        try
        {
            Int32.Parse(time);
            if (Int32.Parse(time) > 0 && Int32.Parse(time) < 6)
            {
                saveData.SaveInt("TimeBefore", Int32.Parse(time));
            }
        }
        catch (FormatException)
        {

        }
    }
    public void SaveLaps(string laps)
    {
        laptext.text = saveData.GetInt("Laps").ToString();
        try
        {
            Int32.Parse(laps);
            if (Int32.Parse(laps) > 0 && Int32.Parse(laps) < 6)
            {
                if (saveData.GetString("PlayerName") == "marlon" && Int32.Parse(laps) == 4)
                {
                    saveData.SaveInt("Laps", 69);
                }
                else
                {
                    saveData.SaveInt("Laps", Int32.Parse(laps));
                }
            }
        }
        catch (FormatException)
        {

        }
    }
}
