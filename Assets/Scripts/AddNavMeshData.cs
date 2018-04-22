using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AddNavMeshData : MonoBehaviour {

    public NavMeshData navMeshData;

	// Use this for initialization
	void Awake () {
        NavMesh.AddNavMeshData(navMeshData);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
