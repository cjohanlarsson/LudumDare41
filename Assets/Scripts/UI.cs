using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UI : MonoBehaviour 
{
	[SerializeField] private Button resetBtn;
	[SerializeField] private GameObject winScreen;
    [SerializeField] private Button nextLvlBtn;

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
	}

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

	void Update()
	{
		if(Level.current != null)
		{
			winScreen.SetActive( Level.current.CurrentState == Level.State.Won );
		}
	}

}
