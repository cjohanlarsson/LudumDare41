using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGoal : MonoBehaviour 
{
	[SerializeField] SpriteRenderer goalIcon;
	[SerializeField] float requiredDistToGoal = 0.5f;

	public Building TargetBuilding { get; set; }
	Building attachedBuilding;

	public bool IsSuccess
	{
		get
		{
			return attachedBuilding != null && 
				attachedBuilding.gameObject != null &&
				Vector3.Angle(Vector3.up, attachedBuilding.transform.up) < 10f &&
				Vector3.Distance( attachedBuilding.BasePosition, this.transform.position ) < requiredDistToGoal ;
		}
	}

	void Update()
	{
		if(IsSuccess  )
		{
			goalIcon.color = Color.green;
		}
		else
		{
			goalIcon.color = Color.red;
		}
	}

	void OnTriggerEnter(Collider c)
	{
		Debug.Log("entered" + c.gameObject.name.ToString());
		var b = c.gameObject.GetComponent<Building>();
		if(b != null && TargetBuilding == b)
		{
			Debug.Log("building found!");
			attachedBuilding = b;
		}
	}
}
