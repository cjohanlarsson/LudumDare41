using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCasualties : MonoBehaviour {

    [SerializeField]
    private int peopleKilled;
    [SerializeField]
    private int maximumKillable;

    public Text casualties;

	// Use this for initialization
	void Start () {
		casualties = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(Level.current.PeopleKilled);
        casualties.text = "Lawyers have settled "+ peopleKilled+" out of "+maximumKillable+" acceptable casualties";
        casualties.color = new Color((peopleKilled/maximumKillable), 0f, 0f);
	}
}
