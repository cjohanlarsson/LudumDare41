using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenController : MonoBehaviour {

    public float startTime;
    public float moveDuration = 5;
    public bool readyForNewTarget = true;
    public static float XMIN = -75;
    public static float XMAX = 75;
    public static float ZMIN = -75;
    public static float ZMAX = 75;
    Transform model;
    bool frozenWithFear;

    public Camera cam;
    public NavMeshAgent agent;

    void Start()
    {
        startTime = Time.time;
        model = transform.GetChild(0);

    }

    // Update is called once per frame
    void Update()
    {
        if(readyForNewTarget & !frozenWithFear)
        {
            readyForNewTarget = false;
            startTime = Time.time;
            agent.SetDestination(NewTarget());

        }
        else if(Time.time - startTime > moveDuration)
        {
            readyForNewTarget = true;
        }

        /*if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                //MOVE OUR AGENT
                agent.SetDestination(hit.point);
            }
        }*/

    }

	private void OnTriggerEnter(Collider other)
	{
        frozenWithFear = true;
        GetComponent<AudioSource>().Play();
        Vector3 currentLocation = transform.position;
        agent.SetDestination(currentLocation);
        model.Rotate(0, 0, -30);
	}

	private void OnTriggerExit(Collider other)
	{
        frozenWithFear = false;
        model.Rotate(0, 0, 30);
	}

	public Vector3 NewTarget()
    {
        float randomX = Random.Range(XMIN, XMAX);
        float randomZ = Random.Range(ZMIN, ZMAX);
        return new Vector3(randomX, 0, randomZ);
    }
}
