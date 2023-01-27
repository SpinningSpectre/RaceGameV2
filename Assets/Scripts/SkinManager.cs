using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    [Header("Basic variables")]
    public string currentLocation;
    SaveData saveData;
    public GameObject playerCar;
    public GameObject playerCarVisual;
    public Image playerPfp;
    public Image[] shopImages;
    public Image coin;
    [Header("Car Skins")]
    public string currentCarSkin;
    public Material[] boxCarMats;
    public GameObject boxCar;
    public GameObject forklift;
    public GameObject oldCar;
    [Header("Player Pictures")]
    public string currentPFP;
    public Image blenderMan;
    [Header("Shop skins")]
    public string currentShopSkin;
    public Image metalShop;
    [Header("Coin skins")]
    public string currentCoinSkin;
    public Image goldenCoin;
    // Start is called before the first frame update
    void Start()
    {
        saveData = FindObjectOfType<SaveData>();
        if (currentLocation == "map")
        {
            currentCarSkin = saveData.carSkin;
            switch (currentCarSkin)
            {
                case "box":
                    playerCarVisual.GetComponent<MeshRenderer>().enabled = false;
                    boxCar.GetComponent<MeshRenderer>().enabled = true;
                    break;
                case "forklift":
                    playerCarVisual.GetComponent<MeshRenderer>().enabled = false;
                    forklift.GetComponent<MeshRenderer>().enabled = true;
                    break;
                case "oldCar":
                    playerCarVisual.GetComponent<MeshRenderer>().enabled = false;
                    oldCar.GetComponent<MeshRenderer>().enabled = true;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void equipCarSkin(string skin)
    {
        Debug.Log(skin);
        if (skin == "default")
        {
            saveData.SetCarSkin("");
        }
        if (skin == "box")
        {
            bool hasAchievement = false;
            hasAchievement = saveData.CheckAchievement(1);
            if (hasAchievement == true)
            {
                    saveData.SetCarSkin(skin);
            }
        }
        if (skin == "forklift")
        {
            bool hasAchievement = false;
            hasAchievement = saveData.CheckAchievement(2);
            if (hasAchievement == true)
            {
                saveData.SetCarSkin(skin);
            }
        }
        if (skin == "oldCar")
        {
            bool hasAchievement = false;
            hasAchievement = saveData.hasOld();
            if (hasAchievement == true)
            {
                saveData.SetCarSkin(skin);
            }
        }
    }
}
