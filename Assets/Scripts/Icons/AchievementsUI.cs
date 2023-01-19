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
    public Sprite unLock;
    public Image[] locks;
    // Start is called before the first frame update
    void Start()
    {
        saveData = FindObjectOfType<SaveData>();
        bool hasAchievement;
        hasAchievement = saveData.CheckAchievement(1);
        if(hasAchievement == true)
        {
            locks[0].sprite = unLock;
        }
        else
        {
            Achievement[0].color = Color.gray;
        }
        hasAchievement = saveData.CheckAchievement(2);
        if (hasAchievement == true)
        {
            locks[1].sprite = unLock;
        }
        else
        {
            Achievement[1].color = Color.gray;
        }
        hasAchievement = saveData.CheckAchievement(3);
        if (hasAchievement == true)
        {
            locks[2].sprite = unLock;
        }
        else
        {
            Achievement[2].color = Color.gray;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
