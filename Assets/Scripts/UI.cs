using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UI : MonoBehaviour 
{
	[SerializeField] private Button resetBtn;
	[SerializeField] private GameObject winScreen;

	void Awake()
	{
		if(resetBtn != null)
		{
			resetBtn.onClick.AddListener( () => {
				SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
			} );
		}


	}

	void Update()
	{
		if(Level.current != null)
		{
			winScreen.SetActive( Level.current.CurrentState == Level.State.Won );
		}
	}

}
