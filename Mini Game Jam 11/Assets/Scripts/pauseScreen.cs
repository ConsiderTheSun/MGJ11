using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseScreen : MonoBehaviour{

	[Header("Set in Inspector")] 
	public GameObject pauseMenu;

	[Header("Set Dynamically")]
	bool gameIsPaused = false;


	void Start(){
	//pauseMenu.SetActive(false);
	}

	public void gamePause(){
		gameIsPaused = true;
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;
	}

	public void Resume(){
		gameIsPaused = false;
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
	}

	public void Home(int sceneID){
		Time.timeScale = 1f;
		SceneManager.LoadScene(sceneID);
	}

	

	void Update(){

		if(Input.GetKeyDown(KeyCode.Escape)){

			gameIsPaused = !gameIsPaused;
			if(gameIsPaused)
				gamePause();
			else
				Resume();

		}
	}
}
