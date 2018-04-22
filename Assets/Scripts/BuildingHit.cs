using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHit : MonoBehaviour {

    public GameObject fireChild;
    private float BREAK_THRESHOLD = 4f;
    private bool destroyed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(!destroyed && collision.impulse.magnitude > BREAK_THRESHOLD)
        {
            Level.current.RegisterBuildingDestroyed();
            fireChild.SetActive(true);
        }
    }
}
