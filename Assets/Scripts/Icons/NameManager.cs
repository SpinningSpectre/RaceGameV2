using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    public string playerName;
    public Text nameBox;
    public InputField nameField;
    public Transform offScreen;
    public Text nametext;
    SaveData savedata;
    public bool inStart;
    // Start is called before the first frame update
    void Start()
    {
        savedata = FindObjectOfType<SaveData>();
        playerName = savedata.GetString("PlayerName");
        if (nameBox != null)
        {
            nameBox.text = playerName;
        }
        if (playerName != "Enter Player Name")
        {
            if (inStart == true)
            {
                nametext.transform.position = nameField.transform.position;
                nametext.text = savedata.GetString("PlayerName");
                nameField.transform.position = offScreen.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void nameChange()
    {
        if (inStart == true)
        {
            nametext.transform.position = nameField.transform.position;
            nametext.text = savedata.GetString("PlayerName");
            nameField.transform.position = offScreen.position;
        }
    }
}
