﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UI : MonoBehaviour 
{
	[SerializeField] private Button resetBtn;
	[SerializeField] private GameObject winScreen;
	[SerializeField] private GameObject lossScreen;
    [SerializeField] private Button nextLvlBtn;
    [SerializeField] private GameObject PauseMenu;

    private bool paused = false;

    void Awake()
    {
        Debug.Log("We wake up");
        if (resetBtn != null)
        {
            resetBtn.onClick.AddListener(() => {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        }

        if (SceneManager.GetActiveScene().buildIndex > SceneManager.sceneCount)
        {
            nextLvlBtn.gameObject.SetActive(false);
        }

        PauseMenu.SetActive(false);
	}

    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToLevel(int lvl)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(lvl);
    }

	void Update()
	{
        if(Input.GetMouseButtonDown(0)) //Reset the cursor lockstate whenever player clicks
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

		if(Level.current != null)
		{
			winScreen.SetActive( Level.current.CurrentState == Level.State.Won );
			lossScreen.SetActive( Level.current.CurrentState == Level.State.Lost );
		}

        //Keyboard controls

        if (Input.GetKeyDown(KeyCode.P))
            TogglePause();
        if(Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (paused) //Keyboard level select when paused
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                SceneManager.LoadScene(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                SceneManager.LoadScene(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                SceneManager.LoadScene(3);
            }
        }
	}

    public void TogglePause()
    {
        paused = !paused;
        PauseMenu.SetActive(paused);
        if (paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

    }

}
