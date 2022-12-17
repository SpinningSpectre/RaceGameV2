using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    [Header("Transforms")]
    public RectTransform noItems;
    public RectTransform powerupLocation;
    [Header("Images")]
    public Image powerUp1;
    public Image powerUp2;
    public Image powerUp3;
    [Header("Bools")]
    bool itemSlot1Taken = false;
    private void Start()
    {
        UnEquipAllItems();
    }
    void UnEquipAllItems()
    {
        UnEquipUI(0);
    }
    private void Update()
    {

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
    }
    public void UnEquipUI(int uiType)
    {
        //0 is the all guns unequipped
        if (uiType == 0)
        {
            powerUp1.GetComponent<RectTransform>().anchoredPosition = noItems.anchoredPosition;
            powerUp2.GetComponent<RectTransform>().anchoredPosition = noItems.anchoredPosition;
            powerUp3.GetComponent<RectTransform>().anchoredPosition = noItems.anchoredPosition;
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
    }
}
