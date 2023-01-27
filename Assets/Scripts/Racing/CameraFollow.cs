using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform Camera;
    SaveData saveData;
    // Start is called before the first frame update
    void Start()
    {
        saveData = FindObjectOfType<SaveData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (saveData.GetBool("CamUp") == true)
        {
            Camera.position = player.position + new Vector3(0, 10, 0);
            Camera.rotation = Quaternion.Euler(90, 0, 0);
        }
    }
}
