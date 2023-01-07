using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour
{
    [Header("Basic variables")]
    public int carsPassedR1 = 0;
    public int carsPassedR2 = 0;
    public int carsPassedR3 = 0;
    private void OnTriggerEnter(Collider other)
    {
    //    if (other.transform.GetComponent<CarController>() == true)
    //    {
    //        if (other.transform.GetComponent<CarController>().lapCounter == 0)
    //        {
    //            carsPassedR1++;
    //            other.transform.GetComponent<CarController>().currentPlace = carsPassedR1;
    //            other.transform.GetComponent<CarController>().ChangePlacement();
    //        }
    //        if (other.transform.GetComponent<CarController>().lapCounter == 1)
    //        {
    //            carsPassedR2++;
    //            other.transform.GetComponent<CarController>().currentPlace = carsPassedR2;
    //            other.transform.GetComponent<CarController>().ChangePlacement();
    //        }
    //        if (other.transform.GetComponent<CarController>().lapCounter == 2)
    //        {
    //            carsPassedR3++;
    //            other.transform.GetComponent<CarController>().currentPlace = carsPassedR3;
    //            other.transform.GetComponent<CarController>().ChangePlacement();
    //        }
    //    }
    }
}
