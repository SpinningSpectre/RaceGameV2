using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    public Text timerText;
    public float gameTime = 0;
    void Update()
    {
        gameTime = gameTime + Time.deltaTime;
        timerText.text = String.Format("{0:0.00}", gameTime);
    }
}
