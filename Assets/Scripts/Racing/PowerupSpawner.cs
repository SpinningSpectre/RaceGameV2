using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject powerBlock;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(powerBlock, transform.position, transform.rotation);
    }
    public void SpawnBox()
    {
        Instantiate(powerBlock, transform.position, transform.rotation);
    }
}