using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public RawImage[] Upgrades;
    public UpgradeShop[] whatDoIHave;
    SaveData saveData;
    // Start is called before the first frame update
    void Start()
    {
        saveData = FindObjectOfType<SaveData>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (saveData.upgrade)
        {
            case "":
                whatDoIHave[0].UpgradeUI();
                whatDoIHave[0].hasThisUpgrade = true;
                break;
            case "Powerup":
                whatDoIHave[7].UpgradeUI();
                whatDoIHave[7].hasThisUpgrade = true;
                break;
            case "Speed":
                whatDoIHave[1].UpgradeUI();
                whatDoIHave[1].hasThisUpgrade = true;
                break;
            case "Accel":
                whatDoIHave[2].UpgradeUI();
                whatDoIHave[2].hasThisUpgrade = true;
                break;
            case "Turn":
                whatDoIHave[3].UpgradeUI();
                whatDoIHave[3].hasThisUpgrade = true;
                break;
            case "Stop":
                whatDoIHave[4].UpgradeUI();
                whatDoIHave[4].hasThisUpgrade = true;
                break;
            case "Balance":
                whatDoIHave[5].UpgradeUI();
                whatDoIHave[5].hasThisUpgrade = true;
                break;
            case "Money":
                whatDoIHave[6].UpgradeUI();
                whatDoIHave[6].hasThisUpgrade = true;
                break;
        }
        for (int i = 0; i < saveData.hasUpgrade.Length;)
        {
            int a = i + 1;
            if (saveData.hasUpgrade[i] == true)
            {
                whatDoIHave[a].OwnUpgrade();
            }
            i++;
        }
    }
    public void UnEquipUI()
    {
        for (int i = 0; i < Upgrades.Length; i++)
        {
            Upgrades[i].color = Color.clear;
            whatDoIHave[i].hasThisUpgrade = false;
        }
    }
}
