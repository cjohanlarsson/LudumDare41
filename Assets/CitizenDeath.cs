using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenDeath : MonoBehaviour {

    AudioSource squish;
	private void Start()
	{
        squish = Camera.main.GetComponent<AudioSource>();
	}
	private void OnTriggerEnter(Collider other)
	{
        squish.Play();
        //ADD CITIZEN--;
        Destroy(transform.root.gameObject);

	}
}
