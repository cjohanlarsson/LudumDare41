using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenDeath : MonoBehaviour {

    AudioSource squish;
    GameObject[] bloodSplatters;
    bool timeToDie = false;

	private void Start()
	{
        squish = GameObject.Find("Blood&Squish").GetComponent<AudioSource>();
        bloodSplatters = GameObject.Find("Blood&Squish").GetComponent<Blood>().bloodSplatters;
	}

	private void OnTriggerEnter(Collider other)
	{
        squish.Play();
        timeToDie = true;
        Level.current.RegisterPersonKilled();
        Instantiate(bloodSplatters[Random.Range(0,bloodSplatters.Length)], transform.position, Quaternion.identity);
    }

	private void LateUpdate()
	{
		if(timeToDie)
        {
            Destroy(transform.root.gameObject);
        }
	}
}
