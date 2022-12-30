using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour
{
    public int upgradeCost;
    MoneyManager moneyManager;
    SaveData saveData;
    ShopManager shopManager;
    public bool hasThisUpgrade = false;
    public RawImage selectedImage;
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
    public void BuySomething(string upgrade)
    {
        if (hasThisUpgrade == false)
        {
            if (moneyManager.money < upgradeCost)
            {
                Debug.Log("HA , Poor!");
            }
            else
            {
                Debug.Log("Bought " + upgrade);
                hasThisUpgrade = true;
                UpgradeUI();
                saveData.SetUpgrade(upgrade);
                moneyManager.GainMoney(-upgradeCost);
            }
        }
    }
    public void UpgradeUI()
    {
        shopManager.UnEquipUI();
        selectedImage.color = Color.white;
    }
}
