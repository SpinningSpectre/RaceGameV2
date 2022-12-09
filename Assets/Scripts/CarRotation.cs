using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRotation : MonoBehaviour
{
    public Transform raycastPosition;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(raycastPosition.position,-transform.up, out hit,1))
        {
            Debug.Log(hit.transform.name);
            transform.localRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }
}
