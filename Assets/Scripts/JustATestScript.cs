using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustATestScript : MonoBehaviour
{
    SceneLoader loader;
    // Start is called before the first frame update
    void Start()
    {
        loader = FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            loader.LoadScene("Shop");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            loader.LoadScene("Blender");
        }
    }
}
