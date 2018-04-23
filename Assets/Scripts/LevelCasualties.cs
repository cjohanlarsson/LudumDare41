using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCasualties : MonoBehaviour 
{
	public static LevelCasualties current;

    private int peopleKilled;
    [SerializeField]
    private int maximumKillable;
    float casualtyRisk;

    public Text casualties;
  
    public bool TooManyCasualties
    {
    	get { return this.peopleKilled > this.maximumKillable; }
    }

    void Awake() {
		current = this;
    }

    void OnDestroy() {
		current = null;
    }

	// Use this for initialization
	void Start () {
		casualties = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(Level.current.PeopleKilled);
		var pk = Level.current.PeopleKilled;
		if(pk != peopleKilled)
		{
	        peopleKilled = pk;
	        casualties.text = "Lawyers have settled \n"+ peopleKilled+" out of "+maximumKillable+ "\n" + "acceptable casualties";
	        casualtyRisk = (float)peopleKilled/maximumKillable;
	        casualties.color = new Color((casualtyRisk+.2f), .2f, .2f);
        }
        //Debug.Log(casualtyRisk);
	}
}
