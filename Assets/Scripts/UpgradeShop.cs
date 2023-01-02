using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour
{
    public int upgradeCost;
    MoneyManager moneyManager;
    SaveData saveData;
    ShopManager shopManager;
    public bool hasThisUpgrade = false;
    public int buyingUpgrade;
    public RawImage selectedImage;
    public GameObject costSign;
    public Image checkMark;
    // Start is called before the first frame update
    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        saveData = FindObjectOfType<SaveData>();
        shopManager = FindObjectOfType<ShopManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Cost(int cost)
    {
        upgradeCost = cost;
    }
    public void UpgradeNumber(int upgrade)
    {
        buyingUpgrade = upgrade;
    }
    public void BuySomething(string upgrade)
    {
        if (hasThisUpgrade == false)
        {
            bool hasUpgrade = false;
            hasUpgrade = saveData.CheckUpgrade(buyingUpgrade);
            if (hasUpgrade == true)
            {
                saveData.SetUpgrade(buyingUpgrade);
                hasThisUpgrade = true;
                UpgradeUI();
                saveData.SetUpgrade(upgrade);
                if (hasUpgrade == false)
                {
                    moneyManager.GainMoney(-upgradeCost);
                    OwnUpgrade();
                }
            }
            else if(moneyManager.money < upgradeCost)
            {
                Debug.Log("HA , Poor!");
            } else
            {
                saveData.SetUpgrade(buyingUpgrade);
                hasThisUpgrade = true;
                UpgradeUI();
                saveData.SetUpgrade(upgrade);
                if (hasUpgrade == false)
                {
                    moneyManager.GainMoney(-upgradeCost);
                    OwnUpgrade();
                }
            }
        } else if (hasThisUpgrade == false)
        {
            hasThisUpgrade = true;
            UpgradeUI();
            saveData.SetUpgrade(upgrade);
        }
    }
    public void UpgradeUI()
    {
        shopManager.UnEquipUI();
        selectedImage.color = Color.white;
    }
    public void OwnUpgrade()
    {
        Image[] image;
        image = costSign.GetComponentsInChildren<Image>();
        for (int i = 0; i < image.Length; i++)
        {
            image[i].color = Color.clear;
        }
        costSign.GetComponentInChildren<Text>().text = "";
        checkMark.color = Color.white;
    }
}
