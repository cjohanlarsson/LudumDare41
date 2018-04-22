using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenController : MonoBehaviour {

    public float startTime;
    public float moveDuration = 5;
    public bool readyForNewTarget = true;
    private static float XMIN = -6;
    private static float XMAX = 6;
    private static float ZMIN = -6;
    private static float ZMAX = 6;
    public static float MINMOVETIME = 3;
    public static float MAXMOVETIME = 6;
    Transform model;
    bool frozenWithFear;
    bool doMirrorImage = false;
    float randomX;
    float randomZ;

    private NavMeshAgent agent;

    void Start()
    {
        startTime = Time.time;
        model = transform.GetChild(0);
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(readyForNewTarget & !frozenWithFear)
        {
            readyForNewTarget = false;
            agent.isStopped = false;
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
            agent.isStopped = true;
            model.Rotate(0, 0, -25);
	}

	private void OnTriggerExit(Collider other)
	{
        frozenWithFear = false;
        model.Rotate(0, 0, 25);
	}

	public Vector3 NewTarget()
    {
        if(!doMirrorImage)
        {
            randomX = Random.Range(XMIN, XMAX);
            randomZ = Random.Range(ZMIN, ZMAX);
            doMirrorImage = !doMirrorImage;
        }
        else
        {
            randomX = -randomX;
            randomZ = -randomZ;
            doMirrorImage = !doMirrorImage;
        }
        moveDuration = Random.Range(MINMOVETIME, MAXMOVETIME);
        return new Vector3(randomX, -5, randomZ);
    }
}
