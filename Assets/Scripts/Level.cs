using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour 
{
	public enum State
	{
		Idle,
		InProgress,
		Won,
		Lost
	}

	[System.Serializable]
	public class Segment
	{
		public Building buildingPrefab;
		public LevelGoal goal;
	}

	public static Level current;

	[SerializeField] private List<Segment> segments;

	private LevelGoal currentGoal;
	private Building currentBuilding;
	private int currentIndex = 0;

	public State CurrentState { get; set; }

	void Awake()
	{
		current = this;
	}

	IEnumerator Start()
	{
		if(segments.Count == 0)
			throw new System.Exception( "NOT ENOUGH SEGMENTS FOR THIS LEVEL - must be at least 1" );

		CurrentState = State.InProgress;

		while(CurrentState == State.InProgress)
		{
			if(currentGoal == null)
			{
				var segment = segments[currentIndex];
				currentGoal = segment.goal;
				var building = GameObject.Instantiate<Building>( segment.buildingPrefab );
				Crane.current.AttachBuilding( building );
				currentGoal.TargetBuilding = building;
			}
			else if(currentGoal.IsSuccess)
			{
				if(currentIndex == (segments.Count-1))
				{
					CurrentState = State.Won;
				}
				else
				{
					currentGoal = null;
					currentIndex++;
				}
			}
			yield return null;
		}

		CurrentState = State.Won;
	}
}
