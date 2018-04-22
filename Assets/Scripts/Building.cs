using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour 
{
	[SerializeField] private Transform basePosition;
	public FixedJoint joint;

	public bool IsAttached
	{
		get
		{
			return Crane.current.AttachedBuilding == this;
		}
	}

	public Vector3 BasePosition
	{
		get
		{
			return basePosition.position;
		}
	}

}
