using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHit : MonoBehaviour {

    private float BREAK_THRESHOLD = 2f;
    private bool destroyed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(!destroyed && collision.impulse.magnitude > BREAK_THRESHOLD)
        {
            Debug.Log("Trigger Destroy");
        }
    }
}
