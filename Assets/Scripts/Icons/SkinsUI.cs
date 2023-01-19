using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinsUI : MonoBehaviour
{
    [Header("Basics")]
    SaveData saveData;
    [Header("Images")]
    public Image[] skins;
    // Start is called before the first frame update
    void Start()
    {
        saveData = FindObjectOfType<SaveData>();
        bool hasAchievement;
        hasAchievement = saveData.CheckAchievement(1);
        if (hasAchievement == false)
        {
            skins[0].color = Color.red;
        }
        else
        {
            skins[0].color = Color.green;
        }
        hasAchievement = saveData.CheckAchievement(2);
        if (hasAchievement == false)
        {
            skins[1].color = Color.red;
        }
        else
        {
            skins[1].color = Color.green;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
