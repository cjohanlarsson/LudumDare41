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
        Cursor.lockState = CursorLockMode.Locked;

        for (int i=1;i<=(SceneManager.sceneCountInBuildSettings-1);i++)
		{
			var btn = GameObject.Instantiate<Button>(levelButton, levelButton.transform.parent);
			btn.GetComponentInChildren<Text>().text = "Job " + i.ToString() + "\nPress '" + i.ToString() + "'";
			int levelNum = i;
			btn.onClick.AddListener( () => {
				Debug.Log( "change scene" + levelNum.ToString() );
				SceneManager.LoadScene(levelNum);
			} );
		}

		levelButton.gameObject.SetActive(false);
	}

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            SceneManager.LoadScene(3);
        }
    }
}
