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
    public Mesh boxCar;
    public Material[] boxCarMats;
    public GameObject boxCarMats2;
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
                    Debug.Log("Yo");
                    playerCarVisual.GetComponent<MeshFilter>().mesh = boxCar;
                    playerCarVisual.GetComponent<MeshRenderer>().materials = boxCarMats;
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
    }
}
