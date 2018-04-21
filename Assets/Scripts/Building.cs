using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour 
{
	[SerializeField] private string buildingGameId;
	[SerializeField] private Transform basePosition;

	public string BuildingGameId
	{
		get { return this.buildingGameId; }
	}
	public Vector3 BasePosition
	{
		get
		{
			return basePosition.position;
		}
	}
}
