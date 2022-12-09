using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointFixer : MonoBehaviour
{
    public int fixesBug;
    public int nextCheckpoint;
    public bool effectPlayer = false;
    private void OnTriggerEnter(Collider other)
    {
        if (fixesBug == 1)
        {
            if (other.name == "Player")
            {
                if (effectPlayer == true)
                {
                    if (other.GetComponent<CarController>().checkPointCounter != nextCheckpoint)
                    {
                        other.GetComponent<CarController>().checkPointCounter = nextCheckpoint;
                    }
                }
            }
            else
            {
                if (other.GetComponent<CarController>().checkPointCounter != nextCheckpoint)
                {
                    other.GetComponent<CarController>().checkPointCounter = nextCheckpoint;
                }
            }
        }
        if (fixesBug == 2)
        {
            other.GetComponent<Transform>().position = other.GetComponent<CarController>().currentCheckpoint.transform.position;
        }

    }
}
