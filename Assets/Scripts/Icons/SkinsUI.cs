using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinsUI : MonoBehaviour
{
    [Header("Basics")]
    SaveData saveData;
    [Header("Images")]
    public Sprite unlock;
    public Image[] skins;
    public Image[] locks;
    public Text oldText;
    // Start is called before the first frame update
    void Start()
    {
        saveData = FindObjectOfType<SaveData>();
        bool hasAchievement;
        hasAchievement = saveData.CheckAchievement(1);
        if (hasAchievement == false)
        {
            skins[0].color = Color.gray;
        }
        else
        {
            locks[0].sprite = unlock;
        }
        hasAchievement = saveData.CheckAchievement(2);
        if (hasAchievement == false)
        {
            skins[1].color = Color.gray;
        }
        else
        {
            locks[1].sprite = unlock;
        }
        hasAchievement = saveData.hasOld();
        if (hasAchievement == false)
        {
            skins[2].color = Color.clear;
            oldText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
