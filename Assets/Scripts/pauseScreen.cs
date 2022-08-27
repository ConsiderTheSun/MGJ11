using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class pauseScreen : MonoBehaviour{

	[Header("Set in Inspector")] 
	public GameObject pauseMenu;
	

	[Header("Set Dynamically")]
	static GameObject BoxNum;
	bool gameIsPaused = false;
	static Color textColor;
	static Color textColorPause;

	void Start(){
		BoxNum = GameObject.Find("PackageText");
		textColor = BoxNum.GetComponent<Text>().color;
		textColor = new Vector4(textColor.r, textColor.g, textColor.b, 1f);
		textColorPause = new Vector4(textColor.r, textColor.g, textColor.b, 0.2f);
    }

	public void gamePause(){
		gameIsPaused = true;
		pauseMenu.SetActive(true);
		BoxNum.GetComponent<Text>().color = textColorPause;

        Time.timeScale = 0f;
	}

	public void Resume(){
		gameIsPaused = false;
		pauseMenu.SetActive(false);
		BoxNum.GetComponent<Text>().color = textColor;
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
