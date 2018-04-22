using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenDeath : MonoBehaviour {

    AudioSource squish;
    GameObject[] bloodSplatters;

	private void Start()
	{
        squish = Camera.main.GetComponent<AudioSource>();
        bloodSplatters = GameObject.Find("Blood").GetComponent<Blood>().bloodSplatters;
	}

	private void OnTriggerEnter(Collider other)
	{
        squish.Play();
        Instantiate(bloodSplatters[Random.Range(0,bloodSplatters.Length)], transform.position, Quaternion.identity);
        //Level.current.RegisterPersonKilled();
        Destroy(transform.root.gameObject);
	}
}
