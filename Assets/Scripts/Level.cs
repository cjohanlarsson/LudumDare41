using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour 
{
	public static Level current;

	void Awake()
	{
		current = this;
	}

	void Update()
	{
		
	}
}
