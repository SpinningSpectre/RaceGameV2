using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float deathTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (deathTime < 10)
        {
            //Destroy(gameObject);
        }
        deathTime = deathTime +Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject);
    }
}
