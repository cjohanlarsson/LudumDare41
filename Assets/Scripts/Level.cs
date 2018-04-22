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

	[SerializeField] Crane cranePrefab;
	[SerializeField] Transform craneStart;
	[SerializeField] private List<Segment> segments;
	[SerializeField] float boundsMinZ;
	[SerializeField] float boundsMaxZ;
	[SerializeField] float boundsMinX;
	[SerializeField] float boundsMaxX;

	public Vector3 GetMinBounds()
	{
		return new Vector3( boundsMinX, 0 , boundsMinZ );
	}

	public Vector3 GetMaxBounds()
	{
		return new Vector3( boundsMaxX, 0 , boundsMaxZ );
	}

	private LevelGoal currentGoal;
	private Building currentBuilding;
	private int currentIndex = 0;

    private int buildingsDestroyed = 0;
    private int peopleKilled = 0;

	public State CurrentState { get; set; }

    public int PeopleKilled {
        get {
            return peopleKilled + buildingsDestroyed*20;
        }
    }

	void Awake()
	{
		current = this;
		HideGoals();
	}

	void ResetCrane()
	{
		if(Crane.current != null)
		{
			GameObject.Destroy( Crane.current.gameObject );
		}
		Crane.current = GameObject.Instantiate( cranePrefab , craneStart.transform.position, craneStart.transform.rotation );
	}

	void HideGoals()
	{
		foreach(var s in this.segments)
		{
			if(!s.goal.IsSuccess)
				s.goal.gameObject.SetActive(false);
		}
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

				//Crane.current.ResetCrane();
				ResetCrane();

				yield return new WaitForSeconds(1f);

				var building = GameObject.Instantiate<Building>( segment.buildingPrefab );
				Crane.current.AttachBuilding( building );
				currentGoal.TargetBuilding = building;

				HideGoals();
				currentGoal.gameObject.SetActive(true);
			}
			else if(currentGoal.IsSuccess)
			{
				Crane.current.DetachBuilding( currentGoal.TargetBuilding );
				if(currentIndex == (segments.Count-1))
				{
					yield return new WaitForSeconds(3f);
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

    public void RegisterBuildingDestroyed()
    {
        buildingsDestroyed++;
    }

    public void RegisterPersonKilled()
    {
        peopleKilled++;
    }
}
