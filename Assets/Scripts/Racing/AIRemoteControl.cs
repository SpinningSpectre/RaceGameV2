using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AIRemoteControl : MonoBehaviour
{
    [Header("AI Input variables")]
    public float forwards = 1;
    public float turn = 1;
    CarController myCarController;
    [SerializeField]
    GameObject checkPoint;
    public int aiTurn = 80;
    // Start is called before the first frame update
    void Awake()
    {
        myCarController = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        checkPoint = myCarController.currentCheckpoint;
        Quaternion checkPointRotation = checkPoint.transform.rotation;
        Quaternion carRotation = transform.rotation;
        if (carRotation.y != checkPointRotation.y)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, checkPoint.transform.rotation, aiTurn * Time.deltaTime);
        }
        myCarController.ChangeSpeed(forwards);
        myCarController.Turn(turn);
    }

}
