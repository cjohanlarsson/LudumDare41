using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
	[SerializeField] int numberOfLevels = 2;
	[SerializeField] Button levelButton;

	void Awake()
	{
		for(int i=1;i<=(SceneManager.sceneCountInBuildSettings-1);i++)
		{
			var btn = GameObject.Instantiate<Button>(levelButton, levelButton.transform.parent);
			btn.GetComponentInChildren<Text>().text = "Job " + i.ToString();
			int levelNum = i;
			btn.onClick.AddListener( () => {
				Debug.Log( "change scene" + levelNum.ToString() );
				SceneManager.LoadScene(levelNum);
			} );
		}

		levelButton.gameObject.SetActive(false);
	}
}
