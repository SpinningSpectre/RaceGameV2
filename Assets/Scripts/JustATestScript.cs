using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustATestScript : MonoBehaviour
{
    SceneLoader loader;
    SaveData saveData;
    // Start is called before the first frame update
    void Start()
    {
        loader = FindObjectOfType<SceneLoader>();
        saveData = FindObjectOfType<SaveData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (saveData.GetBool("DevMode") == true)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                loader.LoadScene("StartScreen");
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                loader.LoadScene("Blender");
            }
        }
    }
}
