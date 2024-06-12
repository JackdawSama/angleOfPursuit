using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSys : MonoBehaviour
{
    [SerializeField] bool touchDownTrigger = false;

    //Events
    public delegate void TouchDownEvent();
    public static event TouchDownEvent touchDown;

    public delegate void OutOfBoundsEvent();
    public static event OutOfBoundsEvent outOfBounds;

    //Checks for collision trigger with player and calls the appropriate event
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && touchDownTrigger)
        {
            Debug.Log("Player TouchDown");
            touchDown();
        }
        else if(other.gameObject.tag == "Player" && !touchDownTrigger)
        {
            Debug.Log("Player Out of Bounds");
            outOfBounds();
        }
    }
}
