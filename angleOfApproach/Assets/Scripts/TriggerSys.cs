using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSys : MonoBehaviour
{
    bool touchDownTrigger = false;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && touchDownTrigger)
        {
            Debug.Log("Player TouchDown");
        }
        else if(other.gameObject.tag == "Player" && !touchDownTrigger)
        {
            Debug.Log("Player Out of Bounds");
        }
    }
}
