using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenUI : MonoBehaviour
{
    SaveData saveData;
    public float raceTime = 0;
    public Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        saveData = FindObjectOfType<SaveData>();
        raceTime = saveData.GetFloat("RaceTime");
        timeText.text = String.Format("{0:0.00}", raceTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
