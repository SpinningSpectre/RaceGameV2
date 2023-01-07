using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsUI : MonoBehaviour
{
    [Header("Basics")]
    SaveData saveData;
    [Header("Images")]
    public Image[] Achievement;
    // Start is called before the first frame update
    void Start()
    {
        saveData = FindObjectOfType<SaveData>();
        bool hasAchievement;
        hasAchievement = saveData.CheckAchievement(1);
        if(hasAchievement == false)
        {
            Achievement[0].color = Color.red;
        }
        else
        {
            Achievement[0].color = Color.green;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
