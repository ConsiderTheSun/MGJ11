using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour{
	[Header("Set in Inspector")]
	public PlayerController player;
	public GridController grid;
	public GUIController gui;


	[Header("Set Dynamically")]
	bool playing = true;

	// Start is called before the first frame update
	void Start(){
		grid.LoadMap();
	}

	// Update is called once per frame
	void Update(){
		
	}

	void FixedUpdate(){
		if(playing){
			player.FixedUpdatePlayer();
		}
		

	}

	public void TakeHit(){
		player.health--;
		if(player.health > 0){
			gui.SetHealth(player.health);
		}
		else if(player.health == 0){
			gui.SetHealth(player.health);
			EndGame(false);
		}

	}

	void EndGame(bool win){
		playing = false;
		Debug.Log("Game Over :(");
	}
}
