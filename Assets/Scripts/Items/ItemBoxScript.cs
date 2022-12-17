using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxScript : MonoBehaviour
{
    ItemScript currentCar;
    public Transform Death;
    private void Start()
    {
        Death = GameObject.Find("Death").GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("HEEEEEEEEEEEEEEeEEEEEEEEY");
        if (collision.GetComponent<ItemScript>() && collision.GetComponent<ItemScript>().randomUpgrade == 0)
        {
            Debug.Log("Car spotted");
            currentCar = collision.GetComponent<ItemScript>();
            currentCar.RandomUpgrade();
            transform.position = Death.position;
            Destroy(gameObject);
        }

    }
}