using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class IconManager : MonoBehaviour
{
    public int[] rankingNumber;
    public Text[] rankingText;
    public Image[] carImages;
    public Sprite[] carImageSprites;
    public GameObject[] cars;
    [SerializeField]
    CarController carController;
    [SerializeField]
    public List<GameObject> allCars;
    [SerializeField]
    List<Sprite> spriteRanking; 
    [Header("Transforms")]
    public RectTransform noItems;
    public RectTransform powerupLocation;
    [Header("Images")]
    public Image powerUp1;
    public Image powerUp2;
    public Image powerUp3;
    public Image powerUp4;
    public Image powerUp5;
    [Header("Bools")]
    bool itemSlot1Taken = false;
    private void Start()
    {
        UnEquipAllItems();
    }
    private void Awake()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            allCars.Add(cars[i]);
            spriteRanking.Add(carImageSprites[i]);
        }
    }
    void UnEquipAllItems()
    {
        UnEquipUI(0);
    }
    private void Update()
    {
        allCars = allCars.ToList().OrderByDescending(x => x.GetComponent<CarController>().counterUI).ToList();
        for (int i = 0; i < allCars.Count; i++)
        {
            rankingText[i].text = " " + rankingNumber[i] + ". " + allCars[i].name;
            carImages[i].sprite = allCars[i].GetComponent<CarController>().sprite_name_idfk_ask_mike;
        }
    }
    public void EquipPowerup(int powerUpNumber)
    {
        if (itemSlot1Taken == false && powerUpNumber == 1)
        {
            powerUp1.GetComponent<RectTransform>().anchoredPosition = powerupLocation.anchoredPosition;
            Debug.Log("Banana UI?");
        }
        else if (itemSlot1Taken == false && powerUpNumber == 2)
        {
            powerUp2.GetComponent<RectTransform>().anchoredPosition = powerupLocation.anchoredPosition;
            itemSlot1Taken = true;
        }
        else if (itemSlot1Taken == false && powerUpNumber == 3)
        {
            powerUp3.GetComponent<RectTransform>().anchoredPosition = powerupLocation.anchoredPosition;
            itemSlot1Taken = true;
        }
        else if (itemSlot1Taken == false && powerUpNumber == 4)
        {
            powerUp4.GetComponent<RectTransform>().anchoredPosition = powerupLocation.anchoredPosition;
            itemSlot1Taken = true;
        }
        else if (itemSlot1Taken == false && powerUpNumber == 5)
        {
            powerUp5.GetComponent<RectTransform>().anchoredPosition = powerupLocation.anchoredPosition;
            itemSlot1Taken = true;
        }
    }
    public void UnEquipUI(int uiType)
    {
        if (uiType == 0)
        {
            powerUp1.GetComponent<RectTransform>().anchoredPosition = noItems.anchoredPosition;
            powerUp2.GetComponent<RectTransform>().anchoredPosition = noItems.anchoredPosition;
            powerUp3.GetComponent<RectTransform>().anchoredPosition = noItems.anchoredPosition;
            powerUp4.GetComponent<RectTransform>().anchoredPosition = noItems.anchoredPosition;
            powerUp5.GetComponent<RectTransform>().anchoredPosition = noItems.anchoredPosition;
        }
        if (uiType == 1)
        {
            Debug.Log("Banana?");
            powerUp1.GetComponent<RectTransform>().anchoredPosition = noItems.anchoredPosition;
            itemSlot1Taken = false;
        }
        if (uiType == 2)
        {
            powerUp2.GetComponent<RectTransform>().anchoredPosition = noItems.anchoredPosition;
            itemSlot1Taken = false;
        }
        if (uiType == 3)
        {
            powerUp3.GetComponent<RectTransform>().anchoredPosition = noItems.anchoredPosition;
            itemSlot1Taken = false;
        }
        if (uiType == 4)
        {
            powerUp4.GetComponent<RectTransform>().anchoredPosition = noItems.anchoredPosition;
            itemSlot1Taken = false;
        }
        if (uiType == 5)
        {
            powerUp5.GetComponent<RectTransform>().anchoredPosition = noItems.anchoredPosition;
            itemSlot1Taken = false;
        }
    }
}
